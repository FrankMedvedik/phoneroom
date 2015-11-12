using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Areas.CallsRpts;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Helpers;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public partial class JobRecruitersView : UserControl
    {
        private JobRecruitersViewModel _vm = null;

        public JobRecruitersView()
        {
            InitializeComponent();
            _vm = new JobRecruitersViewModel();
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
                string fname = string.Format("{0}", "Jobs" + _vm.ReportDateRange.ToFormattedString('.'));
                JobRecruitersDG.Export(fname);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file - " + ex.Message);
            }
        }
        public string SelectedJobNum
        {
            get { return _vm.CallRptJobNum; }
            set
            {
                //SetValue(SelectedJobNumProperty, value);
                _vm.CallRptJobNum = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedJobNumProperty =
            DependencyProperty.Register("SelectedJobNum", typeof(string), typeof(JobRecruitersView), new PropertyMetadata(""));


        public ReportDateRange ReportDateRange
        {
            get { return _vm.ReportDateRange; }
            set
            {
                //SetValue(ReportDateRangeProperty, value);
                _vm.ReportDateRange = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReportDateRangeProperty =
            DependencyProperty.Register("ReportDateRange", typeof(ReportDateRange), typeof(JobRecruitersView), new PropertyMetadata(new ReportDateRange()));

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
        public bool ShowData
        {
            get { return _vm.ShowGridData; }
            set
            {
                _vm.ShowGridData = value;
                cv.ShowData = value;
            }
        }

        // Using a DependencyProperty as the backing store for boolean ShowData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDataProperty =
      DependencyProperty.Register("ShowData", typeof(bool), typeof(JobRecruitersView), new PropertyMetadata(false));


       
    }

}
