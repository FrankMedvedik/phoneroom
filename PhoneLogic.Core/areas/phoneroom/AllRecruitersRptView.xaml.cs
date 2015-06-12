using System.Windows.Controls;

namespace PhoneLogic.Core.areas.phoneroom
{
    public partial class AllRecruitersRptView : UserControl
    {
        private AllRecruitersViewModel _vm = null;
        public AllRecruitersRptView()
        {
            InitializeComponent();
            _vm = new AllRecruitersViewModel();
            DataContext = _vm;
        }

      

        private void RecruiterDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecDtl.RecruiterSip = _vm.SelectedRecruiter.sip;
            _vm.GetRecruiterLogs();
        }

    }
}