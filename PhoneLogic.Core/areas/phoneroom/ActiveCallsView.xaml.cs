using System.Windows.Controls;

namespace PhoneLogic.Core.areas.phoneroom
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