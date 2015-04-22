using System.Windows.Controls;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
   public partial class MyJobsView : UserControl
    {
       private MyJobsViewModel _vm;
       public MyJobsView()
        {
            InitializeComponent();
            _vm = new MyJobsViewModel();
            DataContext = _vm;
        }

       
    }
}

