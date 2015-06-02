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

    }
}