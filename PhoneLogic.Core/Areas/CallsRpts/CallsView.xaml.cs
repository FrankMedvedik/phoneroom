﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Lync.Model;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.Services;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    /// <summary>
    ///     Description for CallsView.
    /// </summary>
    public partial class CallsView : UserControl
    {
        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecruiterSIPProperty =
            DependencyProperty.Register("RecruiterSIP", typeof(string), typeof(CallsView), new PropertyMetadata(""));

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedJobNumProperty =
            DependencyProperty.Register("SelectedJobNum", typeof(string), typeof(CallsView), new PropertyMetadata(""));

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastCallTimeProperty =
            DependencyProperty.Register("LastCallTime", typeof(DateTime?), typeof(CallsView),
                new PropertyMetadata(DateTime.Today));

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReportDateRangeProperty =
            DependencyProperty.Register("ReportDateRange", typeof(ReportDateRange), typeof(CallsView),
                new PropertyMetadata(new ReportDateRange()));

        // Using a DependencyProperty as the backing store for boolean ShowData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDataProperty =
            DependencyProperty.Register("ShowData", typeof(bool), typeof(CallsView), new PropertyMetadata(false));

        private readonly CallsViewModel _vm;

        // A variable to count with.
        private int i = 0;

        public CallsView()
        {
            InitializeComponent();
            _vm = new CallsViewModel();
            DataContext = _vm;
        }

        public string RecruiterSIP
        {
            get { return _vm.SelectedRecruiter; }
            set
            {
                SetValue(RecruiterSIPProperty, value);
                _vm.SelectedRecruiter = value;
                // set the contact card's sip
                cc.Source = value;
            }
        }


        public string SelectedJobNum
        {
            get { return _vm.SelectedJobNum; }
            set
            {
                SetValue(SelectedJobNumProperty, value);
                _vm.SelectedJobNum = value;
            }
        }


        public DateTime? LastCallTime
        {
            get { return _vm.LastCallStartTime; }
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

        private async void Call_Click(object sender, RoutedEventArgs e)
        {
            if (LyncClient.GetClient().State != ClientState.SignedIn)
            {
                MessageBox.Show("Please Sign in to Lync first ");
                return;
            }
            _vm.CanCall = false;
            _vm.ActionMsg = "Your call has been placed";
            StartTimer();
            await LyncSvc.RecruiterDialOut(_vm.SelectedCall.JobNumber, _vm.SelectedCall.CallerId, 0);
        }

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

        public void Refresh()
        {
            _vm.RefreshAll();
        }

        private void CallsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AudioPlayer.Visibility = Visibility.Collapsed;
            if (_vm.SelectedCall != null)
            {
                AudioPlayer.Url = ConditionalConfiguration.rootUrl + "ClientBin/LiveRecordings/" +
                                  _vm.SelectedCall.CallId + ".wma";
                AudioPlayer.Reset();
                AudioPlayer.Title = string.Format("{0} - {1}", _vm.SelectedCall.JobFormatted,
                    _vm.SelectedCall.PhoneFormatted);
                AudioPlayer.Visibility = Visibility.Visible;
            }
        }

        private void CallsDG_Loaded(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fname = string.Format("{0}", "Calls" + _vm.ReportDateRange.ToFormattedString('.'));
                CallsDG.Export(fname);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file - " + ex.Message);
            }
        }

        /* used to setup the view so it can be used by MyWork 
         * (default beahvior shows the contact card and puts playback control to the right 
        */

        public void SetMyCallsConfiguration()
        {
            cc.Visibility = Visibility.Collapsed;
        }

        public void ResizeGrid()
        {
            //tbGridSize.Text = String.Format("Width : {0}  Height : {1} ", BrowserInfoSvc.ClientWidth,
            //    BrowserInfoSvc.ClientHeight);
            if (BrowserInfoSvc.ClientWidth < UserInterfaceTimings.ResizeBoundryWidth)
            {
                CallsDG.Columns[3].Visibility = Visibility.Collapsed;
                CallsDG.Columns[4].Visibility = Visibility.Collapsed;
            }
            if (BrowserInfoSvc.ClientWidth >= UserInterfaceTimings.ResizeBoundryWidth)
            {
                CallsDG.Columns[3].Visibility = Visibility.Visible;
                CallsDG.Columns[4].Visibility = Visibility.Visible;
            }
        }
    }
}