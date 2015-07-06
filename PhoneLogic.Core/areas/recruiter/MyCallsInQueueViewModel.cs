using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.MVVMMessenger;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.areas.recruiter
    
{
    public class MyCallsInQueueViewModel : CollectionViewModelBase
    {
  
        // sets up the 
        public MyCallsInQueueViewModel()
        {
         StartAutoRefresh(ApiRefreshFrequency.LyncApi);
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
                var cq = await LyncSvc.GetMyQueueSummary();
                if (cq.Count > 0)
                {
                  CallsInQueue = cq;
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

         private ObservableCollection<QueueSummary> _CallsInQueue = new ObservableCollection<QueueSummary>();

         public ObservableCollection<QueueSummary> CallsInQueue
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
                     TheForeground = ColorToBrushSvc.GetForeground(CallsInQueue.Sum(c => c.InQueue)),
                     TheBackground = ColorToBrushSvc.GetBackground(CallsInQueue.Sum(c => c.InQueue))
                 };
                 Messenger.Default.Send(ac);
             }
         }
        #endregion

         #region DisplayColors

         public  string TheBackground
        {
            get
            {
                int callCnt = CallsInQueue.Sum(c => c.InQueue);
                return ColorMappingSvc.GetBackground(callCnt);
            }
        }

        public string TheForeground
        {
            get {
                int callCnt = CallsInQueue.Sum(c => c.InQueue);
                return ColorMappingSvc.GetForeground(callCnt);
            }
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
