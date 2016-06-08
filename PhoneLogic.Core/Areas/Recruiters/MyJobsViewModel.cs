using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Lync.Model;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.Recruiters
{
    public class MyJobsViewModel : CollectionViewModelBase
    {
        public MyJobsViewModel()
        {
            RefreshAll();
            StartAutoRefresh(ApiRefreshFrequency.UserDB);
            Messenger.Default.Register<string>(this, HandleNotification);
        }

        private string headingText;

        public string HeadingText
        {
            get { return headingText; }
            set
            {
                headingText = value;
                NotifyPropertyChanged();
            }
        }

        private void HandleNotification(string message)
        {
            if (message == Notifications.AudioPlaybackStarted)
            {
                CanRefresh = false;
            }
            if (message == Notifications.AudioPlaybackEnded)
            {
                CanRefresh = true;
            }
        }

        public async void GetMyJobs()
        {
            PhoneLogicTask s = null;
            if (SelectedPhoneLogicTask != null)
                s = SelectedPhoneLogicTask;

            LoadDate = DateTime.Now;
            try
            {
                var sip = LyncClient.GetClient().Self.Contact.Uri;
                var mcb = await PhoneLogicTaskSvc.GetMyJobs(sip);
                PhoneLogicTasks = new ObservableCollection<PhoneLogicTask>(mcb);
                if (PhoneLogicTasks.Count > 0)
                {
                    HeadingText =
                        String.Format("There are {0} jobs that have {1} voice mail messages and {2} calls today",
                            PhoneLogicTasks.Count, PhoneLogicTasks.Sum(x => x.MsgCnt),
                            PhoneLogicTasks.Sum(x => x.CallCnt));
                    if (s != null)
                    {
                        SelectedPhoneLogicTask =
                            PhoneLogicTasks.FirstOrDefault(x => x.JobNum == s.JobNum && x.TaskID == s.TaskID);
                    }
                    ShowGridData = true;
                }
                else
                {
                    ShowGridData = false;
                    HeadingText = "No jobs found";
                }
                LoadedOk = true;
            }
            catch (Exception e)
            {
                HeadingText = "Load Error";
                LoadFailed(e);
            }
        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            if (CanRefresh)
                GetMyJobs();
        }

        #region MyJobs

        private ObservableCollection<PhoneLogicTask> _phoneLogicTask = new ObservableCollection<PhoneLogicTask>();

        public ObservableCollection<PhoneLogicTask> PhoneLogicTasks
        {
            get { return _phoneLogicTask; }
            set
            {
                _phoneLogicTask = value;
                NotifyPropertyChanged();
            }
        }

        private Boolean _canRefresh = true;

        public Boolean CanRefresh
        {
            get { return _canRefresh; }
            set
            {
                _canRefresh = value;
                NotifyPropertyChanged();
            }
        }

        private PhoneLogicTask _selectedPhoneLogicTask;

        public PhoneLogicTask SelectedPhoneLogicTask
        {
            get { return _selectedPhoneLogicTask; }
            set
            {
                if (_selectedPhoneLogicTask != value)
                {
                    _selectedPhoneLogicTask = value;
                    NotifyPropertyChanged();
                    CanRefresh = true;
                    Messenger.Default.Send(new NotificationMessage<PhoneLogicTask>(SelectedPhoneLogicTask,
                        Notifications.MySelectedPhoneLogicTaskChanged));
                }
            }
        }

        #endregion
    }
}