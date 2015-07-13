using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Areas.CallsRpts;
using PhoneLogic.Core.areas.CallsRpts.Models;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.areas.phoneroom
{
    public class CallReportViewModel : CollectionViewModelBase
    {
        public CallReportViewModel()
        {
            GetCalls();
            PhoneRooms = PhoneRoomSvc.GetAll();
            SelectedPhoneRoomName = PhoneRoomSvc.GetDefault();
            StartRptDate = DateTime.Now.AddDays(-1);
            EndRptDate = DateTime.Now;
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
                RefreshFilteredData();
            }
        }

        private string _selectedJobNum;
        public string SelectedJobNum
        {
            get { return _selectedJobNum; }
            set
            {
                _selectedJobNum = value;
                NotifyPropertyChanged();
                RefreshFilteredData();
            }
        }

        private string _selectedRecruiter;
        public string SelectedRecruiter
        {
            get { return _selectedRecruiter; }
            set
            {
                _selectedRecruiter = value;
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

        #region CallSummaries
        private ObservableCollection<ByJob> _jobs = new ObservableCollection<ByJob>();
        public ObservableCollection<ByJob> Jobs
        {
            get { return _jobs; }
            set
            {
                _jobs = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ByRecruiter> _recruiters = new ObservableCollection<ByRecruiter>();
        public ObservableCollection<ByRecruiter> Recruiters
        {
            get { return _recruiters; }
            set
            {
                _recruiters = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ByJobsForRecruiter> _recruiterJobs = new ObservableCollection<ByJobsForRecruiter>();
        public ObservableCollection<ByJobsForRecruiter> RecruiterJobs
        {
            get { return _recruiterJobs; }
            set
            {
                _recruiterJobs = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ByRecruitersForJob> _jobRecruiters = new ObservableCollection<ByRecruitersForJob>();
        public ObservableCollection<ByRecruitersForJob> JobRecruiters
        {
            get { return _jobRecruiters; }
            set
            {
                _jobRecruiters = value;
                NotifyPropertyChanged();
            }
        }





        #region Filters

        private ObservableCollection<Call> _filteredCalls = new ObservableCollection<Call>();
        public ObservableCollection<Call> FilteredCalls
        {
            get { return _filteredCalls; }
            set
            {
                _filteredCalls = value;
                NotifyPropertyChanged();
            }
        }

        private void RefreshFilteredData()
        {
            FilterByPhoneRoom();
        }

        private void FilterByPhoneRoom()
        {
            var fr = new List<Call>();
            if ((SelectedPhoneRoomName == null) || (SelectedPhoneRoomName == "All"))
                FilteredCalls = Calls;
            else
            {
                fr = (from fobjs in Calls
                      where fobjs.PhoneRoom == SelectedPhoneRoomName
                      select fobjs).ToList();

                FilteredCalls = new ObservableCollection<Call>(fr);
            }
        }
        #endregion

        protected override void RefreshAll(object sender, EventArgs e)
        {
            if (CanRefresh)
            {
                var sr = SelectedCall;
                GetCalls();
                //if (sr != null)
                //{
                //    SelectedCall = Calls.FirstOrDefault(x => x.CallId == sr.sip);
                //    GetCalls();
                //}
            }
        }

        #region Calls

        private ObservableCollection<Call> _calls = new ObservableCollection<Call>();

        public ObservableCollection<Call> Calls
        {
            get
            {
                return _calls;
            }
            set
            {
                _calls = value;
                NotifyPropertyChanged();
            }
        }


        public async void GetCalls()
        {
            try
            {
                var ro = await LyncCallLogSvc.GetLynCallsByJob(StartRptDate, EndRptDate);
                if (ro.Count > 0)
                {
                    ShowGridData = true;
                    Jobs = new ObservableCollection<ByJob>(ro);
                }
                else
                    ShowGridData = false;
                LoadedOk = true;
            }
            catch (Exception e)
            {
                LoadFailed(e);
            }
        }

        #endregion

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
                if (_selectedCall != null)
                {
                    ShowSelectedCall = true;
                }
                else
                    ShowSelectedCall = false;
                NotifyPropertyChanged();

            }
        }

        public bool ShowSelectedCall
        {
            get { return _showSelectedCall; }
            set { _showSelectedCall = value; 
                    NotifyPropertyChanged(); }
        }

        #endregion

        
        #region SelectedRecruiterLog

        private Call _selectedRecruiterLog;
        public Call SelectedRecruiterLog
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
        private bool _canRefresh;
        private bool _showSelectedCall;

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
                    var ro = await LyncCallLogSvc.GetCall(SelectedRecruiter , StartRptDate, EndRptDate);
                    var r = new ObservableCollection<Call>(ro);
                    if (ro.Count > 0)
                    {
                        ShowLogData = true;
                        Calls = r;
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