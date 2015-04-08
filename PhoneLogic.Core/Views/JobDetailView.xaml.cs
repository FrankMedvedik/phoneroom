using System.Collections.Generic;
using System.Windows;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using Microsoft.Lync.Model.Extensibility;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Views
{
    public partial class JobDetailView 
    {
        private JobDetailViewModel _vm;
        private Window _mainWindow;
        public JobDetailView()
        {
            InitializeComponent();
            
            Visibility = ConversationContext.Instance.ShowJobDetailView ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            //CallbackButtons.Visibility = ConversationContext.Instance.ShowCallbackButtons ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            _vm = new JobDetailViewModel();
            DataContext = _vm;
            var conversation = (Conversation)LyncClient.GetHostingConversation();
            if (conversation != null)
            {
                //var myAutomation = LyncClient.GetAutomation();
                //ConversationWindow win = myAutomation.GetConversationWindow(conversation);
                //conversation.StateChanged += conversation_StateChanged;
            }
            //        http://forums.silverlight.net/forums/p/185664/424174.aspx

            //_mainWindow = App.GetApp.MainWindow;

            //App.GetApp.MainWindow.Closing += (s, e1) =>
            //{
            //    if (UIUtilities.ShowMessage("Would you like to exit AMT Mobile?", "Exit Application", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            //    {
            //        e1.Cancel = true;
            //    }
            //};
        }



          //you have to store this to work around the bug
    //
        //private async void conversation_StateChanged(object sender, ConversationStateChangedEventArgs e)
        //{
        //    MessageBox.Show("Conversation State:" + e.NewState.ToString());

        //    if (e.NewState == ConversationState.Inactive)
        //    {
        //        var cb = new CallbackDto()
        //        {
        //            callbackID = ConversationContext.Instance.PhoneLogicContext.callbackId,
        //            SIP = LyncClient.GetClient().Self.Contact.Uri
        //        };

        //        if (ConversationContext.Instance.PhoneLogicContext.callbackId > 0)
        //        {   var waitingWindow = new WaitingWindow();
        //            MessageBoxResult result = MessageBox.Show("Delete Message? Select Cancel to Keep", "Confirm",
        //                MessageBoxButton.OKCancel);
        //            if (result == MessageBoxResult.OK)
        //            {
        //                waitingWindow.Show();
        //                await CallbackSvc.EndCall(cb);
        //            }
        //            else
        //            {
        //                waitingWindow.Show();
        //                await CallbackSvc.Close(cb);
        //            }
        //            waitingWindow.Close();
        //        } 
        //        var conversation = (Conversation)LyncClient.GetHostingConversation();
        //        var myAutomation = Microsoft.Lync.Model.LyncClient.GetAutomation();
        //        conversation.End();
        //        ConversationWindow cw = myAutomation.GetConversationWindow(conversation);
        //        cw.Close();
        //    }

        //}

        //private void BtnCloseCall_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult result = MessageBox.Show("Close this Message?", "Confirm", MessageBoxButton.OKCancel);
        //    if (result == MessageBoxResult.OK)
        //    {
        //        EndCallAndClose();
        //    }
        //}

        //private void BtnKeepOpen_Click(object sender, RoutedEventArgs e)
        //{
        //    EndCallAndKeepOpen();
        //}

        //private async void EndCallAndKeepOpen()
        //{
        //    await CallbackSvc.EndCall(GetThisCallback());
        //    ShutdownCwe();
        //}


        //private async void EndCallAndClose()
        //{
        //    await CallbackSvc.Close(GetThisCallback());
        //    ShutdownCwe();
        //}

        //private void ShutdownCwe()
        //{
        //    var conversation = (Conversation)LyncClient.GetHostingConversation();
        //    var myAutomation = Microsoft.Lync.Model.LyncClient.GetAutomation();
        //    conversation.End();
        //    ConversationWindow cw = myAutomation.GetConversationWindow(conversation);
        //    cw.Close();
        //}

        private CallbackDto GetThisCallback()
        {
            var cb = new CallbackDto()
            {
                callbackID = ConversationContext.Instance.PhoneLogicContext.callbackId,
                SIP = LyncClient.GetClient().Self.Contact.Uri
            };
            return cb;
        }
    }
}

