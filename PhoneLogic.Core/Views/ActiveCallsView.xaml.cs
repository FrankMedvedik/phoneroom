using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using Microsoft.Lync.Model.Extensibility;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{

    public partial class ActiveCallsView : UserControl
    {
        private ActiveCallsViewModel _vm;

        public ActiveCallsView()
        {
            InitializeComponent();
            _vm = new ActiveCallsViewModel();
            DataContext = _vm;
        }

        private void btnJoinCall_Click(object sender, RoutedEventArgs e)
        {
            var automation = LyncClient.GetAutomation();
            //ConversationWindow window = automation.GetConversationWindow()
            IAsyncResult ar = automation.BeginStartConversation(_vm.SelectedActiveCall.ConferenceUri, 0, StartConversationCallback, null);
        }

        private void StartConversationCallback(IAsyncResult result)
        {
            try
            {
                ConversationWindow cw = LyncClient.GetAutomation().EndStartConversation(result);
            }
            catch (LyncClientException lyncClientException)
            {
                MessageBox.Show("Join Failed.");
                Console.WriteLine(lyncClientException);
            }
            catch (SystemException systemException)
            {
                    MessageBox.Show("Call failed.");
                    Console.WriteLine("Error: " + systemException);
            }
        }


    }
}