using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.areas.recruiter
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

