using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Lync.Model;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.ReportCriteria
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PhoneroomsViewModel : CollectionViewModelBase
    {
        private IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

        public bool ActiveOnly
        {
            get { return _activeOnly; }
            set
            {
                   _activeOnly = value;
                    NotifyPropertyChanged();
                    RefreshAll();
            }
        }

        public bool AllRecruiters
        {
            get { return _allRecruiters; }
            set
            {
                    _allRecruiters = value;
                    NotifyPropertyChanged();
                    RefreshAll();
            }
        }

        public bool AvailableOnly
        {
            get { return _availableOnly; }
            set
            {
                    _availableOnly = value;
                    NotifyPropertyChanged();
                    RefreshAll();
            }
        }
        public string JobHeading
        {
            get { return _jobHeading; }
            set { _jobHeading = value; NotifyPropertyChanged(); }
        }
        public string RecruiterHeading 
        {
            get { return _recruiterHeading; }
            set { _recruiterHeading = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Initializes a new instance of the PhoneroomsViewModel class.
        /// </summary>
        public PhoneroomsViewModel()
        {
            try
            {
                var a = (string) appSettings["PhoneRoomName"];
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                appSettings.Add("PhoneRoomName", PhoneRoomSvc.GetDefault().Name);
            }
            Setup();
        }

        private bool _showGridData;

        public bool ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }

        private async void Setup()
        {
            // gets universe of all jobs in all phonerooms associated with all recruiters 
            _phoneroomRecruiterJobs = await PhoneroomRecruiterJobSvc.GetAllPhoneroomRecruiterJobs();
            StartAutoRefresh(5);
            AvailableOnly = true;
            ActiveOnly = false;
            AllRecruiters = false;
            ShowGridData = false;
            // setting the selected phone room
            PhoneRooms = PhoneRoomSvc.GetAll();
            SelectedPhoneRoomName = (string) appSettings["PhoneRoomName"];
            // populated the lists 
            ShowGridData = true;

        }

        private ObservableCollection<PhoneLogic.Model.PhoneRoom> _PhoneRooms =
            new ObservableCollection<PhoneLogic.Model.PhoneRoom>();

        public ObservableCollection<PhoneLogic.Model.PhoneRoom> PhoneRooms
        {
            get { return _PhoneRooms; }
            set
            {
                _PhoneRooms = value;
                NotifyPropertyChanged();
            }
        }

        private string _oldPhoneRoomName ="not set";
        private string _selectedPhoneRoomName;

        public string SelectedPhoneRoomName
        {
            get { return _selectedPhoneRoomName; }
            set
            {
                _selectedPhoneRoomName = value;
                NotifyPropertyChanged();
                appSettings["PhoneRoomName"] = value;
                RefreshAll();

            }
        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
         Refresh();
        }

        private List<PhoneLogicTask> _allPhoneroomJobs = new List<PhoneLogicTask>();
        private List<Recruiter> _allPhoneRoomRecruiters = new List<Recruiter>();

        private async Task Refresh()
        {
            _allPhoneroomJobs = await GetAllPhoneroomJobs();
            FilteredJobs = new ObservableCollection<PhoneLogicTask>(_allPhoneroomJobs);
            _allPhoneRoomRecruiters = await GetAllPhoneroomRecruiters();
            FilteredRecruiters = new ObservableCollection<Recruiter>(GetActivePhoneroomRecruiters(_allPhoneRoomRecruiters));
            RecruiterHeading = string.Format("{0} {1} Recruiters", FilteredRecruiters.Count, GetRecruiterFilter());
            JobHeading = string.Format("{0} Jobs", FilteredJobs.Count);


            Console.WriteLine("Phone room -" + this.SelectedPhoneRoomName);
            Console.WriteLine("Job Cnt -" + _allPhoneroomJobs.Count);
            //Console.WriteLine("Job Number -" + _allPhoneroomJobs.First()?.JobNum);
            Console.WriteLine("Recruiter Cnt -" + _allPhoneRoomRecruiters.Count);
            //Console.WriteLine("Job Number -" + _allPhoneRoomRecruiters.First()?.sip);
            if (_oldPhoneRoomName != SelectedPhoneRoomName)
            {
                Messenger.Default.Send(new NotificationMessage<GlobalReportCriteria>(new GlobalReportCriteria()
                {
                    Phoneroom = this.SelectedPhoneRoomName,
                    PhoneroomJobs = _allPhoneroomJobs,
                    PhoneroomRecruiters = _allPhoneRoomRecruiters
                },
                    Notifications.PhoneroomChanged));
                _oldPhoneRoomName = SelectedPhoneRoomName;
            }

        }

        private string GetRecruiterFilter()
        {
            if (ActiveOnly) return "Active";
            if (AvailableOnly) return "Available";
            return "All";
        }

        private async Task<IEnumerable<PhoneLogicTask>> GetActivePhoneroomJobs(List<PhoneLogicTask> allPhoneroomJobs, List<RecruiterTimeSummary> activeRecruiters)
        {

            List<PhoneLogicTask> jobs;
            jobs = await Task.Factory.StartNew(() =>
            {

                   jobs = (from t in allPhoneroomJobs
                           join u in _phoneroomRecruiterJobs on t.JobNum equals u.jobNum
                           where activeRecruiters.Any(y => y.RecruiterSIP.Contains(u.sip)) 
                           group t by new { t.JobNum, t.TaskID, t.TaskName, t.TaskTypeID, t.TaskDscr, t.tollFreeNumber }
                        into grp
                           select new PhoneLogicTask()
                           {
                               JobNum = grp.Key.JobNum,
                               TaskID = grp.Key.TaskID,
                               TaskDscr = grp.Key.TaskDscr,
                               TaskName = grp.Key.TaskName,
                               TaskTypeID = grp.Key.TaskTypeID,
                               tollFreeNumber = grp.Key.tollFreeNumber
                           })
                        .OrderByDescending(x => x.JobFormatted)
                        .ToList();
                return jobs;
            });
            return jobs;
        }

        private List<PhoneroomRecruiterJob> _phoneroomRecruiterJobs =
            new List<PhoneroomRecruiterJob>();

        private ObservableCollection<Recruiter> _filteredRecruiters;
        private ObservableCollection<PhoneLogicTask> _filteredJobs;
        private bool _activeOnly;
        private string _recruiterHeading;
        private string _jobHeading;
        private bool _availableOnly;
        private bool _allRecruiters;

        public ObservableCollection<Recruiter> FilteredRecruiters
        {
            get { return _filteredRecruiters; }
            set
            {
                _filteredRecruiters = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<PhoneLogicTask> FilteredJobs
        {
            get { return _filteredJobs; }
            set
            {
                _filteredJobs = value;
                NotifyPropertyChanged();
            }
        }


        private async Task<List<PhoneLogicTask>> GetAllPhoneroomJobs()
        {

            List<PhoneLogicTask> jobs;
            jobs = await Task.Factory.StartNew(() =>
            {

                if (!String.IsNullOrEmpty(SelectedPhoneRoomName) &&
                    SelectedPhoneRoomName != PhoneRoomSvc.GetDefault().Name)
                {
                    jobs = (from t in _phoneroomRecruiterJobs
                        where t.PhoneRoom == SelectedPhoneRoomName
                        group t by new {t.jobNum, t.taskID, t.taskName, t.taskTypeID, t.taskDscr, t.tollFreeNumber}
                        into grp
                        select new PhoneLogicTask()
                        {
                            JobNum = grp.Key.jobNum,
                            TaskID = grp.Key.taskID,
                            TaskDscr = grp.Key.taskDscr,
                            TaskName = grp.Key.taskName,
                            TaskTypeID = grp.Key.taskTypeID,
                            tollFreeNumber = grp.Key.tollFreeNumber
                        })
                        .OrderByDescending(x => x.JobFormatted)
                        .ToList();
                }
                else
                {
                    jobs = (from t in _phoneroomRecruiterJobs
                        group t by new {t.jobNum, t.taskID, t.taskName, t.taskTypeID, t.taskDscr, t.tollFreeNumber}
                        into grp
                        select new PhoneLogicTask()
                        {
                            JobNum = grp.Key.jobNum,
                            TaskID = grp.Key.taskID,
                            TaskDscr = grp.Key.taskDscr,
                            TaskName = grp.Key.taskName,
                            TaskTypeID = grp.Key.taskTypeID,
                            tollFreeNumber = grp.Key.tollFreeNumber
                        })
                        .OrderByDescending(x => x.JobFormatted)
                        .ToList();
                }

                return jobs;
            });
            return jobs;
        }
        private List<Recruiter> GetActivePhoneroomRecruiters(List<Recruiter> allPhoneroomRecruiters )
        {

                var contactManager = LyncClient.GetClient().ContactManager;
                var activeRecruiters = new List<Recruiter>();

            if (AllRecruiters) return allPhoneroomRecruiters;

            if (ActiveOnly)
            {
                foreach (var r in allPhoneroomRecruiters)
                {
                    try
                    {
                        var c = contactManager.GetContactByUri(r.sip);
                        var ca = (ContactAvailability) c.GetContactInformation(ContactInformationType.Availability);
                        if ( ca != ContactAvailability.Offline)
                        {
                            activeRecruiters.Add(r);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Write("Bad sip " + r.sip + " " + e.Message);
                    }
                }
            }
            else if (AvailableOnly)
            {
                foreach (var r in allPhoneroomRecruiters)
                {
                    try
                    {
                  
                            var c = contactManager.GetContactByUri(r.sip);
                            var ca = (ContactAvailability) c.GetContactInformation(ContactInformationType.Availability);
                            if (ca == ContactAvailability.Free || ca == ContactAvailability.FreeIdle)
                            {
                                activeRecruiters.Add(r);
                            }
                    }
                    catch (Exception e)
                    {
                        Console.Write("Bad sip " + r.sip + " " + e.Message);
                    }
                }
            }
            return activeRecruiters;
        }

        private async Task<List<Recruiter>> GetAllPhoneroomRecruiters()
        {

            List<Recruiter> sips;
            sips = await Task.Factory.StartNew(() =>
            {

                if (!String.IsNullOrEmpty(SelectedPhoneRoomName) &&
                    SelectedPhoneRoomName != PhoneRoomSvc.GetDefault().Name)
                {
                    sips =
                        (
                            from x in _phoneroomRecruiterJobs
                            where x.PhoneRoom == SelectedPhoneRoomName
                            where x.sip.Length > 4
                            group x by new {x.sip, x.EmailAddress, x.DisplayName, x.Description}
                            into grp
                            select new Recruiter()
                            {
                                DisplayName = grp.Key.DisplayName?.Length > 4 ? grp.Key.DisplayName.Substring(0, grp.Key.DisplayName.IndexOf(' ')) + " " +
                                            grp.Key.DisplayName.Substring(grp.Key.DisplayName.IndexOf(' ') + 1, 1) : grp.Key.DisplayName,
                                EmailAddress = grp.Key.EmailAddress,
                                Description = grp.Key.Description
                            })
                            .OrderBy(c => c.DisplayName).ToList();
                }
                else
                {
                    sips =
                        (
                            from x in _phoneroomRecruiterJobs
                            group x by new {x.sip, x.EmailAddress, x.DisplayName, x.Description}
                            into grp
                            select new Recruiter()
                            {
                                DisplayName = grp.Key.DisplayName?.Length > 4 ?  grp.Key.DisplayName.Substring(0, grp.Key.DisplayName.IndexOf(' ')) + " " +
                                            grp.Key.DisplayName.Substring(grp.Key.DisplayName.IndexOf(' ') + 1, 1): grp.Key.DisplayName,
                                EmailAddress = grp.Key.EmailAddress,
                                Description = grp.Key.Description
                            }).OrderBy(c => c.DisplayName).ToList();
                }
                return sips;
            });
            return sips;
        }
       
    }
}