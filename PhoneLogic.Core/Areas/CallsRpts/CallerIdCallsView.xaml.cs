using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.Services;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    /// <summary>
    /// Description for CallsView.
    /// </summary>
    public partial class CallerIdCallsView : UserControl
    {
        private CallerIdCallsViewModel _vm = null;

        public CallerIdCallsView()
        {
            InitializeComponent();
            _vm = new CallerIdCallsViewModel();
            DataContext = _vm;


        }
        public void Refresh()
        {
            _vm.RefreshAll();
        }

        public string SelectedCallerId
        {
            get { return _vm.SelectedCallerId; }
            set
            {
                SetValue(SelectedCallerIdProperty, value);
                _vm.SelectedCallerId = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedCallerIdProperty =
            DependencyProperty.Register("SelectedCallerId", typeof(string), typeof(CallerIdCallsView), new PropertyMetadata(""));

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        try
        {
            _vm.GetCalls();
            //CallsDG.Export();
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
                _vm.HeadingText = "";

            }
        }

        // Using a DependencyProperty as the backing store for boolean ShowData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDataProperty =
      DependencyProperty.Register("ShowData", typeof(bool), typeof(CallerIdCallsView), new PropertyMetadata(false));

        /* used to setup the view so it can be used by MyWork 
         * (default beahvior shows the contact card and puts playback control to the right 
        */
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