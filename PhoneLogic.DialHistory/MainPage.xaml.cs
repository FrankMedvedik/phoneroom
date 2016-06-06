using System.Windows.Controls;
using PhoneLogic.Core.Areas.InboundCallAlerts;

namespace PhoneLogic.DialHistory
{
    public partial class MainPage : UserControl
    {
        public ToastNotification Toast;
        public MainPage()
        {
            InitializeComponent();
            Toast = ToastNotification.CreateToast(10.5); 
            Toast.ShowMessage("WORLD SOLALISM NOW");
        }
    }
}
