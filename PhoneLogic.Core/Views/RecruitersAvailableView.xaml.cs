using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
    /// <summary>
    /// Description for RecruitersAvailableView.
    /// </summary>
    public partial class RecruitersAvailableView : UserControl
    {
        private RecruitersAvailableViewModel _vm;
        
        /// <summary>
        /// Initializes a new instance of the RecruitersAvailableView class.
        /// </summary>
        public RecruitersAvailableView()
        {
            InitializeComponent();
            _vm = new RecruitersAvailableViewModel();
            DataContext = _vm;
        }

        private void ClickRefresh(object sender, RoutedEventArgs e)
        {
              Dispatcher.BeginInvoke(() => LyncSvc.testAll());
        }
    }
}