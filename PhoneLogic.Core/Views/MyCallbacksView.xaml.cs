using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Extensibility;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Views
{
    public partial class MyCallbacksView
    {
        private readonly MyCallBacksViewModel _vm;

        private readonly DispatcherTimer timer = new DispatcherTimer();

        public MyCallbacksView()
        {
            InitializeComponent();
            _vm = new MyCallBacksViewModel();
            DataContext = _vm;
            _vm.RefreshAll();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += timer_Tick;
            PlaybackControls.Visibility = Visibility.Collapsed;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = msgPlayback.Position.ToString().TrimEnd(new[] {'0'});
            sliderPositionBackground.Value = msgPlayback.Position.TotalSeconds;
        }

        private async void CloseCallback_Click(object sender, RoutedEventArgs e)
        {
            // set the status of the callback to closed
            _vm.CanRefresh = false;

            if (_vm.SelectedMyCallback == null) return;
            MessageBoxResult result = MessageBox.Show("Close this Message?", "Confirm", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                var cb = new CallbackDto
                {
                    SIP = LyncClient.GetClient().Self.Contact.Uri,
                    callbackID = _vm.SelectedMyCallback.callbackID
                };

                await CallbackSvc.Close(cb);
                _vm.CanRefresh = true;
                _vm.RefreshAll();
            }
        }


        private async void Call_Click(object sender, RoutedEventArgs e)
        {
            
            if (LyncClient.GetClient().State != ClientState.SignedIn)
            {
                MessageBox.Show("Please Sign in to Lync first ");
                return;
            }
            
            /* should never happen since have to have a row selected to show this button */
            if (_vm.SelectedMyCallback == null)
            {
                MessageBox.Show("Select A Callback");
                return;
            }
            if (_vm.SelectedMyCallback.SIP != null)
            {
                // inhibit them from calling someone already in a call
                if (_vm.SelectedMyCallback.SIP.Substring(0, 4) == "sip:")
                {
                    MessageBoxResult result = MessageBox.Show("Call may already be in progress, call anyway?", "Confirm",
                        MessageBoxButton.OKCancel);
                    if (result != MessageBoxResult.OK)
                        return;
                }
            }
            /* setup making the call  */
            var participantUri = new List<string> {_vm.SelectedMyCallback.callbackNum};
            var modalitySettings = new Dictionary<AutomationModalitySettings, object>
            {
                {AutomationModalitySettings.ApplicationId, ConditionalConfiguration.RecknerCallAppGuid},
                {AutomationModalitySettings.Subject, _vm.SelectedMyCallback.callbackNum},
                {AutomationModalitySettings.ApplicationData, _vm.SelectedAppData}
            };

            /* start of call */
            await CallbackSvc.StartCall(new CallbackDto
            {
                SIP = LyncClient.GetClient().Self.Contact.Uri,
                callbackID = _vm.SelectedMyCallback.callbackID
            });


            await PhoneCallSvc.PlacePhoneCall(new PhoneCall()
            {
                callbackID = _vm.SelectedMyCallback.callbackID,
                jobNum = _vm.SelectedMyCallback.jobNum,
                taskID = _vm.SelectedMyCallback.taskID,
                phoneNum = _vm.SelectedMyCallback.phoneNum,
                SIP = LyncClient.GetClient().Self.Contact.Uri
            });
            
            LyncClient.GetAutomation().BeginStartConversation(
                AutomationModalities.Audio,
                participantUri,
                modalitySettings, ar =>
                {
                    try
                    {
                        ConversationWindow newWindow = LyncClient.GetAutomation().EndStartConversation(ar);
                        newWindow.BeginOpenExtensibilityWindow(
                            ConditionalConfiguration.RecknerCallAppGuid,
                            newWindow.EndOpenExtensibilityWindow,
                            null);
                    }
                    catch (Exception oe)
                    {
                        MessageBox.Show("Can't Open Output Windows: Operation exception on start conversation " +
                                        oe.Message);
                        
                    }
                },
                null);
        }

        private void callbackGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.CanRefresh = true;
            msgPlayback.Stop();
            if (_vm.SelectedMyCallback == null) return;
            msgPlayback.Source = new Uri(ConditionalConfiguration.rootUrl + "ClientBin/messages/" + _vm.SelectedMyCallback.msgScr, UriKind.Absolute);
            msgPlayback.Stop();
            SliderPosition.Value = 0;
            msgPlayback.Stop();
        }

        private void callbackGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedMyCallback != null)
                callbackGrid.SelectedItem = _vm.SelectedMyCallback;
            else
                callbackGrid.SelectedIndex = 0;
        }

        #region msgPlayback controls
        
        private void PlayMsg_Click(object sender, RoutedEventArgs e)
        
        {
            (sender as ToggleButton).Content = "Stop";
            _vm.CanRefresh = false;
            msgPlayback.Play();
            timer.Start();
        }
        private void StopMsg_Click(object sender, RoutedEventArgs e)
        {
           (sender as ToggleButton).Content = "Play";
            msgPlayback.Stop();
            SliderPosition.Value=0;
        }
        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            SliderPosition.Maximum = msgPlayback.NaturalDuration.TimeSpan.TotalSeconds;
            msgPlayback.Position = TimeSpan.FromSeconds(0);
        }

        private void sliderPosition_ValueChanged(object sender, RoutedEventArgs e)
        {
            msgPlayback.Pause();
            msgPlayback.Position = TimeSpan.FromSeconds(SliderPosition.Value);
            msgPlayback.Play();
        }

        private void media_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            switch (msgPlayback.CurrentState)
            {
                case MediaElementState.Playing:
                    _vm.CanRefresh = false;
                    PlaybackControls.Visibility = Visibility.Visible;
                    break;
                case MediaElementState.Opening:
                    _vm.CanRefresh = false;
                    PlaybackControls.Visibility = Visibility.Visible;
                    break;
                default:
                    _vm.CanRefresh = true;
                    PlaybackControls.Visibility = Visibility.Collapsed;
                    break;
            }
            lblStatus.Text = msgPlayback.CurrentState.ToString();
        }

        
        #endregion


    }
}