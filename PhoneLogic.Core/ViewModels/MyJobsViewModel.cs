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

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _outBoundPhoneNumber = "2679540467";
        public string OutBoundPhoneNumber
        {
            get { return _outBoundPhoneNumber; }
            set
            {
                if (_outBoundPhoneNumber != value)
                {
                    _outBoundPhoneNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public string SelectedAppData
        {
            get
            {
                if (SelectedPhoneLogicTask != null)
                {
                    var plc = new PhoneLogicContext()
                    {
                        callerId = PhoneNumber,
                        conversationId = "",
                        dialedNumber = OutBoundPhoneNumber,
                        jobNumber = SelectedPhoneLogicTask.JobNum,
                        //taskId = SelectedPhoneLogicTask..taskID,
                        //timeReceived = DateTime.Now.ToLongDateString(),
                        //callbackId = SelectedMyCallback.callbackID
                    };
                    return JsonConvert.SerializeObject(plc);
                }
                return "";
            }
        }
        private PhoneLogicTask _selectedPhoneLogicTask;
        public PhoneLogicTask SelectedPhoneLogicTask
        {
            get { return _selectedPhoneLogicTask; }
            set
            {
                OutboundViewVisible =  (value != null ) ? true: false;
                if (_selectedPhoneLogicTask != value)
                {
                    _selectedPhoneLogicTask = value;
                    
                    NotifyPropertyChanged();
                }
            }
        }
        private Boolean _outboundViewVisible = true;
        public Boolean OutboundViewVisible
        {
            get { return _outboundViewVisible; }
            set
            {
                _outboundViewVisible = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
    }
}

