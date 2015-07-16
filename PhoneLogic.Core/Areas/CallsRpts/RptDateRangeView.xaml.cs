using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.areas.CallsRpts;

namespace PhoneLogic.Core.Areas.CallsRpts
{

    public partial class RptDateRangeView : UserControl
    {
        private RptDateRangeViewModel _vm = null;

        public RptDateRangeView()
        {
            InitializeComponent();
            _vm = new RptDateRangeViewModel();
            DataContext = _vm;

        }


        public string  NotificationMessage
        {
            get { return _vm.NotificationMessage; }
            set
            {
                SetValue(NotificationMessageProperty, value);
                _vm.NotificationMessage = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotificationMessageProperty =
            DependencyProperty.Register("NotificationMessage", typeof(string ), typeof(RptDateRangeView), new PropertyMetadata(""));

        
    }
}