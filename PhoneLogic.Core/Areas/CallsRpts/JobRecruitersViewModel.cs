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
        private string _callRptJobNum;


        private string _headingText;

        private ObservableCollection<ByRecruitersForJob> _jobRecruiters = new ObservableCollection<ByRecruitersForJob>();

        private ByRecruitersForJob _selectedRecruiter;

        #region reporting variables

        public ReportDateRange ReportDateRange = new ReportDateRange();

        #endregion

        public bool CanRefresh { get; set; } = true;

        public string CallRptJobNum
        {
            get { return _callRptJobNum; }
            set
            {
                _callRptJobNum = value;
                NotifyPropertyChanged();
            }
        }

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
                    HeadingText = string.Format("Job {0} has {1} Recruiters with call activity between {2} and {3}",
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