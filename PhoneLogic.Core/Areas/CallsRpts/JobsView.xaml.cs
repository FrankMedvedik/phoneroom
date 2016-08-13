using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Helpers;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    /// <summary>
    ///     Description for JobsView.
    /// </summary>
    public partial class JobsView : UserControl
    {
        private readonly JobsViewModel _vm;

        public JobsView()
        {
            InitializeComponent();
            _vm = new JobsViewModel();
            DataContext = _vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JobsDG.Export("Jobs" + _vm.ReportDateRange.ToFormattedString('.'));
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
                jrv.ReportDateRange = _vm.ReportDateRange;
                jrv.Refresh();
            }
            else
                jrv.ShowData = false;
        }
    }
}