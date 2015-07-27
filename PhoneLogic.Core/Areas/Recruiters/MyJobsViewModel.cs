using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Lync.Model;
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
            if(SelectedPhoneLogicTask != null)
                s = SelectedPhoneLogicTask;

            LoadDate = DateTime.Now;
            try
            {
                var sip = LyncClient.GetClient().Self.Contact.Uri;
                var mcb = await PhoneLogicTaskSvc.GetMyJobs(sip);
                PhoneLogicTasks = new ObservableCollection<PhoneLogicTask>(mcb);
                if (PhoneLogicTasks.Count > 0)
                {
                    if (s != null)
                         SelectedPhoneLogicTask = PhoneLogicTasks.FirstOrDefault(x => x.JobNum == s.JobNum);
                    ShowGridData = true;
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

        protected override void RefreshAll(object sender, EventArgs e)
        {
            if(CanRefresh)
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
                }
            }
        }
        #endregion
    }
}

