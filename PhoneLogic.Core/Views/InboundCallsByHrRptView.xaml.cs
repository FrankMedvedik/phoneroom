﻿using System;
using System.Windows.Controls;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
    public partial class InboundCallsByHrRptView : UserControl
    {
        private InboundCallsByHrRptViewModel _vm;
       public InboundCallsByHrRptView()
        {
            InitializeComponent();
            _vm = new InboundCallsByHrRptViewModel();
            DataContext = _vm;
        }

       private void btnExport_Click(object sender, System.Windows.RoutedEventArgs e)
       {
           DGrid.Export();
       }

       private void btnRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
       {
           _vm.GetRpt();
       }
    }
}
