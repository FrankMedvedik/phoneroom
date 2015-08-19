using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.Areas.ReportCriteria
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PhoneroomsViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the PhoneroomsViewModel class.
        /// </summary>
        public PhoneroomsViewModel()
        {
            setup();
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
        
        private async void setup()
        {
            ShowGridData = false;
            _phoneroomRecruiterJobs = await PhoneroomRecruiterJobSvc.GetAllPhoneroomRecruiterJobs();
            PhoneRooms = PhoneRoomSvc.GetAll();
            SelectedPhoneRoomName = PhoneRoomSvc.GetDefault().Name;
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
                GetJobs();
                GetRecruiters();
                Messenger.Default.Send(new NotificationMessage(this, Notifications.PhoneroomChanged));
            }
        }


        private string _selectedPhoneRoomName;

        public string SelectedPhoneRoomName
        {
            get { return _selectedPhoneRoomName; }
            set
            {
                _selectedPhoneRoomName = value;
                NotifyPropertyChanged();
                GetJobs();
                GetRecruiters();
                Messenger.Default.Send(new NotificationMessage(this, Notifications.PhoneroomChanged));
            }
        }

        private System.Collections.Generic.List<PhoneroomRecruiterJob> _phoneroomRecruiterJobs =
            new System.Collections.Generic.List<PhoneroomRecruiterJob>();

        private ObservableCollection<Recruiter> _filteredRecruiters;
        private ObservableCollection<PhoneLogicTask> _filteredJobs;

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


        private void GetJobs()
        {
            List<PhoneLogicTask> jobs;
            if (!String.IsNullOrEmpty(SelectedPhoneRoomName) && SelectedPhoneRoomName != PhoneRoomSvc.GetDefault().Name)
            {
                jobs = (from t in _phoneroomRecruiterJobs
                    where t.PhoneRoom == SelectedPhoneRoomName
                        group t by new { t.jobNum, t.taskID, t.taskName, t.taskTypeID, t.taskDscr, t.tollFreeNumber }
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
            FilteredJobs = new ObservableCollection<PhoneLogicTask>(jobs);
        }

        private void GetRecruiters()
        {
              List<Recruiter> sips;
            if (!String.IsNullOrEmpty(SelectedPhoneRoomName) && SelectedPhoneRoomName != PhoneRoomSvc.GetDefault().Name) 
            {
                sips =
                (
                    from x in _phoneroomRecruiterJobs
                    where x.PhoneRoom == SelectedPhoneRoomName
                    group x by new {x.sip, x.EmailAddress, x.DisplayName, x.Description}
                    into grp
                    select new Recruiter()
                    {
                        DisplayName = grp.Key.DisplayName,
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
                    group x by new { x.sip, x.EmailAddress, x.DisplayName, x.Description }
                        into grp
                        select new Recruiter()
                        {
                            DisplayName = grp.Key.DisplayName,
                            EmailAddress = grp.Key.EmailAddress,
                            Description = grp.Key.Description
                        }).OrderBy(c => c.DisplayName).ToList();
            }

            FilteredRecruiters =  new ObservableCollection<Recruiter>(sips);
        }

    
    
    }
}



