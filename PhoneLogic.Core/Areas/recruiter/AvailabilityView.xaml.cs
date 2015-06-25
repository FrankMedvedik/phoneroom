using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.areas.recruiter
{
    public partial class AvailabilityView : UserControl
    {
        public AvailabilityView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HtmlPage.Window.Navigate(new Uri("PhoneLogic.MyCalls.html", UriKind.Relative), "_blank");
        }
        
    }
}
