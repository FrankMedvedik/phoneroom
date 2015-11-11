using System.Windows.Controls;
using PhoneLogic.Core.Areas.Callbacks;
using PhoneLogic.Core.Behaviors;
using PhoneLogic.Core.Helpers;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.Callbacks
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
            //cv.SetAllCallsbacksConfiguration();
            ExcelBehavior.EnableForGrid(JobsDG);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            JobsDG.Export();
        }

        private void JobsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedJob != null)
            {
                cv.SelectedJobNum = _vm.SelectedJob.jobNum;
                cv.SelectedTaskId = _vm.SelectedJob.taskID;
                cv.ReportDateRange = _vm.ReportDateRange;
                cv.Refresh();
            }
            else
                cv.ShowData = false;
        }
    }
}