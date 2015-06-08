/*=====================================================================
  File:      MainPage.xaml.cs

  Summary:   Main page view for PhoneLogic.Outbound project. 

=====================================================================*/

using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.Views;

namespace PhoneLogic.Outbound
{
    public partial class MainPage : UserControl
    {
          
        public MainPage()
        {
            InitializeComponent();
            LayoutRoot.Width = 1200;
            SizeChanged += ResizeGrid;
        }

        private void ResizeGrid(object sender, SizeChangedEventArgs e)
        {
            //BrowserInfo.Text = String.Format("Width : {0}  Height : {1} ", BrowserInfoSvc.ClientWidth,
            //     BrowserInfoSvc.ClientHeight);
            if (BrowserInfoSvc.ClientWidth < UserInterfaceTimings.ResizeBoundryWidth)
            {
                MyCallsInQ.SetValue(Grid.RowProperty, 1);
                MyCallsInQ.SetValue(Grid.ColumnProperty, 0);
            }
            if (BrowserInfoSvc.ClientWidth >= UserInterfaceTimings.ResizeBoundryWidth)
            {
                MyCallsInQ.SetValue(Grid.RowProperty, 0);
                MyCallsInQ.SetValue(Grid.ColumnProperty, 1);
            }
            MyWork.ResizeGrid();
            }

        private void tbtnShrink_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            LayoutRoot.Width = 350;
            

        }

        private void tbtnShrink_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            LayoutRoot.Width = 800;

        }
    }
}
