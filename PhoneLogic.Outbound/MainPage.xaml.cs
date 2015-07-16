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
        private bool _isCheckedState = true;


        public MainPage()
        {
            InitializeComponent();
            LayoutRoot.Width = 1200;
            SizeChanged += ResizeGrid;

            Messenger.Default.Register<AppColors>
                (
                    this, SetColors
                );
          Messenger.Default.Register<NotificationMessage>(this, HandleNotification);
        }

       private void HandleNotification(NotificationMessage message)
        {
            if (message.Notification == Notifications.PauseRefresh)
            {
                tbCanRefresh.Text = "Can Refresh";
            }
            if (message.Notification == Notifications.Refresh)
            {
                tbCanRefresh.Text = "Refresh Paused";
            }
        }


        private void SetColors(AppColors ac)
        {
            //LayoutRoot.Foreground = ac.TheForeground;
            LayoutRoot.Background = ac.TheBackground;
            Main.Background = ac.TheBackground;

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

        //private void tbtnShrink_Checked(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    LayoutRoot.Width = 600;
        //    TBBrowserResize.Content = "~/Images/Health-Benefits-of-Celery.jpg";

        //}

        //private void tbtnShrink_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    LayoutRoot.Width = 1200;
        //    TBBrowserResize.Content = "~/Images/fountain_collage.jpg";
        //}

        //public Boolean IsCheckedState   
        //{
        //    get { return _isCheckedState; }
        //    set
        //    {
        //        _isCheckedState = value;
        //        if(_isCheckedState == false)
        //            LayoutRoot.Width = 600;
        //        else
        //            LayoutRoot.Width = 1200;
        //    }
        //}
    }
}
