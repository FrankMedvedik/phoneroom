using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.areas.CallsRpts;
using PhoneLogic.Core.areas.CallsRpts.Models;
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
            JobRecruitersDG.Export();
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


        public CallRptDateRange CallRptDateRange
        {
            get { return _vm.CallRptDateRange; }
            set
            {
                //SetValue(CallRptDateRangeProperty, value);
                _vm.CallRptDateRange = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CallRptDateRangeProperty =
            DependencyProperty.Register("CallRptDateRange", typeof(CallRptDateRange), typeof(JobRecruitersView), new PropertyMetadata(new CallRptDateRange()));

        private void JobRecruitersDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedRecruiter != null)
            {
                cv.RecruiterSIP = _vm.SelectedRecruiter.recruiterSip;
                cv.SelectedJobNum = _vm.CallRptJobNum;
                cv.CallRptDateRange = _vm.CallRptDateRange;
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
