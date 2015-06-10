using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Views
{
    public partial class MyWorkView : UserControl
    {
        private MyJobsViewModel _vm;

        public MyWorkView()
        {
            InitializeComponent();
            _vm = new MyJobsViewModel();
            DataContext = _vm;
            OBCView.Visibility = Visibility.Collapsed;
        //    SizeChanged += HandleSizeChanged;
        }

        private void HandleSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeGrid();
        }


        private void DGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var c = (PhoneLogicTask)e.Row.DataContext;
            if (c.call_cnt > 0)
            {
                FrameworkElement ele = DGrid.Columns[2].GetCellContent(e.Row);
                (ele as TextBlock).Foreground = new SolidColorBrush(Colors.Red);
            }



        }

        private void DGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedPhoneLogicTask == null) return;
            MyCallbacks.SelectedJobNum = _vm.SelectedPhoneLogicTask.JobNum;
            if(OBCView.CanRefresh)
                OBCView.Task = _vm.SelectedPhoneLogicTask;
            if (MyCallbacks.CanRefresh)
                OBCView.Visibility = Visibility.Visible;
        }
 

        public void ResizeGrid()
        {
            //tbGridSize.Text = String.Format("Width : {0}  Height : {1} ", BrowserInfoSvc.ClientWidth,
            //    BrowserInfoSvc.ClientHeight);

            if (BrowserInfoSvc.ClientWidth < UserInterfaceTimings.ResizeBoundryWidth)
            {
                DGrid.Columns[3].Visibility = Visibility.Collapsed;
                DGrid.Columns[4].Visibility = Visibility.Collapsed;
                OBCView.SetValue(Grid.RowProperty, 2);
                MyCallbacks.SetValue(Grid.ColumnProperty, 0);
            }
            if (BrowserInfoSvc.ClientWidth >= UserInterfaceTimings.ResizeBoundryWidth)
            {
                DGrid.Columns[3].Visibility = Visibility.Visible;
                DGrid.Columns[4].Visibility = Visibility.Visible;
                OBCView.SetValue(Grid.RowProperty, 1);
                MyCallbacks.SetValue(Grid.ColumnProperty, 1);
            }

            MyCallbacks.ResizeGrid();
        }
        
    }

}
