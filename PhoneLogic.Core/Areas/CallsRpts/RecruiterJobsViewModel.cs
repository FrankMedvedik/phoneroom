using System;
using System.Collections.ObjectModel;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public class RecruiterJobsViewModel : CollectionViewModelBase
    {
        public RecruiterJobsViewModel()
        {
        }

        private bool _canRefresh = true;

        public Boolean CanRefresh
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables

        public ReportDateRange ReportDateRange = new ReportDateRange();
        public string RecruiterSIP;

        #endregion

        private ObservableCollection<ByRecruitersForJob> _recruiterJobs = new ObservableCollection<ByRecruitersForJob>();

        private ByRecruitersForJob _selectedJob;

        public ByRecruitersForJob SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                NotifyPropertyChanged();
            }
        }


        private string _headingText;

        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }


        public ObservableCollection<ByRecruitersForJob> RecruiterJobs
        {
            get { return _recruiterJobs; }
            set
            {
                _recruiterJobs = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("HeadingText");
            }
        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            if (CanRefresh)
            {
                GetRecruiterJobs();
            }
        }

        public async void GetRecruiterJobs()
        {
            ShowGridData = false;
            HeadingText = "";
            if ((ReportDateRange != null) && (RecruiterSIP != null))
            {
                HeadingText = "Loading...";
                try
                {
                    var ro = await LyncCallLogSvc.GetLynJobsForRecruiter(ReportDateRange.StartRptDate,
                        ReportDateRange.EndRptDate, RecruiterSIP);
                    ShowGridData = true;
                    RecruiterJobs = new ObservableCollection<ByRecruitersForJob>(ro);
                    HeadingText = String.Format("{0} Jobs with call activity between  {1} and {2}", RecruiterJobs.Count,
                        ReportDateRange.StartRptDate, ReportDateRange.EndRptDate);
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
}