using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
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
            if(CanRefresh)
                GetMyCallbacks();

        }

        #region myCallbacks

        public void FilterCallbacks()
        {

            //if ((SelectedJobNum == null) || (SelectedJobNum == "All"))
                FilteredCallbacks = MyCallbacks;
            //else
            //{
            //    var fr = (from c in MyCallbacks.to
            //              where c.jobNum == SelectedJobNum
            //        select c);
            //    var oc = new ObservableCollection<myCallback>(fr);
            //    FilteredCallbacks = oc;
            //}
            GroupDataByColumnName("jobFormatted");
        }

        public void GroupDataByColumnName(string groupName)
        {
            FilteredCallbacks.GroupDescriptions.Clear();
            FilteredCallbacks.GroupDescriptions.Add(new PropertyGroupDescription(groupName));
        }

        public async void GetMyCallbacks()
        {
            myCallback s = SelectedMyCallback;

            LoadDate = DateTime.Now; 
            try{
                var mcb  = new PagedCollectionView(await CallbackSvc.GetMyCallbacks(LyncClient.GetClient().Self.Contact.Uri));
                MyCallbacks = mcb;
                FilterCallbacks();
                if (FilteredCallbacks.Count > 0)
                {
                    ShowGridData = true;
                    //if(s != null)
                    //    SelectedMyCallback = FilteredCallbacks..FirstOrDefault(x => x.callbackID == s.callbackID);
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

        private PagedCollectionView _filteredCallbacks;
        public PagedCollectionView FilteredCallbacks
        {
            get { return _filteredCallbacks; }
            set
            {
                _filteredCallbacks = value;
                NotifyPropertyChanged();
            }
        }

        private PagedCollectionView _myCallbacks;
        public PagedCollectionView MyCallbacks
        {
            get { return _myCallbacks; }
            set
            {
                _myCallbacks = value;
                NotifyPropertyChanged();
            }
        }
        //private PagedCollectionView<myCallback> _filteredCallbacks = new PagedCollectionView<myCallback>();
        //public PagedCollectionView<myCallback> FilteredCallbacks
        //{
        //    get { return _filteredCallbacks; }
        //    set
        //    {
        //        _filteredCallbacks = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //private PagedCollectionView<myCallback> _myCallbacks = new PagedCollectionView<myCallback>();
        //public PagedCollectionView<myCallback> MyCallbacks
        //{
        //    get { return _myCallbacks; }
        //    set
        //    {
        //        _myCallbacks = value;
        //        NotifyPropertyChanged();
        //    }
        //}

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

        private string _selectedJobNum = "All";
        public string SelectedJobNum
        {
            get { return _selectedJobNum; }
            set
            {
                if (_selectedJobNum != value)
                {
                    _selectedJobNum = value;
//                    NotifyPropertyChanged();
                    FilterCallbacks();
                }
            } }

    }
}
