using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.areas.recruiter
{
    public partial class LyncSearchView : UserControl
    {
        private readonly LyncSearchViewModel _vm;
        
        public LyncSearchView()
        {
            InitializeComponent();
            _vm = new LyncSearchViewModel();
            DataContext = _vm;
            tbSearchString.KeyUp += tbSearchString_KeyUp;
   
        }

        private void tbSearchString_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbSearchString.Text.Length == 0)
            {
                _vm.ClearSearch();
            }
            if (tbSearchString.Text.Length > 2)
            {
                _vm.SearchForContacts(tbSearchString.Text);
            }

        }

        private async void btnAddToCall_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((ContactAvailability)_vm.SelectedContact.Contact.GetContactInformation(ContactInformationType.Availability) != ContactAvailability.Free) 
               MessageBox.Show("The person must be Available (Green) to accept a transferred call");
                //CmdParms.Text = String.Format("Id {0} - {1} ",ConversationContext.Instance.PhoneLogicContext.conversationId, _vm.SelectedContact.Contact.Uri);
                await LyncSvc.TransferCall(ConversationContext.Instance.PhoneLogicContext.conversationId,
                        _vm.SelectedContact.Contact.Uri);
            }
        }

    }
 
