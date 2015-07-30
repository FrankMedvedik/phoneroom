using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.Services;

namespace PhoneLogic.Core.Areas.Callbacks
{
    /// <summary>
    /// Description for CallsbacksView.
    /// </summary>
    public partial class oCallbacksView : UserControl
    {
        private CallbacksViewModel _vm = null;

        public oCallbacksView()
        {
            InitializeComponent();
            _vm = new CallbacksViewModel();
            DataContext = _vm;
        }

        public void Refresh()
        {
            _vm.RefreshAll();
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


        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedJobNumProperty =
            DependencyProperty.Register("SelectedJobNum", typeof(string), typeof(oCallbacksView), new PropertyMetadata(""));
        


        public int SelectedTaskId
        {
            get { return _vm.SelectedTaskId; }
            set
            {
                SetValue(SelectedTaskIdProperty, value);
                _vm.SelectedTaskId = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedTaskIdProperty =
            DependencyProperty.Register("SelectedTaskId", typeof(int), typeof(oCallbacksView), new PropertyMetadata(0));


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
            DependencyProperty.Register("CallRptDateRange", typeof(CallRptDateRange), typeof(oCallbacksView), new PropertyMetadata(new CallRptDateRange()));

        private void CallsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AudioPlayer.Visibility = Visibility.Collapsed;
            if (_vm.SelectedCallback != null)
            {
                AudioPlayer.Url = ConditionalConfiguration.rootUrl + "ClientBin/messages/" +
                                  _vm.SelectedCallback.msgScr; 
                AudioPlayer.Reset();
                AudioPlayer.Title = string.Format("{0} - {1}", _vm.SelectedCallback.JobFormatted,
                    _vm.SelectedCallback.phoneFormatted);
                AudioPlayer.Visibility = Visibility.Visible;
            }


        }

        private void CallsDG_Loaded(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Visibility = Visibility.Collapsed;
        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
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
      DependencyProperty.Register("ShowData", typeof(bool), typeof(oCallbacksView), new PropertyMetadata(false));

  
    }
}