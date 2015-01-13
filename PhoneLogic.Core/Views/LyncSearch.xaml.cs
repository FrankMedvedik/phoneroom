using System.Windows.Controls;
using PhoneLogic.Core.ViewModels;


namespace PhoneLogic.Core.Views
{
    public partial class LyncSearchView : UserControl
    {

        public LyncSearchView()
        {

            InitializeComponent();
            this.DataContext = ConversationContext.Instance.PhoneLogicContext;
        }

    }
}
