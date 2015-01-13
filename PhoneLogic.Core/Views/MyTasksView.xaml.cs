using System;
using System.Windows.Controls;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
   public partial class MyTasksView : UserControl
    {
       private MyTasksViewModel _vm;
       public MyTasksView()
        {
            InitializeComponent();
            _vm = new MyTasksViewModel();
            DataContext = _vm;
        }

       private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
       {
           DGrid.Export();
       }
    }
}

