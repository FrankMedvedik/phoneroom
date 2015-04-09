using System.Collections.Generic;
using System.Windows;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using Microsoft.Lync.Model.Extensibility;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Views
{
    public partial class JobDetailView 
    {
        public JobDetailViewModel ViewModel;
        private Window _mainWindow;
        public JobDetailView()
        {
            InitializeComponent();
            ViewModel = new JobDetailViewModel();
            DataContext = ViewModel;
        }

    }
}

