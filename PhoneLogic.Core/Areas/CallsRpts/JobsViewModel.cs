using System;
using System.Collections.ObjectModel;
using System.Linq;
using PhoneLogic.Core.areas.CallsRpts.Models;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.areas.CallsRpts
{
    public class JobsViewModel : CollectionViewModelBase
    {
        public JobsViewModel()
        {
            StartRptDate = DateTime.Now.AddDays(-5);
            EndRptDate = DateTime.Now;
            GetJobs();

        }
        private bool _canRefresh = true;
        public Boolean CanRefresh       
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables

        private DateTime _startRptDate = DateTime.Now.AddDays(-5);
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

        private string _selectedJobNumber;
        public string SelectedJobNumber
        {
            get { return _selectedJobNumber; }
            set
            {
                _selectedJobNumber = value;
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

        #region Filters



        #endregion

        protected override void RefreshAll(object sender, EventArgs e)
        {
            if (CanRefresh)
            {
                var s = SelectedJobNumber;
                GetJobs();
                if (s != null)
                {
                    SelectedJobNumber = Jobs.First(x => x.JobNumber == s).JobNumber;
                    GetJobs();
                }
            }
        }

        public async void GetJobs()
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

    }
}