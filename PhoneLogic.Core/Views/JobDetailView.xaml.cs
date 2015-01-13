using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using Microsoft.Lync.Model.Extensibility;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using Newtonsoft.Json;

namespace PhoneLogic.Core.Views
{
    public partial class JobDetailView 
    {
        private JobDetailViewModel _vm; 
        
        public JobDetailView()
        {
            InitializeComponent();
            Visibility = ConversationContext.Instance.ShowJobDetailView ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            CallbackButtons.Visibility = ConversationContext.Instance.ShowCallbackButtons ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            _vm = new JobDetailViewModel();
            DataContext = _vm;
        }

        private void BtnCloseCall_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Close this Message?", "Confirm", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                EndCallAndClose();
            }
        }

        private void BtnKeepOpen_Click(object sender, RoutedEventArgs e)
        {
            EndCallAndKeepOpen();
        }

        private async void EndCallAndKeepOpen()
        {
            await CallbackSvc.EndCall(GetThisCallback());
            ShutdownCwe();
        }


        private async void EndCallAndClose()
        {
            await CallbackSvc.Close(GetThisCallback());
            ShutdownCwe();
        }

        private void ShutdownCwe()
        {
            var conversation = (Conversation)LyncClient.GetHostingConversation();
            var myAutomation = Microsoft.Lync.Model.LyncClient.GetAutomation();
            conversation.End();
            ConversationWindow cw = myAutomation.GetConversationWindow(conversation);
            cw.Close();
        }

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
