using System.Windows;
using System.Windows.Controls;
using Microsoft.Lync.Model;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
    /// <summary>
    /// Description for RecruitersOnlineView.
    /// </summary>
    public partial class RecruitersListView : UserControl
    {
        private RecruitersViewModel _vm;
        /// <summary>
        /// Initializes a new instance of the RecruitersOnlineView class.
        /// </summary>
        public RecruitersListView()
        {
            InitializeComponent();
            _vm = new RecruitersViewModel();
            DataContext = _vm;
        }

        private void btnRefreshClick(object sender, System.Windows.RoutedEventArgs e)
        {
            //MessageBox.Show("Domain: " + _vm.SelectedRecruiterLyncStatus.ToString()
            //                + " Location: " + _vm.SelectedPhoneRoomName);
            _vm.GetRecruiters();
        }

        private void RecruiterDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.GetRecruiterLogs();
        }

        private void availabilityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}