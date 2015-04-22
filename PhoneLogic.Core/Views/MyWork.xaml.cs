using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Extensibility;
using PhoneLogic.Core.Services;
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
        }

      

    }

}
