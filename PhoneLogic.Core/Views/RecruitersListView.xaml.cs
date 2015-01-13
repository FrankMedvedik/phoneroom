using System.Windows.Controls;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
    public partial class RecruitersListView : UserControl
    {
        private RecruitersViewModel _vm = null;
        public RecruitersListView()
        {
            InitializeComponent();
            _vm = new RecruitersViewModel();
            DataContext = _vm;
        }

        private void btnRefreshClick(object sender, System.Windows.RoutedEventArgs e)
        {
            _vm.GetRecruiters();
        }

        private void RecruiterDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.GetRecruiterLogs();
        }

    }
}