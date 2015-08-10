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
            mcb.SetMyCallsbacksConfiguration();
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
                FrameworkElement ele = DGrid.Columns[2].GetCellContent(e.Row);
                (ele as TextBlock).Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                FrameworkElement ele = DGrid.Columns[2].GetCellContent(e.Row);
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

            RefreshOutboundCall();
            RefreshCallBacks();
            RefreshCalls();

            tc.Visibility = Visibility.Visible;
        }

        private void RefreshCalls()
        {
            // no need to refresh if nothing has changed 
            if ((cv.SelectedJobNum == _vm.SelectedPhoneLogicTask.JobNum + ":0" + _vm.SelectedPhoneLogicTask.TaskID)
                && (_vm.SelectedPhoneLogicTask.LastCallTime == cv.LastCallTime   || _vm.SelectedPhoneLogicTask.LastCallTime == null))
                return;
            // trigger getting the list of outbound calls
            cv.RecruiterSIP = LyncClient.GetClient().Self.Contact.Uri;
            cv.SelectedJobNum = _vm.SelectedPhoneLogicTask.JobNum + ":0" + _vm.SelectedPhoneLogicTask.TaskID;
            cv.CallRptDateRange = new CallsRpts.Models.CallRptDateRange() { StartRptDate = Convert.ToDateTime("7/01/2015"), EndRptDate = DateTime.Now };
            cv.Refresh();
        }

        private void RefreshOutboundCall()
        {
            // no need to refresh if nothing has changed 
            if (OBCView.Task == _vm.SelectedPhoneLogicTask) return;
            // setup the outbound call view job info  
            OBCView.Task = _vm.SelectedPhoneLogicTask;
            OBCView.Visibility = Visibility.Visible;
        }

        private void RefreshCallBacks()
        {
            if (mcb.SelectedJobNum == _vm.SelectedPhoneLogicTask.JobNum 
                && mcb.SelectedTaskId == _vm.SelectedPhoneLogicTask.TaskID.GetValueOrDefault(0) 
                && (_vm.SelectedPhoneLogicTask.NewestMsg == mcb.LastCallBackStartTime)  || _vm.SelectedPhoneLogicTask.MsgCnt.GetValueOrDefault(0) == 0)
                return;
            // Trigger getting callbacks 
            mcb.SelectedJobNum = _vm.SelectedPhoneLogicTask.JobNum;
            mcb.SelectedTaskId = _vm.SelectedPhoneLogicTask.TaskID.GetValueOrDefault(0);
            mcb.Refresh();
        }

        public void ResizeGrid()
        {
            //tbGridSize.Text = String.Format("Width : {0}  Height : {1} ", BrowserInfoSvc.ClientWidth,
            //    BrowserInfoSvc.ClientHeight);

            if (BrowserInfoSvc.ClientWidth < UserInterfaceTimings.ResizeBoundryWidth)
            {
         
                DGrid.Columns[4].Visibility = Visibility.Collapsed;
                DGrid.Columns[5].Visibility = Visibility.Collapsed;
                DGrid.Columns[6].Visibility = Visibility.Collapsed;
                DGrid.Columns[7].Visibility = Visibility.Collapsed;
                DGrid.Columns[8].Visibility = Visibility.Collapsed;
                DGrid.Columns[9].Visibility = Visibility.Collapsed;
                DGrid.Columns[10].Visibility = Visibility.Collapsed;
                DGrid.Columns[11].Visibility = Visibility.Collapsed;
                DGrid.Columns[12].Visibility = Visibility.Collapsed;

                tc.SetValue(Grid.RowProperty, 1);
                tc.SetValue(Grid.ColumnProperty, 0);
            }
            if (BrowserInfoSvc.ClientWidth >= UserInterfaceTimings.ResizeBoundryWidth)
            {
                DGrid.Columns[4].Visibility = Visibility.Visible;
                DGrid.Columns[5].Visibility = Visibility.Visible;
                DGrid.Columns[6].Visibility = Visibility.Visible;
                DGrid.Columns[7].Visibility = Visibility.Visible;
                DGrid.Columns[8].Visibility = Visibility.Visible;
                DGrid.Columns[9].Visibility = Visibility.Visible;
                DGrid.Columns[10].Visibility = Visibility.Visible;
                DGrid.Columns[11].Visibility = Visibility.Visible;
                DGrid.Columns[12].Visibility = Visibility.Visible;
                tc.SetValue(Grid.RowProperty, 0);
                tc.SetValue(Grid.ColumnProperty, 1);
            }

            mcb.ResizeGrid();
            cv.ResizeGrid();
        }
        
    }

}
