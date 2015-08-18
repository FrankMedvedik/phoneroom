using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Lync.Model;
using Newtonsoft.Json;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.Callbacks
    
{
    public class MyCallBacksViewModel : CollectionViewModelBase
    {
     
        public MyCallBacksViewModel()
        {
            
            //Messenger.Default.Register<NotificationMessage>(this, HandleNotification);
        }

       //private void HandleNotification(NotificationMessage message)
       // {
       //     if (message.Notification == Notifications.PauseRefresh)
       //     {
       //         CanRefresh = false;
       //     }
       //     if (message.Notification == Notifications.Refresh)
       //     {
       //         CanRefresh = true;
       //     }
       // }


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
        public DateTime? LastCallBackStartTime
        {
            get
            {
                DateTime? dt = null;
                try
                {
                    dt = _myCallbacks.Select(x => x.timeEntered).Max();
                }
                catch (InvalidOperationException ex)
                {
                    // do nothing 
                    dt = null;
                }

                return dt;

            }
        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
                if (MyCallbacksOnly)
                    GetMyCallbacks();
                else
                    GetAllCallBacks();
        }

        public bool MyCallbacksOnly { get; set; }

        #region myCallbacks
        
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

        private string _selectedJobNum = null;

        public string SelectedJobNum
        {
            get { return _selectedJobNum; }
            set
            {
                _selectedJobNum = value;
                NotifyPropertyChanged();

            }
        }
        private int _selectedTaskId;

        public int SelectedTaskId
        {
            get { return _selectedTaskId; }
            set
            {
                _selectedTaskId = value;
                NotifyPropertyChanged();
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

        public ReportDateRange ReportDateRange = new ReportDateRange();
        public async void GetAllCallBacks()
        {
            ShowGridData = false;
            HeadingText = "";
            try
            {
                if ((ReportDateRange != null) && (SelectedJobNum != null))
                {
                    HeadingText = "Loading...";
                    var ro = await CallbackSvc.GetJobCallbacks(SelectedJobNum, SelectedTaskId.ToString(),
                                ReportDateRange.StartRptDate, ReportDateRange.EndRptDate);
                    ShowGridData = true;
                    MyCallbacks = new ObservableCollection<myCallback>(ro);
                    HeadingText = String.Format("Job {0}-{1}  has {2} Voice Mail Messages", SelectedJobNum.Substring(0, 4),
                        SelectedJobNum.Substring(4, 4), MyCallbacks.Count());
                    LoadedOk = true;
                }
                
            }
            catch (Exception e)
            {
                LoadFailed(e);
            }
        }
        public async void GetMyCallbacks()
        {
            myCallback s = SelectedMyCallback;
            LoadDate = DateTime.Now;
            HeadingText = "Loading...";
            ShowGridData = false;
            try
            {
                var mcb = new ObservableCollection<myCallback>(await CallbackSvc.GetMyCallbacks(LyncClient.GetClient().Self.Contact.Uri, SelectedJobNum, SelectedTaskId.ToString()));
                MyCallbacks = mcb;
                if (MyCallbacks.Count > 0)
                {
                    ShowGridData = true;
                    HeadingText = String.Format("Job {0}-{1} has {2} voice mail messages", SelectedJobNum.Substring(0, 4), SelectedJobNum.Substring(4), MyCallbacks.Count);
                    if (s != null)
                        SelectedMyCallback = MyCallbacks.First(x => x.callbackID == s.callbackID);
                }
                else
                {
                    HeadingText = "No voice mail messages";
                }
                LoadedOk = true;
            }
            catch (Exception e)
            {
                LoadFailed(e);

            }
        }


    }
}
