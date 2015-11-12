using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace PhoneLogic.Core.Areas.ReportCriteria
{

    public partial class ReportDateRangeView : UserControl
    {
        private ReportDateRangeViewModel _vm = null;

        public ReportDateRangeView()
        {
            InitializeComponent();
            _vm = new ReportDateRangeViewModel();
            DataContext = _vm;

        }
        protected void StartAutoRefresh(int refreshIntervalInSeconds)
        {
            _vm.EndRptDate = DateTime.Now;
            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Tick += RefreshEndTime;
            _timer.Interval = new TimeSpan(0, 0, refreshIntervalInSeconds);
            _timer.Start();
        }

        protected void StopAutoRefresh()
        {
            _timer.Tick -= RefreshEndTime;
            _timer.Stop();
        }

        private void RefreshEndTime(object sender, EventArgs e)
        {
            _vm.EndRptDate = DateTime.Now;
        }

        public ReportDateRange DateRange
        {
            get
            {
                return new ReportDateRange()
                {
                    StartRptDate = _vm.StartRptDate,
                    EndRptDate = _vm.EndRptDate
                };
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateRangeProperty =
            DependencyProperty.Register("DateRange", typeof(ReportDateRange), typeof(ReportDateRangeView), new PropertyMetadata(new ReportDateRange()));

        private DispatcherTimer _timer;

        public bool AutoEndTime { get; set; }

        private void chkAutoEndTime_Checked(object sender, RoutedEventArgs e)
        {
            AutoEndTime = true;
            StartAutoRefresh(60);
        }
        private void chkAutoEndTime_Unchecked(object sender, RoutedEventArgs e)
        {
            AutoEndTime = false;
            //pckEnd.IsEnabled = true;
            StopAutoRefresh();

        }

    }
}