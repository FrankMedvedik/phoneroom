using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Areas.CallsRpts;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.Services;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    /// <summary>
    /// Description for CallsView.
    /// </summary>
    public partial class CallsView : UserControl
    {
        private CallsViewModel _vm = null;

        public CallsView()
        {
            InitializeComponent();
            _vm = new CallsViewModel();
            DataContext = _vm;
        }

        public void Refresh()
        {
            _vm.RefreshAll();
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

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecruiterSIPProperty =
            DependencyProperty.Register("RecruiterSIP", typeof(string), typeof(CallsView), new PropertyMetadata(""));


        public string SelectedJobNum
        {
            get { return _vm.SelectedJobNum; }
            set
            {
                SetValue(SelectedJobNumProperty, value);
                _vm.SelectedJobNum = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedJobNumProperty =
            DependencyProperty.Register("SelectedJobNum", typeof(string), typeof(CallsView), new PropertyMetadata(""));


        public int RowCount
        {
            get { return _vm.Calls.Count; }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", typeof(int), typeof(CallsView), new PropertyMetadata(0));


        public CallRptDateRange CallRptDateRange
        {
            get { return _vm.CallRptDateRange; }
            set
            {
                SetValue(CallRptDateRangeProperty, value);
                _vm.CallRptDateRange = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CallRptDateRangeProperty =
            DependencyProperty.Register("CallRptDateRange", typeof(CallRptDateRange), typeof(CallsView), new PropertyMetadata(new CallRptDateRange()));

        private void CallsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AudioPlayer.Visibility = Visibility.Collapsed;
            if (_vm.SelectedCall != null)
            {
                AudioPlayer.Url = ConditionalConfiguration.rootUrl + "ClientBin/LiveRecordings/" +
                                  _vm.SelectedCall.CallId +".wma";
                AudioPlayer.Reset();
                AudioPlayer.Title = string.Format("{0} - {1}", _vm.SelectedCall.JobFormatted,
                    _vm.SelectedCall.PhoneFormatted);
                AudioPlayer.Visibility = Visibility.Visible;
            }


        }

        private void CallsDG_Loaded(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Visibility = Visibility.Collapsed;
            //if (_vm.SelectedCall != null)
            //    CallsDG.SelectedItem = _vm.SelectedCall;
            //else
            //    CallsDG.SelectedIndex = 0;
        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        try {
                CallsDG.Export();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file - " + ex.Message);
            }
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

        // Using a DependencyProperty as the backing store for boolean ShowData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDataProperty =
      DependencyProperty.Register("ShowData", typeof(bool), typeof(CallsView), new PropertyMetadata(false));

        /* used to setup the view so it can be used by MyWork 
         * (default beahvior shows the contact card and puts playback control to the right 
        */
        public void SetMyCallsConfiguration()
        {
            cc.Visibility = System.Windows.Visibility.Collapsed;
            Grid.SetRow(AudioPlayer, 0) ;
            Grid.SetColumn(AudioPlayer, 0);
        }
        public void ResizeGrid()
        {
            //tbGridSize.Text = String.Format("Width : {0}  Height : {1} ", BrowserInfoSvc.ClientWidth,
            //    BrowserInfoSvc.ClientHeight);
            if (BrowserInfoSvc.ClientWidth < UserInterfaceTimings.ResizeBoundryWidth)
            {
                CallsDG.Columns[3].Visibility = Visibility.Collapsed;
                CallsDG.Columns[4].Visibility = Visibility.Collapsed;
                CallsDG.Columns[5].Visibility = Visibility.Collapsed;
            }
            if (BrowserInfoSvc.ClientWidth >= UserInterfaceTimings.ResizeBoundryWidth)
            {
                CallsDG.Columns[3].Visibility = Visibility.Visible;
                CallsDG.Columns[4].Visibility = Visibility.Visible;
                CallsDG.Columns[5].Visibility = Visibility.Visible;
            }

        }

    }
}