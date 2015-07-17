using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.areas.CallsRpts;
using PhoneLogic.Core.Helpers;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    /// <summary>
    /// Description for JobsView.
    /// </summary>
    public partial class JobsView : UserControl
    {
        private JobsViewModel _vm = null;

        public JobsView()
        {
            InitializeComponent();
            _vm = new JobsViewModel();
            DataContext = _vm;
            DatePicker.NotificationMessage = Notifications.JobCallRptDateRangeChanged;
        }
            
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            JobsDG.Export();
        }

        private void JobsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedJob != null)
            {
                jrv.SelectedJobNum = _vm.SelectedJob.JobNumber;
                jrv.CallRptDateRange = _vm.CallRptDateRange;
                jrv.Refresh();
            }
            else
                jrv.ShowData = false;
        }
    }
}