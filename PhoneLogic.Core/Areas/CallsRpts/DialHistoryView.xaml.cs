using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.Services;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    /// <summary>
    /// Description for CallsView.
    /// </summary>
    public partial class DialHistoryView : UserControl
    {
        private DialHistoryViewModel _vm = null;

        public DialHistoryView()
        {
            InitializeComponent();
            _vm = new DialHistoryViewModel();
            DataContext = _vm;


        }
        public void Refresh()
        {
            _vm.RefreshAll();
        }

        public string SelectedCallerId
        {
            get { return _vm.SelectedCallerId; }
            set
            {
                SetValue(SelectedCallerIdProperty, value);
                _vm.SelectedCallerId = value;
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedCallerIdProperty =
            DependencyProperty.Register("SelectedCallerId", typeof(string), typeof(DialHistoryView), new PropertyMetadata(""));

        public void GetCalls(string CallerId )
        {
            SelectedCallerId = CallerId;
            _vm.GetCalls();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _vm.GetCalls();
        }

        public bool ShowData
        {
            get { return _vm.ShowGridData; }
            set
            {
                _vm.ShowGridData = value;
                _vm.HeadingText = "";

            }
        }
        // Using a DependencyProperty as the backing store for boolean ShowData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowDataProperty =
      DependencyProperty.Register("ShowData", typeof(bool), typeof(DialHistoryView), new PropertyMetadata(false));



        public bool ShowInput
        {
            get { return _vm.ShowInput; }
            set
            {
                _vm.ShowInput = value;
                _vm.HeadingText = "";

            }
        }
        // Using a DependencyProperty as the backing store for boolean ShowData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowInputProperty =
      DependencyProperty.Register("ShowInput", typeof(bool), typeof(DialHistoryView), new PropertyMetadata(false));


    }
}