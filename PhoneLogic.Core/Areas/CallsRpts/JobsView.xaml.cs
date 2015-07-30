using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Areas.CallsRpts;
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
            dp.NotificationMessage = Notifications.JobCallRptDateRangeChanged;
        }
            
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                JobsDG.Export();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file - " + ex.Message);
            }
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