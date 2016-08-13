using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.PhoneRooms

{
    public class CallsInQueueViewModel : CollectionViewModelBase
    {
        public CallsInQueueViewModel()
        {
            Messenger.Default.Register<NotificationMessage<GlobalReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.PhoneroomChanged)
                {
                    Jobs = new ObservableCollection<PhoneLogicTask>(message.Content.PhoneroomJobs.ToList());
                    RefreshAll(null, null);
                }
            });
            StartAutoRefresh(ApiRefreshFrequency.LyncApi);
        }

        // sets up the 
        public string CallsInQueueHeading
        {
            get { return _callsInQueueHeading; }
            set
            {
                _callsInQueueHeading = value;
                NotifyPropertyChanged();
            }
        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetQueuedCalls();
            FilterCalls();
            CallsInQueueHeading = string.Format("{0} Calls in Queue", FilteredCallsInQueue.Sum(x => x.InQueue));
        }

        public void testGetMyQueuedCalls()
        {
            CallsInQueue.Clear();
            CallsInQueue.Add(new QueueSummary
            {
                InQueue = 100,
                JobNumber = "9999-9999"
            });
            CallsInQueue.Add(new QueueSummary
            {
                InQueue = 0,
                JobNumber = "1111-1111"
            });
            LoadDate = DateTime.Now;
            ShowGridData = true;
            LoadedOk = true;
        }


        public async void GetQueuedCalls()
        {
            try
            {
                LoadDate = DateTime.Now;
                var cq = await LyncSvc.GetAllQueueSummary();
                CallsInQueue = cq;
                LoadedOk = true;
            }
            catch (Exception e)
            {
                LoadFailed(e);
            }
        }

        #region Jobs

        public ObservableCollection<PhoneLogicTask> Jobs
        {
            get { return _jobs; }
            set
            {
                _jobs = value;
                NotifyPropertyChanged();
            }
        }


        private ObservableCollection<QueueSummary> _filteredCallsInQueue = new ObservableCollection<QueueSummary>();

        public ObservableCollection<QueueSummary> FilteredCallsInQueue
        {
            get { return _filteredCallsInQueue; }
            set
            {
                _filteredCallsInQueue = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("TheForeground");
                NotifyPropertyChanged("TheBackground");
            }
        }

        private void FilterCalls()
        {
            var acd = new ObservableCollection<QueueSummary>();

            //    if(Jobs.Any() && CallsInQueue.Any())
            acd = new ObservableCollection<QueueSummary>
                (from c in CallsInQueue
                    join b in Jobs on c.jobFormatted equals b.JobFormatted
                    select c);
            FilteredCallsInQueue = acd;
            if (FilteredCallsInQueue.Any())
                ShowGridData = true;
            else
                ShowGridData = false;
        }

        #endregion

        #region CallsInQueue

        private ObservableCollection<QueueSummary> _CallsInQueue = new ObservableCollection<QueueSummary>();
        private ObservableCollection<PhoneLogicTask> _jobs = new ObservableCollection<PhoneLogicTask>();
        private string _callsInQueueHeading;

        public ObservableCollection<QueueSummary> CallsInQueue
        {
            get { return _CallsInQueue; }
            set
            {
                _CallsInQueue = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("TheBackground");
                NotifyPropertyChanged("TheForeground");
            }
        }

        #endregion

        #region DisplayColors

        public string TheBackground
        {
            get
            {
                var callCnt = 0;

                foreach (var c in FilteredCallsInQueue)
                {
                    callCnt += c.InQueue;
                }

                return ColorMapping.GetBackground(callCnt);
            }
        }

        public string TheForeground
        {
            get
            {
                var callCnt = 0;

                foreach (var c in FilteredCallsInQueue)
                {
                    callCnt += c.InQueue;
                }

                return ColorMapping.GetForeground(callCnt);
            }
        }

        #endregion
    }
}