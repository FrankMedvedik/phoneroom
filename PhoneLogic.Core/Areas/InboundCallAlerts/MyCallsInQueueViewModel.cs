using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.InboundCallAlerts

{
    public class MyCallsInQueueViewModel : CollectionViewModelBase
    {
        public MyCallsInQueueViewModel()
        {
            StartAutoRefresh(ApiRefreshFrequency.LyncApi);
        }

        private CallInQueue _selectedCallInQueue;

        public CallInQueue SelectedCallInQueue
        {
            get { return _selectedCallInQueue; }
            set
            {
                _selectedCallInQueue = value;
                NotifyPropertyChanged();
            }
        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetMyQueuedCalls();
        }

        public async void GetMyQueuedCalls()
        {
            try
            {
                LoadDate = DateTime.Now;
                var cq = await LyncSvc.GetMyQueuedCalls();

                if (cq.Count > 0)
                {
                    CallsInQueue = new ObservableCollection<CallInQueue>(cq.Select(c => new CallInQueue()
                    {
                        CallerId = c.CallerId,
                        Id = c.Id,
                        JobNumber = c.JobNumber,
                        TimeIn = c.TimeIn
                    }).ToList());

                    ShowGridData = true;
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

        #region CallsInQueue

        private ObservableCollection<CallInQueue> _CallsInQueue = new ObservableCollection<CallInQueue>();

        public ObservableCollection<CallInQueue> CallsInQueue
        {
            get { return _CallsInQueue; }
            set
            {
                _CallsInQueue = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("TheBackground");
                NotifyPropertyChanged("TheForeground");
                AppColors ac = new AppColors()
                {
                    TheForeground = ColorToBrushSvc.GetForeground(CallsInQueue.Count),
                    TheBackground = ColorToBrushSvc.GetBackground(CallsInQueue.Count)
                };
                Messenger.Default.Send(ac);
            }
        }

        #endregion

        #region DisplayColors

        public string TheBackground
        {
            get { return ColorMappingSvc.GetBackground(CallsInQueue.Count); }
        }

        public string TheForeground
        {
            get { return ColorMappingSvc.GetForeground(CallsInQueue.Count); }
        }

        public new Boolean ShowGridData
        {
            set
            {
                if (value == false)
                {
                    AppColors ac = new AppColors()
                    {
                        TheForeground = ColorToBrushSvc.GetForeground(0),
                        TheBackground = ColorToBrushSvc.GetBackground(0)
                    };
                    Messenger.Default.Send(ac);
                }
                base.ShowGridData = value;
            }
            get { return base.ShowGridData; }
        }

        #endregion
    }
}