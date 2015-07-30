/*=====================================================================
  File:      MainPage.xaml.cs

  Summary:   Main page view for PhoneLogic.Outbound project. 

=====================================================================*/

using System;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core;
using PhoneLogic.ViewContracts.MVVMMessenger;
using PhoneLogic.Core.Services;

namespace PhoneLogic.Outbound
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            LayoutRoot.Width = 1200;
            SizeChanged += ResizeGrid;

            Messenger.Default.Register<AppColors>
                (
                    this, SetColors
                );
        }


        private void SetColors(AppColors ac)
        {
            LayoutRoot.Background = ac.TheBackground;
            Main.Background = ac.TheBackground;

        }

        private void ResizeGrid(object sender, SizeChangedEventArgs e)
        {
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
    }
}
