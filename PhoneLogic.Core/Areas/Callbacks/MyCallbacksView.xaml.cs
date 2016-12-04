﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Lync.Model;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.Callbacks
{
    public partial class MyCallbacksView
    {
        public static readonly DependencyProperty SelectedJobNumProperty =
            DependencyProperty.Register("SelectedJobNum", typeof(string), typeof(MyCallbacksView),
                new PropertyMetadata(""));

        public static readonly DependencyProperty LastCallBackStartTimeProperty =
            DependencyProperty.Register("LastCallBackStartTime", typeof(DateTime?), typeof(MyCallbacksView),
                new PropertyMetadata(DateTime.Today));

        public static readonly DependencyProperty SelectedTaskIdProperty =
            DependencyProperty.Register("SelectedTaskId", typeof(int), typeof(MyCallbacksView), new PropertyMetadata(0));

        public static readonly DependencyProperty ReportDateRangeProperty =
            DependencyProperty.Register("ReportDateRange", typeof(ReportDateRange), typeof(MyCallbacksView),
                new PropertyMetadata(new ReportDateRange()));

        private readonly MyCallBacksViewModel _vm;
        private int i = 0;

        public MyCallbacksView()
        {
            InitializeComponent();
            _vm = new MyCallBacksViewModel();
            DataContext = _vm;
            _vm.RefreshAll();
            AudioPlayer.Visibility = Visibility.Collapsed;
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

        public DateTime? LastCallBackStartTime
        {
            get { return _vm.LastCallBackStartTime; }
        }


        public int SelectedTaskId
        {
            get { return _vm.SelectedTaskId; }
            set
            {
                _vm.SelectedTaskId = value;
            }
        }


        public ReportDateRange ReportDateRange
        {
            get { return _vm.ReportDateRange; }
            set
            {
                SetValue(ReportDateRangeProperty, value);
                _vm.ReportDateRange = value;
            }
        }

        public bool ShowData
        {
            get { return _vm.ShowGridData; }
            set
            {
                _vm.ShowGridData = value;
                AudioPlayer.Visibility = Visibility.Collapsed;
                _vm.HeadingText = "";
            }
        }

        public void Refresh()
        {
            _vm.RefreshAll();
        }

        private async void CloseCallback_Click(object sender, RoutedEventArgs e)
        {
            // set the status of the callback to closed
            _vm.CanRefresh = false;

            if (_vm.SelectedMyCallback == null) return;
            var result = MessageBox.Show("Delete this Message?", "Confirm", MessageBoxButton.OKCancel);

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

            if (!string.IsNullOrWhiteSpace(_vm.SelectedMyCallback.status))
            {
                if (MessageBox.Show("Call may already be in progress, call anyway?", "Confirm",
                    MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                    return;
            }
            _vm.CanCall = false;
            _vm.ActionMsg = "Your call has been placed";
            StartTimer();
            var job = _vm.SelectedMyCallback.jobNum + ":0" + _vm.SelectedMyCallback.taskID;
            await LyncSvc.RecruiterDialOut(job, _vm.SelectedMyCallback.phoneNum, _vm.SelectedMyCallback.callbackID);
        }

        private void callbackGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AudioPlayer.Visibility = Visibility.Collapsed;
            if (_vm.SelectedMyCallback != null)
            {
                AudioPlayer.Url = ConditionalConfiguration.rootUrl + "ClientBin/messages/" +
                                  _vm.SelectedMyCallback.msgScr;
                AudioPlayer.Reset();
                AudioPlayer.Title = string.Format("{0} - {1}", _vm.SelectedMyCallback.JobFormatted,
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
            var myDispatcherTimer = new DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, UserInterfaceTimings.OutboundCallButtonInactivateTime, 0);
            myDispatcherTimer.Tick += Each_Tick;
            myDispatcherTimer.Start();
        }

        public void Each_Tick(object o, EventArgs sender)
        {
            _vm.CanCall = true;
            _vm.ActionMsg = "";
        }

        public void ResizeGrid()
        {
            if (BrowserInfoSvc.ClientWidth < UserInterfaceTimings.ResizeBoundryWidth)
            {
                callbackGrid.Columns[2].Visibility = Visibility.Collapsed;
                callbackGrid.Columns[3].Visibility = Visibility.Collapsed;
            }
            if (BrowserInfoSvc.ClientWidth >= UserInterfaceTimings.ResizeBoundryWidth)
            {
                callbackGrid.Columns[2].Visibility = Visibility.Visible;
                callbackGrid.Columns[3].Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        public void SetMyCallsbacksConfiguration()
        {
            Grid.SetRow(AudioPlayer, 1);
            Grid.SetColumn(AudioPlayer, 0);
        }
    }
}