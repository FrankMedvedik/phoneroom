using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public class JobsViewModel : CollectionViewModelBase
    {
        private bool _canRefresh = true;

        #region reporting variables

        public ReportDateRange ReportDateRange = new ReportDateRange();

        #endregion

        public JobsViewModel()
        {
            //StopAutoRefresh();
            Messenger.Default.Register<NotificationMessage<GlobalReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.GlobalReportCriteriaChanged)
                {
                    ReportDateRange = message.Content.ReportDateRange;
                    Phoneroom = message.Content.Phoneroom;
                    PhoneroomJobs = message.Content.PhoneroomJobs;
                    RefreshAll(null, null);
                }
            });
        }

        public List<PhoneLogicTask> PhoneroomJobs
        {
            get { return _phoneroomJobs; }
            set
            {
                _phoneroomJobs = value;
                NotifyPropertyChanged();
            }
        }

        public string Phoneroom
        {
            get { return _phoneroom; }
            set
            {
                _phoneroom = value;
                NotifyPropertyChanged();
            }
        }

        public bool CanRefresh
        {
            get { return _canRefresh; }
            set
            {
                _canRefresh = value;
                NotifyPropertyChanged();
            }
        }

        #region CallSummaries

        private ObservableCollection<ByJob> _jobs = new ObservableCollection<ByJob>();
        private ObservableCollection<ByJob> _filteredJobs = new ObservableCollection<ByJob>();
        private ByJob _selectedJob;


        public ObservableCollection<ByJob> Jobs
        {
            get { return _jobs; }
            set
            {
                _jobs = value;
                NotifyPropertyChanged();
                var s = SelectedJob;
                FilterJobs();
                if (s != null)
                {
                    SelectedJob = FilteredJobs.First(x => x.JobNumber == s.JobNumber);
                }
            }
        }

        public ObservableCollection<ByJob> FilteredJobs
        {
            get { return _filteredJobs; }
            set
            {
                _filteredJobs = value;
                NotifyPropertyChanged();
            }
        }

        #region Filters

        #endregion

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetJobs();
            FilterJobs();
        }

        private string _headingText;
        private string _phoneroom;
        private List<PhoneLogicTask> _phoneroomJobs;

        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }

        public ByJob SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                NotifyPropertyChanged();
            }
        }

        public async void GetJobs()
        {
            HeadingText = "Loading...";
            ShowGridData = false;
            try
            {
                var ro = await LyncCallLogSvc.GetLynCallsByJob(ReportDateRange.StartRptDate, ReportDateRange.EndRptDate);
                Jobs = new ObservableCollection<ByJob>(ro);
            }
            catch (Exception e)
            {
                LoadFailed(e);
                HeadingText = e.Message;
            }
        }

        public async void FilterJobs()
        {
            HeadingText = "Loading...";
            ShowGridData = false;
            try
            {
                if (Jobs.Any())
                {
                    var rx = (from c in Jobs
                        join b in PhoneroomJobs on c.JobFormatted equals b.JobFormatted
                        select c).ToList().OrderByDescending(x => x.JobFormatted);
                    FilteredJobs = new ObservableCollection<ByJob>(rx);
                    HeadingText =
                        string.Format("{0} Phone Room has {1} jobs with call activity between  {2} and {3} as of {4} ",
                            Phoneroom, Jobs.Count(), ReportDateRange.StartRptDate, ReportDateRange.EndRptDate,
                            DateTime.Now.ToShortTimeString());
                    ShowGridData = true;
                    SelectedJob = null; // if i am just changing the filtereing i want to loose the selected job
                }
                else
                {
                    HeadingText = string.Format(
                        "{0} Phone Room(s) has no call activity between  {1} and {2} as of {3}", Phoneroom,
                        ReportDateRange.StartRptDate, ReportDateRange.EndRptDate, DateTime.Now.ToShortTimeString());
                    SelectedJob = null;
                    ShowGridData = false;
                }
                LoadedOk = true;
            }
            catch (Exception e)
            {
                LoadFailed(e);
                HeadingText = e.Message;
            }
        }

        #endregion
    }
}