using System.Windows;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
    public partial class JobDetailView 
    {
        public JobDetailViewModel ViewModel;
        public JobDetailView()
        {
            InitializeComponent();
            ViewModel = new JobDetailViewModel();
            DataContext = ViewModel;
     
        }

    }
}

