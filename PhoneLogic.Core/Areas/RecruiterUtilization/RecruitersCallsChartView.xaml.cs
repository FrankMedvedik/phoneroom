using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.RecruiterUtilization
{
    /// <summary>
    /// Description for RecruitersChart.
    /// </summary>
    public partial class RecruitersCallsChartView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the RecruitersChart class.
        /// </summary>
        public RecruitersCallsChartView()
        {
            InitializeComponent();
        }

        private void LoadBarChartData()
        {
            List<KeyValuePair<string, int>> inbound = new List<KeyValuePair<string, int>>();
            List<KeyValuePair<string, int>> outbound = new List<KeyValuePair<string, int>>();
            List<KeyValuePair<string, int>> callCnt = new List<KeyValuePair<string, int>>();

            foreach (var r in _recruiters.OrderByDescending(x => x.CallCnt))
            {
                inbound.Add(new KeyValuePair<string, int>(r.DisplayName, r.InboundCallCnt));
                outbound.Add(new KeyValuePair<string, int>(r.DisplayName, r.OutboundCallCnt));
            }
            ((BarSeries) CallsChart.Series[0]).ItemsSource = inbound;
            ((BarSeries) CallsChart.Series[1]).ItemsSource = outbound;
            //((BarSeries)CallsChart.Series[2]).ItemsSource = callCnt;
        }

        private List<RecruiterTimeSummary> _recruiters;

        public List<RecruiterTimeSummary> Recruiters
        {
            set
            {
                _recruiters = value;
                LoadBarChartData();
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecruitersProperty =
            DependencyProperty.Register("Recruiters", typeof(string), typeof(RecruitersCallsChartView),
                new PropertyMetadata(""));
    }
}