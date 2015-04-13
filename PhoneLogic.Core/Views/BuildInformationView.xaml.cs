using System;
using System.Windows;
using System.Windows.Controls;

namespace PhoneLogic.Core.Views
{
    public partial class BuildInformationView : UserControl
    {
        private String _releaseInfo;
        public BuildInformationView()
        {
            InitializeComponent();
            _releaseInfo = String.Format(" API Server - {0} \n\n Lync Server - {1}  \n\n Lync CWE GUID - {2} \n\n Build Date - {3} ",
                ConditionalConfiguration.apiUrl,
                ConditionalConfiguration.LyncServiceRefUrl,
                ConditionalConfiguration.RecknerCallAppGuid,
                ConditionalConfiguration.BuildDate);

        }
        private void tbtnTest_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_releaseInfo,"Configuration",MessageBoxButton.OK );
        }
    }
}
