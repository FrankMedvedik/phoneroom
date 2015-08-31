using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Areas.CallsRpts;
using PhoneLogic.Core.Helpers;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    /// <summary>
    /// Description for RecruiterMonitorView.
    /// </summary>
    public partial class RecruiterMonitorView : UserControl
    {
        private RecruiterViewModel _vm = null;
        /// <summary>
        /// Initializes a new instance of the RecruiterView class.
        /// </summary>
        
        public RecruiterMonitorView()
        {
            InitializeComponent();
            _vm = new RecruiterViewModel();
            DataContext = _vm;
            _vm.CurrentRefreshMode = RecruiterViewModel.RefreshModes.AllRecruiters;
            
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
                cv.ReportDateRange = _vm.ReportDateRange;
                cv.Refresh();
            }
            else
                cv.ShowData = false;
        }

    }
}