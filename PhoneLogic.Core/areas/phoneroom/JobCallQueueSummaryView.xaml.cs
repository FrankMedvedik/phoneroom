using System.Windows.Controls;
using PhoneLogic.Core.ViewModels;


namespace PhoneLogic.Core.Views
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
