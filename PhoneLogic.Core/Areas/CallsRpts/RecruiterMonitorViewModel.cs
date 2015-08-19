using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.CallRpt.Model;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public class RecruiterMonitorViewModel : CollectionViewModelBase
    {
        public RecruiterMonitorViewModel()
        {
            PhoneRooms = PhoneRoomSvc.GetAll();
            SelectedPhoneRoomName = PhoneRoomSvc.GetDefault().Name;

            Messenger.Default.Register<NotificationMessage<GlobalReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.GlobalReportCriteriaChanged)
                {
                    if (ReportDateRange != message.Content.ReportDateRange)
                    {
                        ReportDateRange = message.Content.ReportDateRange;
                        SelectedPhoneRoomName = message.Content.Phoneroom;
                        RefreshAll(null, null);
                    }
                    SelectedPhoneRoomName = message.Content.Phoneroom;
                }
            });
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

        private bool _canRefresh = true;
        public Boolean CanRefresh       
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables
        public  ReportDateRange ReportDateRange = new ReportDateRange();
        #endregion

        #region CallSummaries
        private ObservableCollection<LyncCallByRecruiter> _recruiters = new ObservableCollection<LyncCallByRecruiter>();
        private LyncCallByRecruiter _selectedRecruiter;


        public ObservableCollection<LyncCallByRecruiter> Recruiters
        {
            get { return _recruiters; }
            set
            {
                _recruiters = value;
                NotifyPropertyChanged();
            }
        }

        #region Filters
        private ObservableCollection<LyncCallByRecruiter > _filteredRecruiters = new ObservableCollection<LyncCallByRecruiter >();
        public ObservableCollection<LyncCallByRecruiter > FilteredRecruiters
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
            var fr = new List<LyncCallByRecruiter>();
            if ((SelectedPhoneRoomName == null) || (SelectedPhoneRoomName == "All"))
                FilteredRecruiters = Recruiters;
            else
            {
                fr = (from fobjs in Recruiters
                      where fobjs.PhoneRoom == SelectedPhoneRoomName
                      select fobjs).ToList();

                if (FilteredRecruiters.Count == fr.Count())
                    return;
                FilteredRecruiters = new ObservableCollection<LyncCallByRecruiter>(fr);
            }
        }
        #endregion


        #endregion

        protected override void RefreshAll(object sender, EventArgs e)
        {
            if (CanRefresh)
            {
                var s = SelectedRecruiter;
                GetRecruiters();
                if (s != null)
                {
                    SelectedRecruiter = Recruiters.First(x => x.RecruiterSIP == s.RecruiterSIP);
                }
            }
        }
        

        private string _headingText;
        public string HeadingText
        {
            get {return _headingText;}
            set 
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }
        public LyncCallByRecruiter SelectedRecruiter
        {
            get { return _selectedRecruiter; }
            set
            {
                _selectedRecruiter = value;
                NotifyPropertyChanged();
                //if (value != null)
                //    Messenger.Default.Send(new NotificationMessage<string>(this, SelectedRecruiter.RecruiterSIP, Notifications.CallRptRecruiterSet));
                //else
                //    Messenger.Default.Send(new NotificationMessage(this, Notifications.CallRptRecruiterCleared));
            }
        }

        public async void GetRecruiters()
        {
              ShowGridData = false;
              HeadingText = "Loading...";
            try
            {
                var ro = await LyncCallLogSvc.GetRecruitersPlusCallSummary(ReportDateRange.StartRptDate, ReportDateRange.EndRptDate);
                ShowGridData = true;
                Recruiters = new ObservableCollection<LyncCallByRecruiter>(ro);
                HeadingText = String.Format("{0} Phone Room(s) Call Stats for {1} through {2}", SelectedPhoneRoomName, ReportDateRange.StartRptDate, ReportDateRange.EndRptDate);
                RefreshFilteredData();
                LoadedOk = true;
            }
            catch (Exception e)
            {
                LoadFailed(e);
                HeadingText = e.Message;
            }

        }
    }
}