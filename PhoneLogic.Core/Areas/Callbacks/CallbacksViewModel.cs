using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Areas.DialHistory;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.Callbacks
    
{
    public class CallBacksViewModel : CollectionViewModelBase
    {
     
        public CallBacksViewModel()
        {
            
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
        public DateTime? LastCallBackStartTime
        {
            get
            {
                DateTime? dt = null;
                if (MyCallbacks != null)
                {
                    try
                    {
                        dt = MyCallbacks.Select(x => x.timeEntered).Max();
                    }
                    catch (InvalidOperationException ex)
                    {
                        // do nothing 
                        dt = null;
                    }
                }
                return dt;
            }
        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
                if (OpenCallbacksOnly)
                    GetOpenCallbacks();
                else
                    GetClosedCallbacks();
        }


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
                ShowAudioPlayer = false;
                _selectedMyCallback = value;
                NotifyPropertyChanged();
                if (SelectedMyCallback != null) ShowAudioPlayer = true;
                //Messenger.Default.Send(new NotificationMessage<DialHistoryMessage>(
                //    new DialHistoryMessage
                //    {
                //        PhoneNumber = SelectedMyCallback.callbackNum,
                //        StartDate = SelectedMyCallback.timeEntered,
                //        EndDate = DateTime.Now
                //    }, Notifications.PhoneNumberChanged));
            }
        }

        public bool ShowAudioPlayer
        {
            get { return _showAudioPlayer; }
            set { _showAudioPlayer = value; NotifyPropertyChanged(); }
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

        public bool OpenCallbacksOnly { get; set; }


        public async void UpdateCallBack(int callbackId)
        {
            await CallbackSvc.StartCall(
                new CallbackDto()
                {
                    callbackID = callbackId
                });
        }

        public ReportDateRange ReportDateRange = new ReportDateRange();
        private bool _showAudioPlayer;

        public async void GetOpenCallbacks()
        {
            ShowGridData = false;
            HeadingText = "";
            try
            {
                if (SelectedJobNum != null)
                {
                    HeadingText = "Loading...";
                    var ro = await OpenCallbackSvc.GetOpenJobCallbacks(SelectedJobNum, SelectedTaskId.ToString());
                    ShowGridData = true;
                    MyCallbacks = new ObservableCollection<myCallback>(ro.OrderByDescending(x => x.timeEntered));
                    HeadingText = String.Format("Job {0} has {1} Voice Mail Messages", StringFormatSvc.JobAndTaskFormatted(SelectedJobNum), MyCallbacks.Count());
                    LoadedOk = true;
                }
                
            }
            catch (Exception e)
            {
                LoadFailed(e);
            }
        }
        public async void GetClosedCallbacks()
        {
            ShowGridData = false;
            HeadingText = "";
            try
            {
                if ((ReportDateRange != null) && (SelectedJobNum != null))
                {
                    HeadingText = "Loading...";
                    var ro = await ClosedCallbackSvc.GetClosedJobCallbacks(SelectedJobNum, SelectedTaskId.ToString(),
                                ReportDateRange.StartRptDate, ReportDateRange.EndRptDate);
                    ShowGridData = true;
                    MyCallbacks = new ObservableCollection<myCallback>(ro.OrderByDescending(x => x.timeEntered));
                    HeadingText = String.Format("Job {0} has {1} Voice Mail Messages", StringFormatSvc.JobAndTaskFormatted(SelectedJobNum), MyCallbacks.Count());
                    LoadedOk = true;
                }

            }
            catch (Exception e)
            {
                LoadFailed(e);
            }
        }


    }
}
