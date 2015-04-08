using System.Windows.Controls;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{

    public partial class TimerView : UserControl
    {
        private TimerViewModel _vm;

        public TimerView()
        {
            InitializeComponent();
            _vm = new TimerViewModel();
            DataContext = _vm;
        }


    }
}