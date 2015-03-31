using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization;
using System.Windows.Controls.DataVisualization.Charting;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Views
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
        }
 

        private async void LoadChart(object sender, RoutedEventArgs e)
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


        

