/*=====================================================================
  File:      MainPage.xaml.cs

  Summary:   Main page view for PhoneLogic.Outbound project. 

=====================================================================*/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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


        //private void SetColors(AppColors ac)
        //{
        //    LayoutRoot.Background = ac.TheBackground;
        //    Main.Background = ac.TheBackground;

        //}
        private void SetColors(AppColors ac)
        {
            String xamlString = "<Canvas xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Background=\"Aqua\"/>";
            Canvas c = (Canvas)System.Windows.Markup.XamlReader.Load(xamlString);
            SolidColorBrush AquaBrush = (SolidColorBrush)c.Background;


            //LayoutRoot.Foreground = ac.TheForeground;
            if (ac.TheBackground != new SolidColorBrush(Colors.LightGray))
                LayoutRoot.Background = ac.TheBackground;
            else
                LayoutRoot.Background = AquaBrush;
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
