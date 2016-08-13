using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Helpers;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public partial class JobRecruitersView : UserControl
    {

        public static readonly DependencyProperty SelectedJobNumProperty =
            DependencyProperty.Register("SelectedJobNum", typeof(string), typeof(JobRecruitersView),
                new PropertyMetadata(""));

        public static readonly DependencyProperty ReportDateRangeProperty =
            DependencyProperty.Register("ReportDateRange", typeof(ReportDateRange), typeof(JobRecruitersView),
                new PropertyMetadata(new ReportDateRange()));

        public static readonly DependencyProperty ShowDataProperty =
            DependencyProperty.Register("ShowData", typeof(bool), typeof(JobRecruitersView), new PropertyMetadata(false));

        private readonly JobRecruitersViewModel _vm;

        public JobRecruitersView()
        {
            InitializeComponent();
            _vm = new JobRecruitersViewModel();
            DataContext = _vm;
        }

        public string SelectedJobNum
        {
            get { return _vm.CallRptJobNum; }
            set
            {
                _vm.CallRptJobNum = value;
            }
        }


        public ReportDateRange ReportDateRange
        {
            get { return _vm.ReportDateRange; }
            set
            {
                _vm.ReportDateRange = value;
            }
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

        public void Refresh()
        {
            _vm.RefreshAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fname = string.Format("{0}", "Jobs" + _vm.ReportDateRange.ToFormattedString('.'));
                JobRecruitersDG.Export(fname);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file - " + ex.Message);
            }
        }

        private void JobRecruitersDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedRecruiter != null)
            {
                cv.RecruiterSIP = _vm.SelectedRecruiter.recruiterSip;
                cv.SelectedJobNum = _vm.CallRptJobNum;
                cv.ReportDateRange = _vm.ReportDateRange;
                cv.Refresh();
            }
            else
                cv.ShowData = false;
        }
    }
}