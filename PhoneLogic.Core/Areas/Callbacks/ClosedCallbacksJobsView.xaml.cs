using System.Windows.Controls;
using PhoneLogic.Core.Behaviors;
using PhoneLogic.Core.Helpers;

namespace PhoneLogic.Core.Areas.Callbacks
{
    /// <summary>
    /// Description for JobsView.
    /// </summary>
    public partial class ClosedCallbacksJobsView : UserControl
    {
        private ClosedCallbacksJobsViewModel _vm = null;

        public ClosedCallbacksJobsView()
        {
            InitializeComponent();
            _vm = new ClosedCallbacksJobsViewModel();
            DataContext = _vm;
            ExcelBehavior.EnableForGrid(JobsDG);
            cv.OpenCallbacksOnly = false;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            JobsDG.Export("Jobs"+ _vm.ReportDateRange.ToFormattedString('.'));
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