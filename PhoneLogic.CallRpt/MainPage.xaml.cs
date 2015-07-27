using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.ViewContracts.MVVMMessenger;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PhoneLogic.CallRpt
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            Messenger.Default.Register<string>(this, HandleNotification);
            InitializeComponent();
         
        }

        private void HandleNotification(string message)
        {

            //if (message == Notifications.AudioPlaybackStarted)
            //{
            //    tbPlaybackStatus.Text = "Playback started";
            //}
            //if (message == Notifications.AudioPlaybackEnded)
            //{
            //    tbPlaybackStatus.Text = "Playback Ended";
            //}
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //            ap.Visibility = Visibility.Collapsed;
        //    try
        //    {
        //        Messenger.Default.Send(Notifications.AudioPlaybackStarted);
        //        ap.Url = "http://localhost:19938/ClientBin/numbers.wma";
        //        ap.Reset();
        //        ap.Title = "Audio Test";
        //        ap.Visibility = Visibility.Visible;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        }
    }
