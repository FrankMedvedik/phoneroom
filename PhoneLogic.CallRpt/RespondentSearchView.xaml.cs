using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Areas.Recruiters
{
    public partial class RespondentSearchView : UserControl
    {
        private PeopleViewModel _vm = new PeopleViewModel();
        public RespondentSearchView()
        {

            InitializeComponent();
            DataContext = _vm;
            if(ConversationContext.Instance.PhoneLogicContext.callerId != null)
            { 
                this.txtCallerId.Text = ConversationContext.Instance.PhoneLogicContext.callerId;
               _vm.LookupPhone(txtCallerId.Text);
            }
        }
        
        private void LookupButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.LookupPhone(txtCallerId.Text);
        }

        private void txtCallerId_TextChanged(object sender, TextChangedEventArgs args)
        {
            Regex expression = new Regex(@"[^1234567890.]");
            string inputString = ((TextBox)sender).Text;
            bool result = expression.IsMatch(inputString);
            ((TextBox)sender).Text = Regex.Replace(((TextBox)sender).Text, @"[^1234567890.]", string.Empty);
            BtnLookup.IsEnabled = true;
        }

    }
}
