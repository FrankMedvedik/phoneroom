using System.Linq;
using Microsoft.Lync.Model;
using PhoneLogic.Core.MVVM_Base_Types;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;
using System.Collections.ObjectModel;
using System;


namespace PhoneLogic.Core.ViewModels
{
    public class MyJobsViewModel : CollectionViewModelBase
    {

        public MyJobsViewModel()
        {
            StartAutoRefresh(ApiRefreshFrequency.UserDB);
        }
    
        public async void GetMyJobs()
        {
            PhoneLogicTask s = SelectedPhoneLogicTask;

            LoadDate = DateTime.Now;
            try
            {
                var mcb = await PhoneLogicTaskSvc.GetMyJobs(LyncClient.GetClient().Self.Contact.Uri);
                PhoneLogicTasks = new ObservableCollection<PhoneLogicTask>(mcb);
                if (PhoneLogicTasks.Count > 0)
                {
                    if (s.JobNum != null)
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

        private PhoneLogicTask _selectedPhoneLogicTask = new PhoneLogicTask();
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
