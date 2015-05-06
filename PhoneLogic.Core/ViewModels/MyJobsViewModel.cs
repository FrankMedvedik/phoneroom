using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Lync.Model;
using Newtonsoft.Json;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;


namespace PhoneLogic.Core.ViewModels
{
    public class MyJobsViewModel : CollectionViewModelBase
    {
        public MyJobsViewModel()
        {
            RefreshAll();
            StartAutoRefresh(ApiRefreshFrequency.UserDB);
        }
    
        public async void GetMyJobs()
        {
            PhoneLogicTask s = null;
            if(SelectedPhoneLogicTask != null)
                s = SelectedPhoneLogicTask;

            LoadDate = DateTime.Now;
            try
            {
                var mcb = await PhoneLogicTaskSvc.GetMyJobs(LyncClient.GetClient().Self.Contact.Uri);
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
                }
            }
        }
        #endregion
    }
}

