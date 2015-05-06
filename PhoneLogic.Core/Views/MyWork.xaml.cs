using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Extensibility;
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
        }

    
//        private async void Call_Click(object sender, RoutedEventArgs e)
//        {


//            if (LyncClient.GetClient().State != ClientState.SignedIn)
//            {
//                MessageBox.Show("Please Sign in to Lync first ");
//                return;
//            }

//            /* should never happen since have to have a row selected to show this button */
//            if (_vm.SelectedPhoneLogicTask == null)
//            {
//                MessageBox.Show("Select A Job");
//                return;
//            }

//            await LyncSvc.RecruiterDialOut(new PhoneCall()
//            {
//                jobNum = _vm.SelectedPhoneLogicTask.JobNum, // MIGHT NEED TASK TOO?
//                phoneNum = _vm.PhoneNumber,
//                SIP = LyncClient.GetClient().Self.Contact.Uri
//            });
//               _vm.CanRefresh = true;
//;       }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            _vm.CanRefresh = false;
        }

        private void DGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            PhoneLogicTask c = (PhoneLogicTask) e.Row.DataContext;
            if (c.call_cnt > 0)
            {
                e.Row.Background = new SolidColorBrush(Colors.Cyan);
                e.Row.Foreground = new SolidColorBrush(Colors.Black);
            }

        //e.Row.Foreground = ColorToBrushSvc.GetForeground(c.call_cnt);
        }

        private void DGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (String.IsNullOrWhiteSpace(_vm.SelectedPhoneLogicTask.JobNum)) return;
            if (_vm.SelectedPhoneLogicTask == null) return;
            MyCallbacks.SelectedJobNum = _vm.SelectedPhoneLogicTask.JobNum;
            OBCView.Task = _vm.SelectedPhoneLogicTask;
        }

        //private async void Call_Click(object sender, RoutedEventArgs e)
        //{
        // //   string OutboundNumber = ((Button)sender).CommandParameter.ToString();
        //    //DataGridRowEventArgs a = (DataGridRowEventArgs) e;
        //    PhoneLogicTask plt = _vm.SelectedPhoneLogicTask;
        //    //if (String.IsNullOrWhiteSpace(_vm.SelectedPhoneLogicTask.PhoneNumber))
        //    //{
        //    //    MessageBox.Show("Please enter a phone number first");
        //    //    return;
        //    //}
        //    if (LyncClient.GetClient().State != ClientState.SignedIn)
        //    {
        //        MessageBox.Show("Please Sign in to Lync first ");
        //        return;
        //    }

        //    /* should never happen since have to have a row selected to show this button */
        //    //if (_vm.SelectedPhoneLogicTask == null)
        //    //{
        //    //    MessageBox.Show("Select A Callback");
        //    //    return;
        //    //}

        //    /* setup making the call  */
        //    var participantUri = new List<string> { plt.PhoneNumber};
        //    var modalitySettings = new Dictionary<AutomationModalitySettings, object>
        //    {
        //        {AutomationModalitySettings.ApplicationId, ConditionalConfiguration.RecknerCallAppGuid},
        //        {AutomationModalitySettings.Subject, plt.PhoneNumber},
        //        {AutomationModalitySettings.ApplicationData, _vm.SelectedAppData}
        //    };

        //    ///* start of call */
        //    //await CallbackSvc.StartCall(new CallbackDto
        //    //{
        //    //    SIP = LyncClient.GetClient().Self.Contact.Uri,
        //    //    callbackID = _vm.SelectedMyCallback.callbackID
        //    //});


        //    //await PhoneCallSvc.LogPhoneCall(new PhoneCall()
        //    //{
        //    //    //callbackID = _vm.SelectedMyCallback.callbackID,
        //    //    jobNum = _vm.SelectedPhoneLogicTask.JobNum,
        //    //    //taskID = _vm.SelectedPhoneLogicTask.JobNum,
        //    //    phoneNum = _vm.PhoneNumber,
        //    //    SIP = LyncClient.GetClient().Self.Contact.Uri
        //    //});

        //    LyncClient.GetAutomation().BeginStartConversation(
        //        AutomationModalities.Audio,
        //        participantUri,
        //        modalitySettings, ar =>
        //        {
        //            try
        //            {
        //                ConversationWindow newWindow = LyncClient.GetAutomation().EndStartConversation(ar);
        //                newWindow.BeginOpenExtensibilityWindow(
        //                    ConditionalConfiguration.RecknerCallAppGuid,
        //                    newWindow.EndOpenExtensibilityWindow,
        //                    null);
        //            }
        //            catch (Exception oe)
        //            {
        //                MessageBox.Show("Can't Open Output Windows: Operation exception on start conversation " +
        //                                oe.Message);
        //            }
        //        },
        //        null);
        //}
    }

}
