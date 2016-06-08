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
using PhoneLogic.Model.Models;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.Callbacks
{
    public class OpenCallbacksJobsViewModel : CollectionViewModelBase
    {
        public OpenCallbacksJobsViewModel()
        {
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == Notifications.AutoRefreshNow)
                {
                    GetJobs();
                }
            });
            GetJobs();
        }

        #region reporting variables

        public List<PhoneLogicTask> PhoneroomJobs
        {
            get { return _phoneroomJobs; }
            set
            {
                _phoneroomJobs = value;
                NotifyPropertyChanged();
                FilterJobs();
            }
        }

        #endregion

        #region CallSummaries

        private CallbackRpt _selectedJob;
        private List<CallbackRpt> Jobs = new List<CallbackRpt>();


        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetJobs();
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

        public CallbackRpt SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                NotifyPropertyChanged();
            }
        }

        private string _phoneroom;
        private List<PhoneLogicTask> _phoneroomJobs;
        private ObservableCollection<CallbackRpt> _filteredJobs = new ObservableCollection<CallbackRpt>();

        public ObservableCollection<CallbackRpt> FilteredJobs
        {
            get { return _filteredJobs; }
            set
            {
                _filteredJobs = value;
                NotifyPropertyChanged();
            }
        }

        private void FilterJobs()
        {
            var rx = (from c in Jobs
                //join b in PhoneroomJobs on c.JobFormatted equals b.JobFormatted
                select c).ToList().OrderByDescending(x => x.CallbackCnt);
            ShowGridData = true;
            FilteredJobs = new ObservableCollection<CallbackRpt>(rx.ToList());
            HeadingText = string.Format("{0} Jobs with Open Callbacks as of {1}", FilteredJobs.Count(), DateTime.Now);
        }

        public async void GetJobs()
        {
            HeadingText = "Loading...";
            ShowGridData = false;

            try
            {
                var s = SelectedJob;
                Jobs = await OpenCallbackSvc.GetOpenCallbackRpt();
                LoadedOk = true;
                FilterJobs();
                if (s != null)
                {
                    SelectedJob = FilteredJobs.First(x => x.jobNum == s.jobNum);
                }
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