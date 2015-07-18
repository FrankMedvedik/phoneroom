using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.areas.CallsRpts;
using PhoneLogic.Core.areas.CallsRpts.Models;
using PhoneLogic.Core.Helpers;

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
                AudioPlayer.Url = ConditionalConfiguration.rootUrl + "ClientBin/messages/" +
                                  _vm.SelectedCall.CallId;
                AudioPlayer.Reset();
                AudioPlayer.Title = string.Format("{0} - {1}", _vm.SelectedCall.JobFormatted,
                    _vm.SelectedCall.PhoneFormatted);
                AudioPlayer.Visibility = Visibility.Visible;
            }


        }

        private void CallsDG_Loaded(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Visibility = Visibility.Collapsed;
            if (_vm.SelectedCall != null)
                CallsDG.SelectedItem = _vm.SelectedCall;
            else
                CallsDG.SelectedIndex = 0;
        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CallsDG.Export();
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




    }
}