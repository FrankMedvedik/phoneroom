using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.areas.CallsRpts;
using PhoneLogic.Core.Helpers;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public partial class JobRecruitersView : UserControl
    {
        private JobRecruitersViewModel _vm = null;

        public JobRecruitersView()
        {
            InitializeComponent();
            _vm = new JobRecruitersViewModel();
            DataContext = _vm;

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            JobRecruitersDG.Export();
        }

    }

}
