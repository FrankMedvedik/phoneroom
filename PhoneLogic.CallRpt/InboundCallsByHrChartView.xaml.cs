using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;

namespace PhoneLogic.Core.Areas.PhoneRooms
{
    public partial class InboundCallsByHrChartView : UserControl
    {
        private InboundCallsByHrRptViewModel _vm;
        public InboundCallsByHrChartView()
        {
            InitializeComponent();
            _vm = new InboundCallsByHrRptViewModel();
            DataContext = _vm;
            LineChart.DataContext = _vm;

        }

        private async void LoadData(object sender, RoutedEventArgs e)
        {
            _vm.GetRpt();
            LoadChart();
        }

        private void LoadChart()
        {

            LineChart.Series.Clear();
            
            foreach (var s in _vm.ChartSeriesCollection)
            {
                var ser = new LineSeries();
                ser.IsSelectionEnabled = true;
                ser.Name = s.Name;
                ser.ItemsSource = s.Elements;
                ser.IndependentValueBinding = new System.Windows.Data.Binding("IndependentValue");
                ser.DependentValueBinding = new System.Windows.Data.Binding("DependentValue");
                ser.Title = s.Name;
                ser.IsSelectionEnabled=true;
                LineChart.Series.Add(ser);

            }
        }

    }
}


        

