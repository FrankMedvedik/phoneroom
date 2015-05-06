using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private ToggleButton _selectedButton = new ToggleButton();

        public MyCallbacksView()
        {
            InitializeComponent();
            _vm = new MyCallBacksViewModel();
            DataContext = _vm;
            _vm.RefreshAll();
            AudioPlayer.Visibility = Visibility.Collapsed;
            AudioPlayer.AudioPlayback.MediaEnded += ResetPlaybackState;

        }

        public string SelectedJobNum 
        {
            get { return _vm.SelectedJobNum; }
            set
            {
                SetValue(SelectedJobNumProperty, value);
                _vm.SelectedJobNum = value; }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedJobNumProperty =
            DependencyProperty.Register("SelectedJobNum", typeof(string), typeof(MyCallbacksView), new PropertyMetadata(""));


        private async void CloseCallback_Click(object sender, RoutedEventArgs e)
        {
            // set the status of the callback to closed
            _vm.CanRefresh = false;

            if (_vm.SelectedMyCallback == null) return;
            MessageBoxResult result = MessageBox.Show("Delete this Message?", "Confirm", MessageBoxButton.OKCancel);

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

            if (!String.IsNullOrWhiteSpace(_vm.SelectedMyCallback.status))  
            {
                if (MessageBox.Show("Call may already be in progress, call anyway?", "Confirm",
                    MessageBoxButton.OKCancel) != MessageBoxResult.OK) 
                    return;
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


            await PhoneCallSvc.LogPhoneCall(new PhoneCall()
            {
                callbackID = _vm.SelectedMyCallback.callbackID,
                jobNum = _vm.SelectedMyCallback.jobNum,
                taskID = _vm.SelectedMyCallback.taskID,
                phoneNum = _vm.SelectedMyCallback.phoneNum,
                SIP = LyncClient.GetClient().Self.Contact.Uri
            });
            
            String job = _vm.SelectedMyCallback.jobNum + ":0" + _vm.SelectedMyCallback.taskID;
            await LyncSvc.RecruiterDialOut(job, _vm.SelectedMyCallback.phoneNum);

            //LyncClient.GetAutomation().BeginStartConversation(
            //    AutomationModalities.Audio,
            //    participantUri,
            //    modalitySettings, ar =>
            //    {
            //        try
            //        {
            //            ConversationWindow newWindow = LyncClient.GetAutomation().EndStartConversation(ar);
            //            newWindow.BeginOpenExtensibilityWindow(
            //                ConditionalConfiguration.RecknerCallAppGuid,
            //                newWindow.EndOpenExtensibilityWindow,
            //                null);
            //        }
            //        catch (Exception oe)
            //        {
            //            MessageBox.Show("Can't Open Output Windows: Operation exception on start conversation " +
            //                            oe.Message);
            //        }
            //    },
            //    null);
        }

        private void callbackGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AudioPlayer.Visibility = Visibility.Collapsed;
            if(_vm.SelectedMyCallback != null)
                AudioPlayer.PlaybackFileName = ConditionalConfiguration.rootUrl + "ClientBin/messages/" + _vm.SelectedMyCallback.msgScr;
           
        }

        private void callbackGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedMyCallback != null)
                callbackGrid.SelectedItem = _vm.SelectedMyCallback;
            else
                callbackGrid.SelectedIndex = 0;
        }

        private void tbtnPlay_Checked(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Visibility = Visibility.Visible;
            _selectedButton = (sender as ToggleButton);
            _selectedButton.Content = "Stop";
            AudioPlayer.Play();
            _vm.CanRefresh = false;

        }

        private void tbtnPlay_Unchecked(object sender, RoutedEventArgs e)
        {
            _selectedButton = (sender as ToggleButton);
            _selectedButton.Content = "Play";
            AudioPlayer.Stop();
            _vm.CanRefresh = true;
            AudioPlayer.Visibility = Visibility.Collapsed;
        }
        
        private void ResetPlaybackState(object o, RoutedEventArgs e)
        {
            _selectedButton.Content = "Play";
            AudioPlayer.Visibility = Visibility.Collapsed;
        }
    }
}