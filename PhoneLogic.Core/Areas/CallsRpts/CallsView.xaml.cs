using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.areas.CallsRpts;
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

        public string RecruiterNotificationMessage
        {
            get { return _vm.RecruiterNotificationMessage; }
            set
            {
                SetValue(RecruiterNotificationMessageProperty, value);
                _vm.RecruiterNotificationMessage = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecruiterNotificationMessageProperty =
            DependencyProperty.Register("RecruiterNotificationMessage", typeof(string), typeof(CallsView), new PropertyMetadata(""));


        public string JobNotificationMessage
        {
            get { return _vm.JobNotificationMessage; }
            set
            {
                SetValue(JobNotificationMessageProperty, value);
                _vm.JobNotificationMessage = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty JobNotificationMessageProperty =
            DependencyProperty.Register("JobNotificationMessage", typeof(string), typeof(CallsView), new PropertyMetadata(""));


        public string RptDatesNotificationMessage
        {
            get { return _vm.RptDatesNotificationMessage; }
            set
            {
                SetValue(RptDatesNotificationMessageProperty, value);
                _vm.RptDatesNotificationMessage = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RptDatesNotificationMessageProperty =
            DependencyProperty.Register("RptDatesNotificationMessage", typeof(string), typeof(CallsView), new PropertyMetadata(""));

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

     
    }
}