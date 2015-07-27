using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.PhoneRooms
{
    public class AllRecruitersViewModel : CollectionViewModelBase
    {
        public AllRecruitersViewModel()
        {
            GetRecruiters();
            PhoneRooms = PhoneRoomSvc.GetAll();
            SelectedPhoneRoomName = PhoneRoomSvc.GetDefault().Name;
            StartRptDate = DateTime.Now.AddDays(-1);
            EndRptDate = DateTime.Now;
            Messenger.Default.Register<NotificationMessage>(this, HandleNotification);
        }

        private void HandleNotification(NotificationMessage message)
        {
            if (message.Notification == Notifications.PauseRefresh)
            {
                CanRefresh = false;
            }
            if (message.Notification == Notifications.Refresh)
            {
                CanRefresh = true;
            }
        }

        public Boolean CanRefresh       
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
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
                SelectedRecruiter = null;
                RefreshFilteredData();
            }
        }

        private ObservableCollection<PhoneLogic.Model.PhoneRoom> _PhoneRooms = new ObservableCollection<PhoneLogic.Model.PhoneRoom>();
        public ObservableCollection<PhoneLogic.Model.PhoneRoom> PhoneRooms
        {
            get { return _PhoneRooms; }
            set
            {
                _PhoneRooms = value;
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
            if (CanRefresh)
            {
                var sr = SelectedRecruiter;
                GetRecruiters();
                if (sr != null)
                {
                    SelectedRecruiter = Recruiters.FirstOrDefault(x => x.sip == sr.sip);
                    GetRecruiterLogs();
                }
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
                if (_selectedRecruiter != null)
                {
                    GetRecruiterLogs();
                    ShowSelectedRecruiter = true;
                }
                else 
                    ShowSelectedRecruiter = false;
                NotifyPropertyChanged();

            }
        }
        #endregion

        #region RecruiterLogs

        private ObservableCollection<Call> _recruiterLogs = new ObservableCollection<Call>();

        public ObservableCollection<Call> RecruiterLogs
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

        #region SelectedCall

        private Call _selectedCall;
        public Call SelectedCall
        {
            get
            {
                return _selectedCall;
            }
            set
            {
                _selectedCall = value;
                //ShowSelectedRecruiterLog = (_selectedRecruiter != null);
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
        private bool _canRefresh;

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
                    var ro = await LyncCallLogSvc.GetCalls(SelectedRecruiter.sip , StartRptDate, EndRptDate);
                    var r = new ObservableCollection<Call>(ro);
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