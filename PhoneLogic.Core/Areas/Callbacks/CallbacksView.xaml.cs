using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Lync.Model;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.Callbacks
{
    public partial class CallbacksView 
    {
        private readonly CallBacksViewModel _vm;

        public CallbacksView()
        {
            InitializeComponent();
            _vm = new CallBacksViewModel();
            DataContext = _vm;
            _vm.RefreshAll();
            AudioPlayer.Visibility = Visibility.Collapsed;
            dhv.Visibility = Visibility.Collapsed;
            dhv.AllCalls = false;
        }

        public string SelectedJobNum 
        {
            get { return _vm.SelectedJobNum; }
            set
            {
                SetValue(SelectedJobNumProperty, value);
                _vm.SelectedJobNum = value;
                AudioPlayer.Visibility = Visibility.Collapsed;
            }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedJobNumProperty =
            DependencyProperty.Register("SelectedJobNum", typeof(string), typeof(CallbacksView), new PropertyMetadata(""));

        public DateTime? LastCallBackStartTime
        {
            get { return _vm.LastCallBackStartTime; }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastCallBackStartTimeProperty =
            DependencyProperty.Register("LastCallBackStartTime", typeof(DateTime?), typeof(CallbacksView), new PropertyMetadata(DateTime.Today));


        public int SelectedTaskId
        {
            get { return _vm.SelectedTaskId; }
            set
            {
                //SetValue(SelectedTaskIdProperty, value);
                _vm.SelectedTaskId = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTaskIdProperty =
            DependencyProperty.Register("SelectedTaskId", typeof(int), typeof(CallbacksView), new PropertyMetadata(0));


        public ReportDateRange ReportDateRange
        {
            get { return _vm.ReportDateRange; }
            set
            {
                SetValue(ReportDateRangeProperty, value);
                _vm.ReportDateRange = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReportDateRangeProperty =
            DependencyProperty.Register("ReportDateRange", typeof(ReportDateRange), typeof(CallbacksView), new PropertyMetadata(new ReportDateRange()));

        public void Refresh()
        {
            dhv.Visibility = Visibility.Collapsed;
            _vm.RefreshAll();
        }
        public bool ShowData
        {
            get { return _vm.ShowGridData; }
            set
            {
                _vm.ShowGridData = value;
                AudioPlayer.Visibility = System.Windows.Visibility.Collapsed;
                _vm.HeadingText = "";

            }
        }
        private async void CloseCallback_Click(object sender, RoutedEventArgs e)
        {
            // set the status of the callback to closed
            _vm.CanRefresh = false;

            if (_vm.SelectedMyCallback == null) return;
            MessageBoxResult result = MessageBox.Show("Delete this Message?", "Confirm", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                var cb = new CallbackDto
                {
                    SIP = LyncClient.GetClient().Self.Contact.Uri,
                    callbackID = _vm.SelectedMyCallback.callbackID
                };

                await CallbackSvc.Close(cb);
                _vm.CanRefresh = true;
                _vm.RefreshAll();
            }
        }

        private async void Call_Click(object sender, RoutedEventArgs e)
        {
            if (LyncClient.GetClient().State != ClientState.SignedIn)
            {
                MessageBox.Show("Please Sign in to Lync first ");
                return;
            }

            if (!String.IsNullOrWhiteSpace(_vm.SelectedMyCallback.status))  
            {
                if (MessageBox.Show("Call may already be in progress, call anyway?", "Confirm",
                    MessageBoxButton.OKCancel) != MessageBoxResult.OK) 
                    return;
            }

            _vm.CanCall = false;
            _vm.ActionMsg = "Your call has been placed";
            StartTimer();

            String job = _vm.SelectedMyCallback.jobNum + ":0" + _vm.SelectedMyCallback.taskID;
            await LyncSvc.RecruiterDialOut(job, _vm.SelectedMyCallback.phoneNum, _vm.SelectedMyCallback.callbackID);
            _vm.UpdateCallBack(_vm.SelectedMyCallback.callbackID);
        }

        private void SetupAudioPlayback()
        {
            AudioPlayer.Visibility = Visibility.Collapsed;
            if (_vm.SelectedMyCallback != null)
            {
                AudioPlayer.Url = ConditionalConfiguration.rootUrl + "ClientBin/messages/" +
                                  _vm.SelectedMyCallback.msgScr;
                AudioPlayer.Reset();
                AudioPlayer.Title = String.Format("{0} - {1}", _vm.SelectedMyCallback.JobFormatted,
                    _vm.SelectedMyCallback.phoneFormatted);
                AudioPlayer.Visibility = Visibility.Visible;
            }


        }

        private void callbackGrid_Loaded(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Visibility = Visibility.Collapsed;
        }

        // inactivate call button
        public void StartTimer()
        {
            System.Windows.Threading.DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, UserInterfaceTimings.OutboundCallButtonInactivateTime, 0); 
            myDispatcherTimer.Tick += new EventHandler(Each_Tick);
            myDispatcherTimer.Start();
        }

        // A variable to count with.
        int i = 0;


        public bool OpenCallbacksOnly   
        {
            get { return _vm.OpenCallbacksOnly; }
            set { _vm.OpenCallbacksOnly = value;  }
        }

        // Using a DependencyProperty as the backing store for OpenCallbacksOnlyMyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenCallbacksOnlyProperty =
            DependencyProperty.Register("OpenCallbacksOnly", typeof(bool), typeof(CallbacksView), new PropertyMetadata(false));




        public void Each_Tick(object o, EventArgs sender)
        {
            _vm.CanCall = true;
            _vm.ActionMsg = "";
           
        }

        public void ResizeGrid()
        {
            
            //tbGridSize.Text = String.Format("Width : {0}  Height : {1} ", BrowserInfoSvc.ClientWidth,
            //    BrowserInfoSvc.ClientHeight);
            if (BrowserInfoSvc.ClientWidth < UserInterfaceTimings.ResizeBoundryWidth)
            {
                callbackGrid.Columns[2].Visibility = Visibility.Collapsed;
                callbackGrid.Columns[3].Visibility = Visibility.Collapsed;
                //callbackGrid.Columns[4].Visibility = Visibility.Collapsed;
            }
            if (BrowserInfoSvc.ClientWidth >= UserInterfaceTimings.ResizeBoundryWidth)
            {
                callbackGrid.Columns[2].Visibility = Visibility.Visible;
                callbackGrid.Columns[3].Visibility = Visibility.Visible;
                //callbackGrid.Columns[4].Visibility = Visibility.Visible;
            }

        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                callbackGrid.Export("MyCallbacks");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file - " + ex.Message);
            }

        }
        private void CallbackDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetupAudioPlayback();

            if (_vm.SelectedMyCallback != null)
            {
                
                dhv.PhoneNumber = _vm.SelectedMyCallback.phoneNum;
                dhv.StartDate = _vm.SelectedMyCallback.timeEntered;
                dhv.EndDate = DateTime.Now;
                dhv.Refresh();
                dhv.Visibility = Visibility.Visible;
            }
            else
                dhv.Visibility = Visibility.Collapsed;
        }

    }
}