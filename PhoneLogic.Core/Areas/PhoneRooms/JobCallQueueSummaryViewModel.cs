using System;
using System.Collections.ObjectModel;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.PhoneRooms
{
    public class JobCallQueueSummaryViewModel : CollectionViewModelBase
    {

        #region JobCallQueueSummary

        ObservableCollection<JobCallSummary> _jobCallSummaries;

        public ObservableCollection<JobCallSummary> JobCallSummaries
        {
            get { return _jobCallSummaries; }
            set
            {
                _jobCallSummaries = value;
                NotifyPropertyChanged();
            }
        }

        #endregion


        // sets up the 
        public JobCallQueueSummaryViewModel()
        {
            //StartAutoRefresh(ApiRefreshFrequency.LyncApi);
            RefreshAll();

        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetJobCallQueueSummary();
        }

        public void testGetMyQueuedCalls()
        {

            JobCallSummaries.Clear();
            JobCallSummaries.Add(new JobCallSummary
            {
                InQueue = 100,
                JobNumber = "9999-9999"
            });
            JobCallSummaries.Add(new JobCallSummary
            {
                InQueue = 0,
                JobNumber = "1111-1111"
            });
            LoadDate = DateTime.Now;
            ShowGridData = true;
            LoadedOk = true;
        }

        public void yGetMyQueuedCalls()
        {
            JobCallSummaries.Clear();
            LoadFailed(new Exception("Nothing wrong just testing..."));
        }

        public async void GetJobCallQueueSummary()
        {
            try
            {
                LoadDate = DateTime.Now;
                var cq = await LyncSvc.GetJobSummary();
                if (cq.Count > 0)
                {
                    JobCallSummaries = cq;
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

    }
}
