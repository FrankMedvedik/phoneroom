using System;
using System.Collections.ObjectModel;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public class JobRecruitersViewModel : CollectionViewModelBase
    {
        public JobRecruitersViewModel()
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

        #endregion

        private ObservableCollection<ByRecruitersForJob> _jobRecruiters = new ObservableCollection<ByRecruitersForJob>();

        private string _callRptJobNum;

        public string CallRptJobNum
        {
            get { return _callRptJobNum; }
            set
            {
                _callRptJobNum = value;
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


        public ObservableCollection<ByRecruitersForJob> JobRecruiters
        {
            get { return _jobRecruiters; }
            set
            {
                _jobRecruiters = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("JobHeadingText");
            }
        }

        private ByRecruitersForJob _selectedRecruiter;

        public ByRecruitersForJob SelectedRecruiter
        {
            get { return _selectedRecruiter; }
            set
            {
                _selectedRecruiter = value;
                NotifyPropertyChanged();
            }
        }


        protected override void RefreshAll(object sender, EventArgs e)
        {
            if (CanRefresh)
            {
                GetRecruiters();
            }
        }

        public async void GetRecruiters()
        {
            ShowGridData = false;
            HeadingText = "";
            if ((ReportDateRange != null) && (_callRptJobNum != null))
            {
                HeadingText = "Loading...";
                try
                {
                    var ro = await LyncCallLogSvc.GetLynRecruitersForJob(_callRptJobNum, ReportDateRange.StartRptDate,
                        ReportDateRange.EndRptDate);
                    ShowGridData = true;
                    JobRecruiters = new ObservableCollection<ByRecruitersForJob>(ro);
                    HeadingText = String.Format("Job {0} has {1} Recruiters with call activity between {2} and {3}",
                        StringFormatSvc.JobAndTaskFormatted(CallRptJobNum), JobRecruiters.Count,
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