using System.Windows.Controls;
using PhoneLogic.Core.Helpers;

namespace PhoneLogic.Core.areas.PhoneRooms
{
    public partial class JobCallActivityRptView : UserControl
    {
        private JobCallActivityRptViewModel _vm;
       public JobCallActivityRptView()
        {
            InitializeComponent();
            _vm = new JobCallActivityRptViewModel();
            DataContext = _vm;
        }

       private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
       {
           DGrid.Export();
       }
       
        private void btnRefreshClick(object sender, System.Windows.RoutedEventArgs e)
        {
            _vm.GetRpt();
        }
    }
}

