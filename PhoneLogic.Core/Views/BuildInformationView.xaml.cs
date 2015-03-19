using System.Windows.Controls;

namespace PhoneLogic.Core.Views
{
    public partial class BuildInformationView : UserControl
    {
        public BuildInformationView()
        {
            InitializeComponent();
            ReleaseInfo.Text = ConditionalConfiguration.apiUrl + " " + ConditionalConfiguration.LyncServiceRefUrl + " " +
                               ConditionalConfiguration.RecknerCallAppGuid;

        }
    }
}
