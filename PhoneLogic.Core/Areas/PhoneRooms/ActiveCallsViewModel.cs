using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.PhoneRooms
{
    public class ActiveCallsViewModel : CollectionViewModelBase
    {
        #region ActiveCalls

        ObservableCollection<ActiveCallDetail> _activeCalls = new ObservableCollection<ActiveCallDetail>();

        private ObservableCollection<ActiveCallDetail> _filteredActiveCalls =
            new ObservableCollection<ActiveCallDetail>();

        public ObservableCollection<ActiveCallDetail> ActiveCalls
        {
            get { return _activeCalls; }
            set
            {
                if (_activeCalls != value)
                {
                    _activeCalls = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<ActiveCallDetail> FilteredActiveCalls
        {
            get { return _filteredActiveCalls; }
            set
            {
                _filteredActiveCalls = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("TheForeground");
                NotifyPropertyChanged("TheBackground");
            }
        }

        private void FilterCalls()
        {
            var acd = new ObservableCollection<ActiveCallDetail>();
            if (MyRecruiters.Any() && ActiveCalls.Any())
                acd = new ObservableCollection<ActiveCallDetail>
                    (from c in ActiveCalls
                        join b in MyRecruiters on c.RecruiterUri equals b.sip
                        select c);
            FilteredActiveCalls = acd;
            if (FilteredActiveCalls.Any())
                ShowGridData = true;
            else
                ShowGridData = false;
        }

        #endregion

        // sets up the 
        public ActiveCallsViewModel()
        {
            StartAutoRefresh(ApiRefreshFrequency.LyncApi);
            MyRecruiters = new List<Recruiter>();

            Messenger.Default.Register<NotificationMessage<GlobalReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.PhoneroomChanged)
                {
                    MyRecruiters = message.Content.PhoneroomRecruiters;
                    RefreshAll(null, null);
                }
            });
            RefreshAll();
        }
        public string CallsInQueueHeading
        {
            get { return _callsInQueueHeading; }
            set
            {
                _callsInQueueHeading = value;
                NotifyPropertyChanged();
            }
        }
        public List<Recruiter> MyRecruiters { get; set; }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetActiveCalls();
            FilterCalls();
            CallsInQueueHeading = String.Format("{0} Active Calls", FilteredActiveCalls.Count);
        }

        #region SelectedActiveCall

        private ActiveCallDetail _selectedActiveCall;
        private string _callsInQueueHeading;

        public ActiveCallDetail SelectedActiveCall
        {
            get { return _selectedActiveCall; }
            set
            {
                _selectedActiveCall = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        public async void GetActiveCalls()
        {
            try
            {
                LoadDate = DateTime.Now;
                ActiveCalls = await LyncSvc.GetActiveCallsDetail();
                LoadedOk = true;
            }
            catch (Exception e)
            {
                LoadFailed(e);
            }
        }

        #region DisplayColors

        public string TheBackground
        {
            get
            {
                var callCnt = 0;
                try
                {
                    callCnt = FilteredActiveCalls.Count;
                }
                catch (Exception e)
                {
                    callCnt = 0;
                }

                return ColorMappingSvc.GetBackground(callCnt);
            }
        }

        public string TheForeground
        {
            get
            {
                var callCnt = 0;
                try
                {
                    callCnt = FilteredActiveCalls.Count;
                }
                catch (Exception e)
                {
                    callCnt = 0;
                }

                return ColorMappingSvc.GetForeground(callCnt);
            }
        }

        #endregion
    }
}