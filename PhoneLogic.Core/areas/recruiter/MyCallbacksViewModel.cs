using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Lync.Internal.Utilities.Interop;
using Microsoft.Lync.Model;
using Newtonsoft.Json;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;

namespace PhoneLogic.Core.ViewModels
    
{
    public class MyCallBacksViewModel : CollectionViewModelBase
    {
     
        public MyCallBacksViewModel()
        {
          StartAutoRefresh(ApiRefreshFrequency.UserDB);
          GetMyCallbacks();
        }

        private TimeSpan _callDuration;
        public TimeSpan CallDuration
        {
            get { return _callDuration; }
            set
            {
                _callDuration = value;
                NotifyPropertyChanged();
            }
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
            if (CanRefresh)
            {
                GetMyCallbacks();
            }

        }

        #region myCallbacks

        public void FilterCallbacks()
        {

            if ((String.IsNullOrWhiteSpace(SelectedJobNum) ) || (SelectedJobNum == "All"))
                FilteredCallbacks = MyCallbacks;
            else
            {
                var fr = (from c in MyCallbacks
                          where c.jobNum == SelectedJobNum
                          select c);
                
                FilteredCallbacks  = new ObservableCollection<myCallback>(fr);
                if (fr.Any()) 
                    ShowGridData = true;
                else
                    ShowGridData = false;
            }
            
        }

        
        public async void GetMyCallbacks()
        {
            myCallback s = SelectedMyCallback;
            LoadDate = DateTime.Now; 
            try{
                var mcb = new ObservableCollection<myCallback>(await CallbackSvc.GetMyCallbacks(LyncClient.GetClient().Self.Contact.Uri));
                MyCallbacks = mcb;
                FilterCallbacks();
                if (FilteredCallbacks.Count > 0)
                {
                    if(s != null) 
                        SelectedMyCallback = FilteredCallbacks.FirstOrDefault(x => x.callbackID == s.callbackID);
                }
                else
                LoadedOk = true;
            }
            catch (Exception e)
            {
                LoadFailed(e);

            }
        }

        private ObservableCollection<myCallback> _filteredCallbacks;
        public ObservableCollection<myCallback> FilteredCallbacks
        {
            get { return _filteredCallbacks; }
            set
            {
                _filteredCallbacks = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("JobHeadingText");
            }
        }

        private ObservableCollection<myCallback> _myCallbacks;
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

        private string _selectedJobNum; // = "All";
        public string SelectedJobNum
        {
            get { return _selectedJobNum; }
            set
            {
                if (_selectedJobNum != value)
                {
                    _selectedJobNum = value;
                    NotifyPropertyChanged();
                    FilterCallbacks();
                }
            } }

        public string JobHeadingText
        {
            get
            {
                if (String.IsNullOrEmpty(SelectedJobNum)) return "";
                else
                    return String.Format("Job {0}-{1} has {2} voice mail messages", 
                        SelectedJobNum.Substring(0, 4),
                        SelectedJobNum.Substring(4),
                        FilteredCallbacks.Count);
            }
        }

        private Boolean _canCall = true;

        public Boolean CanCall
        {
            get
            {
                return _canCall;
            }
            set
            {
                _canCall = value;
                NotifyPropertyChanged();
            }
        }


        private String _actionMsg ;

        public String ActionMsg
        {
            get { return _actionMsg; }
            set
            {
                _actionMsg = value;
                NotifyPropertyChanged();
            }
        }


        public async void UpdateCallBack(int callbackId)
        {
            await CallbackSvc.StartCall(
                new CallbackDto()
                {
                    callbackID = callbackId
                });
        }
    }
}
