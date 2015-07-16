using System.Windows.Controls;

namespace PhoneLogic.Core.areas.PhoneRooms
{
    public partial class RecruiterCallRptView : UserControl
    {
        private RecruiterCallRptViewModel _vm = null;
        public RecruiterCallRptView()
        {
            InitializeComponent();
            _vm = new RecruiterCallRptViewModel();
            DataContext = _vm;
        }

    }
}