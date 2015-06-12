using System.Windows.Controls;

namespace PhoneLogic.Core.areas.phoneroom
{
    public partial class JobCallQueueSummaryView : UserControl
    {
        private JobCallQueueSummaryViewModel _vm;
        
        public JobCallQueueSummaryView()
        {
            InitializeComponent();
            _vm = new JobCallQueueSummaryViewModel();
            DataContext = _vm;
            

        }

    }
}
