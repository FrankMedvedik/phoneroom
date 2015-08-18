using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.CallRpt.Model;
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
        public JobsViewModel()
        {
           Messenger.Default.Register<NotificationMessage<GlobalReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.GlobalReportCriteriaChanged)
                {
                    if (ReportDateRange != message.Content.ReportDateRange)
                    {
                        ReportDateRange = message.Content.ReportDateRange;
                        Phoneroom = message.Content.Phoneroom;
                        PhoneroomJobs = message.Content.PhoneroomJobs;

                        RefreshAll(null,null);
                    }
                    else if (Phoneroom != message.Content.Phoneroom)
                    {
                        Phoneroom = message.Content.Phoneroom;
                        PhoneroomJobs = message.Content.PhoneroomJobs;
                    }
                }
            });

        }

        public List<PhoneLogicTask> PhoneroomJobs
        {
            get { return _phoneroomJobs; }
            set { _phoneroomJobs = value; NotifyPropertyChanged(); }
        }

        public string Phoneroom
        {
            get { return _phoneroom; }
            set { _phoneroom = value; NotifyPropertyChanged(); }
        }

        private bool _canRefresh = true;
        public Boolean CanRefresh       
        {
            get { return _canRefresh; }
            set { _canRefresh = value; NotifyPropertyChanged(); }
        }

        #region reporting variables
        public ReportDateRange ReportDateRange = new ReportDateRange();
        #endregion

        #region CallSummaries
        private ObservableCollection<ByJob> _jobs = new ObservableCollection<ByJob>();
        private ByJob _selectedJob;


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
                var s = SelectedJob;
                GetJobs();
                if (s != null)
                {
                    SelectedJob = Jobs.First(x => x.JobNumber == s.JobNumber);
                    GetJobs();
                }
            }
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
                ObservableCollection<ByJob> j = new ObservableCollection<ByJob>();

                var ro = await LyncCallLogSvc.GetLynCallsByJob(ReportDateRange.StartRptDate, ReportDateRange.EndRptDate);
                if (ro.Any())
                {
                    var rx = (from c in ro
                                join b in PhoneroomJobs on c.JobFormatted equals b.JobFormatted
                                select c).ToList().OrderByDescending(x => x.JobFormatted);
                    j = new ObservableCollection<ByJob>(rx);
                    ShowGridData = true;
                    Jobs = j;
                    HeadingText = string.Format("{0} Phone Room(s) has {1} Jobs", Phoneroom, Jobs.Count());
                }
                else
                {
                    HeadingText = string.Format("{0} Phone Room(s) has no calls", Phoneroom);
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