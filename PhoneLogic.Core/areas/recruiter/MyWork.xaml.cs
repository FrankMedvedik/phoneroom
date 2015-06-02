using System.Windows.Controls;
using System.Windows.Media;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Views
{
    public partial class MyWorkView : UserControl
    {
        private MyJobsViewModel _vm;

        public MyWorkView()
        {
            InitializeComponent();
            _vm = new MyJobsViewModel();
            DataContext = _vm;
            OBCView.Visibility = System.Windows.Visibility.Collapsed;
        }

    

        private void DGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            PhoneLogicTask c = (PhoneLogicTask) e.Row.DataContext;
            if (c.call_cnt > 0)
            {
                e.Row.Background = new SolidColorBrush(Colors.LightGray);
                e.Row.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void DGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedPhoneLogicTask == null) return;
            MyCallbacks.SelectedJobNum = _vm.SelectedPhoneLogicTask.JobNum;
            OBCView.Task = _vm.SelectedPhoneLogicTask;
            OBCView.Visibility = System.Windows.Visibility.Visible;
        }

        
    }

}
