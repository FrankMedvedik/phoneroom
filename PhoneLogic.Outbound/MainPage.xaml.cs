/*=====================================================================
  File:      MainPage.xaml.cs

  Summary:   Main page view for PhoneLogic.Outbound project. 

=====================================================================*/

using System;
using System.Windows.Controls;
using System.Threading;

namespace PhoneLogic.Outbound
{
    public partial class MainPage : UserControl
    {
          
        public MainPage()
        {
            InitializeComponent();
           // comboBox1.ItemsSource = LoadComboBoxData();
        }


        //private string[] LoadComboBoxData()
        //{
        //    string[] Jobs =
        //    {
        //        "All",
        //        "20121739",
        //        "20121735",
        //        "20121732",
        //        "20121618",
        //        "20090002"
        //    };
        //    return Jobs;
        //}

        //private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    mcb.SelectedJobNum = comboBox1.SelectedItem.ToString();
        //}

        //public int SelectedJob { get; set; }
    }
}
