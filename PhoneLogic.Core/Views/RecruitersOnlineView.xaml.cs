using System.Windows.Controls;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Views
{
    public partial class RecruitersOnlineView : UserControl
    {
        private RecruitersOnlineViewModel _vm;
        public RecruitersOnlineView()
        {
            InitializeComponent();
            _vm = new RecruitersOnlineViewModel();
            DataContext = _vm;
        }
    }
}