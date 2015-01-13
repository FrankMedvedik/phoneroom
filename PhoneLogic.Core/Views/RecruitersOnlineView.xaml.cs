using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
    /// <summary>
    /// Description for RecruitersOnlineView.
    /// </summary>
    public partial class RecruitersOnlineView : UserControl
    {
        private RecruitersOnlineViewModel _vm;
        /// <summary>
        /// Initializes a new instance of the RecruitersOnlineView class.
        /// </summary>
        public RecruitersOnlineView()
        {
            InitializeComponent();
            _vm = new RecruitersOnlineViewModel();
            DataContext = _vm;
        }
    }
}