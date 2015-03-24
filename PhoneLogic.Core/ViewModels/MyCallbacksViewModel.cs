using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Lync.Model;
using Newtonsoft.Json;
using PhoneLogic.Core.MVVM_Base_Types;
using PhoneLogic.Model;
using PhoneLogic.Core.Services;

namespace PhoneLogic.Core.ViewModels
    
{
    public class MyCallBacksViewModel : CollectionViewModelBase
    {
        public DebugEventsViewModel de;

        public MyCallBacksViewModel()
        {
          StartAutoRefresh(ApiRefreshFrequency.UserDB);
        }

        private Boolean _canRefresh = true;

        public Boolean CanRefresh{
            get { return _canRefresh; }
            set
            {
                _canRefresh = value;
                NotifyPropertyChanged();
            }
        }
        protected override void RefreshAll(object sender, EventArgs e)
        {
            if(CanRefresh)
                GetMyCallbacks();
        }

        #region myCallbacks
        
        public async void GetMyCallbacks()
        {
            myCallback s = null; 
            if (SelectedMyCallback != null)
            {
                s = SelectedMyCallback;

            }

            LoadDate = DateTime.Now; 
            try{
                var mcb = await CallbackSvc.GetMyCallbacks(LyncClient.GetClient().Self.Contact.Uri);
                MyCallbacks.Clear();
                if (mcb.Count > 0)
                {
                    MyCallbacks = new ObservableCollection<myCallback>(mcb);
                    ShowGridData = true;
                    if(s != null) 
                        SelectedMyCallback = (myCallback) MyCallbacks.FirstOrDefault(x => x.callbackID == s.callbackID);
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
     
        private ObservableCollection<myCallback> _myCallbacks = new ObservableCollection<myCallback>();
        public ObservableCollection<myCallback> MyCallbacks
        {
            get { return _myCallbacks; }
            set
            {
                _myCallbacks = value;
                NotifyPropertyChanged();
            }
        }
        #endregion


        #region SelectedCallback

        private myCallback _selectedMyCallback;
        public myCallback SelectedMyCallback
        {
            get { return _selectedMyCallback; }
            set
            {
                _selectedMyCallback = value;
                NotifyPropertyChanged();
            }
        }

        public string SelectedAppData
        {
            get
            {
                if (SelectedMyCallback != null)
                {
                    var plc = new PhoneLogicContext()
                    {
                        callerId = SelectedMyCallback.phoneNum,
                        conversationId = "",
                        dialedNumber = SelectedMyCallback.tollFreeNumber,
                        jobNumber = SelectedMyCallback.jobNum,
                        taskId = SelectedMyCallback.taskID,
                        timeReceived = DateTime.Now.ToLongDateString(),
                        callbackId = SelectedMyCallback.callbackID
                    };
                    return JsonConvert.SerializeObject(plc);
                }
                return "";
            }
        }

        #endregion
    }
}
