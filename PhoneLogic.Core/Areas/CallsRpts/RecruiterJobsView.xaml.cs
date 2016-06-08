using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Areas.CallsRpts;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Helpers;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public partial class RecruiterJobsView : UserControl
    {
        private RecruiterJobsViewModel _vm = null;

        public RecruiterJobsView()
        {
            InitializeComponent();
            _vm = new RecruiterJobsViewModel();
            DataContext = _vm;
        }

        public void Refresh()
        {
            _vm.RefreshAll();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                string fname = string.Format("{0}", "RecruiterJobs" + _vm.ReportDateRange.ToFormattedString('.'));
                RecruiterJobsDG.Export(fname);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file - " + ex.Message);
            }
        }

        public string RecruiterSIP
        {
            get { return _vm.RecruiterSIP; }
            set { _vm.RecruiterSIP = value; }
        }

        public static readonly DependencyProperty RecruiterSipProperty =
            DependencyProperty.Register("RecruiterSip", typeof(string), typeof(RecruiterJobsView),
                new PropertyMetadata(""));

        public ReportDateRange ReportDateRange
        {
            get { return _vm.ReportDateRange; }
            set
            {
                //SetValue(ReportDateRangeProperty, value);
                _vm.ReportDateRange = value;
            }
        }


        public static readonly DependencyProperty ReportDateRangeProperty =
            DependencyProperty.Register("ReportDateRange", typeof(ReportDateRange), typeof(JobRecruitersView),
                new PropertyMetadata(new ReportDateRange()));

        private void RecruiterJobsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedJob != null)
            {
                cv.RecruiterSIP = _vm.RecruiterSIP;
                cv.SelectedJobNum = _vm.SelectedJob.JobNumber;
                cv.ReportDateRange = _vm.ReportDateRange;
                cv.Refresh();
            }
            else
                cv.ShowData = false;
        }

        public bool ShowData
        {
            get { return _vm.ShowGridData; }
            set
            {
                _vm.ShowGridData = value;
                cv.ShowData = value;
            }
        }

        public static readonly DependencyProperty ShowDataProperty =
            DependencyProperty.Register("ShowData", typeof(bool), typeof(JobRecruitersView), new PropertyMetadata(false));
    }
}