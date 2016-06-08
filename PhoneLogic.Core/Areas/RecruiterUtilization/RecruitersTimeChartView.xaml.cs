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
    public partial class RecruitersTimeChartView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the RecruitersChart class.
        /// </summary>
        public RecruitersTimeChartView()
        {
            InitializeComponent();
        }

        private void LoadBarChartData()
        {
            List<KeyValuePair<string, long>> incall = new List<KeyValuePair<string, long>>();
            List<KeyValuePair<string, long>> other = new List<KeyValuePair<string, long>>();

            foreach (var r in _recruiters.OrderByDescending(x => x.CallTime.TotalMinutes))
            {
                incall.Add(new KeyValuePair<string, long>(r.DisplayName, (long) (r.CallTime.TotalMinutes)));
                other.Add(new KeyValuePair<string, long>(r.DisplayName, (long) r.IdleTime.TotalMinutes));
            }
            ((BarSeries) TimeChart.Series[0]).ItemsSource = incall;
            ((BarSeries) TimeChart.Series[1]).ItemsSource = other;
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