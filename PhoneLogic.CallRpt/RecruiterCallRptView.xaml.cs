using System.Windows.Controls;

namespace PhoneLogic.Core.Areas.PhoneRooms
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