using System.Windows;
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
        private void tbtnTest_Checked(object sender, RoutedEventArgs e)
        {
            tbtnShowDebug.Content = "(I)";
            InfoStackPanel.Visibility = System.Windows.Visibility.Visible;
        }

        private void tbtnTest_Unchecked(object sender, RoutedEventArgs e)
        {
            tbtnShowDebug.Content = "(i)";
            InfoStackPanel.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
