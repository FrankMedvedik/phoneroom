using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;


namespace PhoneLogic.Core.ViewModels
{
    public class AllRecruitersViewModel : CollectionViewModelBase
    {
        public AllRecruitersViewModel()
        {
            GetRecruiters();
            PhoneRooms = PhoneRoomSvc.GetAll();
            SelectedPhoneRoomName = PhoneRoomSvc.GetDefault();
            StartRptDate = DateTime.Now.AddDays(-1);
            EndRptDate = DateTime.Now;
        }


        #region reporting variables

        private DateTime _startRptDate = DateTime.Now.AddDays(-1);
        public DateTime StartRptDate
        {
            get { return _startRptDate; }
            set
            {
                _startRptDate = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _endRptDate = DateTime.Now;
        public DateTime EndRptDate
        {
            get { return _endRptDate; }
            set
            {
                _endRptDate = value;
                NotifyPropertyChanged();
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
                RefreshFilteredData();
            }
        }

        private ObservableCollection<PhoneRoom> _phoneRooms = new ObservableCollection<PhoneRoom>();
        public ObservableCollection<PhoneRoom> PhoneRooms
        {
            get { return _phoneRooms; }
            set
            {
                _phoneRooms = value;
                NotifyPropertyChanged();
            }
        }



        #endregion

        #region Filters
        private ObservableCollection<Recruiter> _filteredRecruiters = new ObservableCollection<Recruiter>();
        public ObservableCollection<Recruiter> FilteredRecruiters
        {
            get { return _filteredRecruiters; }
            set
            {
                _filteredRecruiters = value;
                NotifyPropertyChanged();
            }
        }

        private void RefreshFilteredData()
        {
            FilterByPhoneRoom();
        }

        private void FilterByPhoneRoom()
        {
            var fr = new List<Recruiter>();
            if ((SelectedPhoneRoomName == null) || (SelectedPhoneRoomName == "All"))
                FilteredRecruiters = Recruiters;
            else
            {
                fr = (from fobjs in Recruiters
                      where fobjs.PhoneRoom == SelectedPhoneRoomName
                      select fobjs).ToList();

                if (FilteredRecruiters.Count == fr.Count())
                    return;
                FilteredRecruiters = new ObservableCollection<Recruiter>(fr);
            }
        }
        #endregion

        protected override void RefreshAll(object sender, EventArgs e)
        {
            var sr = SelectedRecruiter;
            GetRecruiters();
            if (sr != null)
            {
                SelectedRecruiter = Recruiters.FirstOrDefault(x => x.sip == sr.sip);
                GetRecruiterLogs();
            }
        }

        #region Recruiters

        private ObservableCollection<Recruiter> _recruiters = new ObservableCollection<Recruiter>();

        public ObservableCollection<Recruiter> Recruiters
        {
            get
            {
                return _recruiters;
            }
            set
            {
                _recruiters = value;
                NotifyPropertyChanged();
            }
        }


        public async void GetRecruiters()
        {
            try
            {
                var ro = await RecruiterSvc.GetRecruiters();
                if (ro.Count > 0)
                {
                    ShowGridData = true;
                    Recruiters = new ObservableCollection<Recruiter>(ro);
                }
                else
                    ShowGridData = false;
                LoadedOk = true;
                RefreshFilteredData();
            }
            catch (Exception e)
            {
                LoadFailed(e);
            }
        }

        #endregion

        #region SelectedRecruiter

        private Recruiter _selectedRecruiter;
        public Recruiter  SelectedRecruiter
        {
            get
            {
                return _selectedRecruiter;
            }
            set
            {
                _selectedRecruiter = value;
                ShowSelectedRecruiter= (_selectedRecruiter != null);
                NotifyPropertyChanged();
                GetRecruiterLogs();
            }
        }
        #endregion

        #region RecruiterLogs

        private ObservableCollection<RecruiterLog> _recruiterLogs = new ObservableCollection<RecruiterLog>();

        public ObservableCollection<RecruiterLog> RecruiterLogs
        {
            get
            {
                return _recruiterLogs;
            }
            set
            {
                _recruiterLogs = value;
                NotifyPropertyChanged();
            }
        }

        #region SelectedRecruiterLog

        private RecruiterLog _selectedRecruiterLog;
        public RecruiterLog SelectedRecruiterLog
        {
            get
            {
                return _selectedRecruiterLog;
            }
            set
            {
                _selectedRecruiterLog = value;
                //ShowSelectedRecruiterLog = (_selectedRecruiterLog != null);
                NotifyPropertyChanged();
            }
        }
        #endregion

        
        private Boolean _showLogData = false;
        public Boolean ShowLogData
        {
            get
            {
                return _showLogData;
            }
            set
            {
                _showLogData = value;
                NotifyPropertyChanged();
            }
        }
        private Boolean _showSelectedRecruiter = false;
        public Boolean ShowSelectedRecruiter
        {
            get
            {
                return _showSelectedRecruiter;
            }
            set
            {
                _showSelectedRecruiter = value;
                NotifyPropertyChanged();
            }
        }


        public async void GetRecruiterLogs()
        {
            if (SelectedRecruiter == null)
            {
                ShowLogData = false;
                LoadedOk = true;
            }
            else
                try
                {
                    var ro = await RecruiterSvc.GetRecruiterLog(SelectedRecruiter.sip, StartRptDate, EndRptDate);
                    var r = new ObservableCollection<RecruiterLog>(ro);
                    if (ro.Count > 0)
                    {
                        ShowLogData = true;
                        RecruiterLogs = r;
                    }
                    else
                        ShowLogData = false;
                    LoadedOk = true;
                }
                catch (Exception e)
                {
                    LoadFailed(e);
                }
        }

        #endregion
    }
}