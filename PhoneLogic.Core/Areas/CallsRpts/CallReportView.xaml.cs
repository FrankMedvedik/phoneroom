using System.Windows.Controls;
using PhoneLogic.Core.areas.phoneroom;

namespace PhoneLogic.Core.areas.CallsRpts
{
    public partial class CallReportView : UserControl
    {
        private CallReportViewModel _vm = null;
        public CallReportView()
        {
            InitializeComponent();
            _vm = new CallReportViewModel();
            DataContext = _vm;
        }



        private void CallsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //if (_vm.SelectedCall != null)
            //{
            //    RecDtl.RecruiterSip = _vm.SelectedRecruiter.sip;
            //    _vm.GetRecruiterLogs();
            //}
            //else
            //    RecDtl.RecruiterSip = null;

        }

    }
}