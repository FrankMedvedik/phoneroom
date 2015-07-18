using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.areas.CallsRpts;
using PhoneLogic.Core.Helpers;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    /// <summary>
    /// Description for RecruiterMonitorView.
    /// </summary>
    public partial class RecruiterMonitorView : UserControl
    {
        private RecruiterMonitorViewModel _vm = null;
        /// <summary>
        /// Initializes a new instance of the RecruiterView class.
        /// </summary>
        
        public RecruiterMonitorView()
        {
            InitializeComponent();
            _vm = new RecruiterMonitorViewModel();
            DataContext = _vm;
            DatePicker.NotificationMessage = Notifications.RecruiterCallRptDateRangeChanged;

        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RecruitersDG.Export();
        }

        private void RecruitersDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedRecruiter != null)
            {
                cv.RecruiterSIP = _vm.SelectedRecruiter.RecruiterSIP;
                cv.CallRptDateRange = _vm.CallRptDateRange;
                cv.Refresh();
            }
            else
                cv.ShowData = false;
        }

    }
}