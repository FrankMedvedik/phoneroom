using System;
using System.Collections.ObjectModel;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;

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
        public  CallRptDateRange CallRptDateRange = new CallRptDateRange();
        #endregion

        private ObservableCollection<ByRecruitersForJob> _jobRecruiters = new ObservableCollection<ByRecruitersForJob>();
        
        private string _callRptJobNum;
        public string CallRptJobNum
        {
            get { return _callRptJobNum; }
            set { 
                _callRptJobNum = value; 
                  NotifyPropertyChanged();
            }
        }


        private string _headingText;
        public string HeadingText
        {
            get
            {
                return _headingText;
            }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }


        public ObservableCollection<ByRecruitersForJob> JobRecruiters
        {
            get
            {
                return _jobRecruiters;
            }
            set
            {
                _jobRecruiters = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("JobHeadingText");
            }
        }

        private ByRecruitersForJob  _selectedRecruiter;
        public ByRecruitersForJob SelectedRecruiter
        {
            get { return _selectedRecruiter; }
            set {
                _selectedRecruiter = value;
                NotifyPropertyChanged();
            }
        }

        
        protected override void RefreshAll (object sender, EventArgs e)
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
            if ((CallRptDateRange != null) && (_callRptJobNum != null))
            {
                HeadingText = "Loading...";
                try
                {
                    var ro = await LyncCallLogSvc.GetLynRecruitersForJob(_callRptJobNum, CallRptDateRange.StartRptDate,
                                CallRptDateRange.EndRptDate);
                        ShowGridData = true;
                        JobRecruiters = new ObservableCollection<ByRecruitersForJob>(ro);
                        HeadingText = String.Format("Job {0}-{1} has {2} Recruiters", CallRptJobNum.Substring(0, 4), CallRptJobNum.Substring(4, 4), JobRecruiters.Count);
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