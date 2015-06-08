using System;
using System.Collections.ObjectModel;
using PhoneLogic.Model;
using QueueSummary = PhoneLogic.Model.QueueSummary;


namespace PhoneLogic.Core.ViewModels
    
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
             }
         }
        #endregion

         #region DisplayColors

         public  string TheBackground
        {
            get
            {
                int callCnt = 0;

                foreach (var c in CallsInQueue)
                {
                    callCnt += c.InQueue;
                }

                return ColorMappingSvc.GetBackground(callCnt);
            }
        }

        public string TheForeground
        {
            get {
                int callCnt = 0;

                foreach (var c in CallsInQueue)
                {
                    callCnt += c.InQueue;
                }

                return ColorMappingSvc.GetForeground(callCnt);
            }
        }

        #endregion
        
    }
}
