using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Behaviors;
using PhoneLogic.Core.Helpers;

namespace PhoneLogic.Core.Areas.Callbacks
{
    /// <summary>
    ///     Description for JobsView.
    /// </summary>
    public partial class OpenCallbacksJobsView : UserControl
    {
        private readonly OpenCallbacksJobsViewModel _vm;

        public OpenCallbacksJobsView()
        {
            InitializeComponent();
            _vm = new OpenCallbacksJobsViewModel();
            DataContext = _vm;
            ExcelBehavior.EnableForGrid(JobsDG);
            cv.OpenCallbacksOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JobsDG.Export(string.Format("Open Callbacks Jobs {0}", DateTime.Now));
        }

        private void JobsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedJob != null)
            {
                cv.SelectedJobNum = _vm.SelectedJob.jobNum;
                cv.SelectedTaskId = _vm.SelectedJob.taskID;
                cv.Refresh();
            }
            else
                cv.ShowData = false;
        }
    }
}