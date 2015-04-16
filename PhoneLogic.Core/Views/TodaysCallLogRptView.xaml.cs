using System;
using System.Windows.Controls;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
   public partial class TodaysCallLogRptView : UserControl
    {
       private TodaysCallLogRptViewModel _vm;
       public TodaysCallLogRptView()
        {
            InitializeComponent();
            _vm = new TodaysCallLogRptViewModel();
            DataContext = _vm;
            _vm.ShowLoadDatetimeHeading = true;
        }

       private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
       {
           DGrid.Export();
       }
    }
}

