using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Helpers;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    /// <summary>
    ///     Description for RecruiterMonitorView.
    /// </summary>
    public partial class RecruiterMonitorView : UserControl
    {
        private readonly RecruiterViewModel _vm;

        /// <summary>
        ///     Initializes a new instance of the RecruiterView class.
        /// </summary>
        public RecruiterMonitorView()
        {
            InitializeComponent();
            _vm = new RecruiterViewModel();
            DataContext = _vm;
            _vm.CurrentRefreshMode = RecruiterViewModel.RefreshModes.AllRecruiters;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fname = string.Format("{0}", "AllRecruiters" + _vm.ReportDateRange.ToFormattedString('.'));

                RecruitersDG.Export(fname);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file - " + ex.Message);
            }
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