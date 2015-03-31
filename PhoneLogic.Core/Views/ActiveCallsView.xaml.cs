using System;
using System.Windows;
using System.Windows.Controls;
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

        private void ActiveCallsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

//        IAsyncResult ar = LyncClient.GetAutomation().BeginStartConversation(
//    _vm.SelectedActiveCall.
//    this.Handle.ToInt32(),
//    null,
//    null);
//_Automation.EndStartConversation(ar);
//    }
    }
}