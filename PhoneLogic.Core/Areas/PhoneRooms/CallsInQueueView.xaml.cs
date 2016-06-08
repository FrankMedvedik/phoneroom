using System.Windows.Controls;

namespace PhoneLogic.Core.Areas.PhoneRooms
{
    public partial class CallsInQueueView : UserControl
    {
        private CallsInQueueViewModel _vm;

        public CallsInQueueView()
        {
            InitializeComponent();
            _vm = new CallsInQueueViewModel();
            DataContext = _vm;
        }
    }
}