using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Lync.Model;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.areas.recruiter
{
    public partial class MyCallbacksView 
    {
        private readonly MyCallBacksViewModel _vm;
        private ToggleButton _selectedButton = new ToggleButton();

        public MyCallbacksView()
        {
            InitializeComponent();
            _vm = new MyCallBacksViewModel();
            DataContext = _vm;
            _vm.RefreshAll();
            AudioPlayer.Visibility = Visibility.Collapsed;
            Messenger.Default.Register<NotificationMessage>(this, HandleNotification);
            //AudioPlayer.AudioPlayback.MediaEnded += ResetPlaybackState;
            //SizeChanged += HandleResizeGrid;
        }

        private void HandleNotification(NotificationMessage message)
        {
            if (message.Notification == Notifications.PauseRefresh)
            {
                tbCanRefresh.Text = "Can Refresh";
            }
            if (message.Notification == Notifications.ResumeRefresh)
            {
                tbCanRefresh.Text = "Refresh Paused";
            }
        }


        //}
        private void HandleResizeGrid(object sender, SizeChangedEventArgs e)
        {
            ResizeGrid();
        }

        public Boolean CanRefresh
        {
            get
            {
                return _vm.CanRefresh && _vm.CanCall; 
            }
        }

        // Using a DependencyProperty as the backing store for CanRefresh.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanRefreshProperty =
            DependencyProperty.Register("CanRefresh", typeof(Boolean), typeof(MyCallbacksView), new PropertyMetadata(false));



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
            DependencyProperty.Register("SelectedJobNum", typeof(string), typeof(MyCallbacksView), new PropertyMetadata(""));


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

        private void callbackGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            if (_vm.SelectedMyCallback != null)
                callbackGrid.SelectedItem = _vm.SelectedMyCallback;
            else
                callbackGrid.SelectedIndex = 0;
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
    }
}