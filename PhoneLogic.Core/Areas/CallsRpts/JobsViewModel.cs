using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.CallRpt.Model;
using PhoneLogic.Core.areas.CallsRpts.Models;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.areas.CallsRpts
{
    public class JobsViewModel : CollectionViewModelBase
    {
        public JobsViewModel()
        {
            Messenger.Default.Register<NotificationMessage<CallRptDateRange>>(this, message =>
            {
                if (message.Notification == Notifications.JobCallRptDateRangeChanged)
                {
                    CallRptDateRange = message.Content;
                    RefreshAll(null,null);
                }
            });
        }
        private bool _canRefresh = true;
        public Boolean CanRefresh       
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables
        public CallRptDateRange CallRptDateRange = new CallRptDateRange();
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
                if (value != null)
                {
                    Messenger.Default.Send(new NotificationMessage<string>(this, SelectedJob.JobNumber,Notifications.CallRptJobNumSet));
                    Messenger.Default.Send(new NotificationMessage(this, Notifications.CallRptRecruiterCleared));
                }
                else
                {
                    Messenger.Default.Send(new NotificationMessage(this, Notifications.CallRptJobNumCleared));
                    Messenger.Default.Send(new NotificationMessage(this, Notifications.CallRptRecruiterCleared));
                }
            }
        }

        public async void GetJobs()
        {
            HeadingText = "Loading...";
            ShowGridData = false;
            try
            {
                var ro = await LyncCallLogSvc.GetLynCallsByJob(CallRptDateRange.StartRptDate, CallRptDateRange.EndRptDate);
                if (ro.Count > 0)
                {
                    ShowGridData = true;
                    Jobs = new ObservableCollection<ByJob>(ro);
                    HeadingText = string.Format("{0} Jobs", Jobs.Count());
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