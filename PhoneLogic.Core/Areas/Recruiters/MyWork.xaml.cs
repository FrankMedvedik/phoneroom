using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Lync.Model;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.Recruiters
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
            cv.SetMyCallsConfiguration();
        }

        private void HandleSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeGrid();
        }


        private void DGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var c = (PhoneLogicTask)e.Row.DataContext;
            if (c.MsgCnt > 0)
            {
                FrameworkElement ele = DGrid.Columns[4].GetCellContent(e.Row);
                (ele as TextBlock).Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                FrameworkElement ele = DGrid.Columns[4].GetCellContent(e.Row);
                (ele as TextBlock).Foreground = new SolidColorBrush(Colors.Black);
            }


        }

        private void DGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedPhoneLogicTask == null)
            {
                tc.Visibility = Visibility.Collapsed; 
                return;
            }

            mcb.SelectedJobNum = _vm.SelectedPhoneLogicTask.JobNum;
            OBCView.Task = _vm.SelectedPhoneLogicTask;
            OBCView.Visibility = Visibility.Visible;
            cv.RecruiterSIP = LyncClient.GetClient().Self.Contact.Uri;
            cv.SelectedJobNum = _vm.SelectedPhoneLogicTask.JobNum + ":0" +_vm.SelectedPhoneLogicTask.taskID;
            cv.CallRptDateRange = new CallsRpts.Models.CallRptDateRange()
                { StartRptDate = Convert.ToDateTime("7/01/2015"), EndRptDate = DateTime.Now};
            cv.Refresh();
            tc.Visibility = Visibility.Visible;
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
                tc.SetValue(Grid.ColumnProperty, 0);
            }
            if (BrowserInfoSvc.ClientWidth >= UserInterfaceTimings.ResizeBoundryWidth)
            {
                DGrid.Columns[3].Visibility = Visibility.Visible;
                DGrid.Columns[4].Visibility = Visibility.Visible;
                OBCView.SetValue(Grid.RowProperty, 1);
                tc.SetValue(Grid.ColumnProperty, 1);
            }

            mcb.ResizeGrid();
        }
        
    }

}
