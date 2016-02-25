using System.Windows;
using System.Windows.Controls;

namespace PhoneLogic.Core.Areas.Recruiters
{
    public partial class MyJobsSmallView : UserControl
    {
        private MyJobsViewModel _vm;

        public MyJobsSmallView()
        {
            InitializeComponent();
            _vm = new MyJobsViewModel();
            DataContext = _vm;
            _vm.CanRefresh = false;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    _vm.CanRefresh = true;
        //    _vm.RefreshAll();
        //    _vm.CanRefresh = false;

        //}
    }

}
