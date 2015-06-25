using System.Windows;
using System.Windows.Controls;

namespace PhoneLogic.Core.areas.recruiter
{
    public partial class MyCallsInQueueView : UserControl
    {
        private MyCallsInQueueViewModel _vm;
        
        public MyCallsInQueueView()
        {
            
            InitializeComponent();
            _vm = new MyCallsInQueueViewModel();
            DataContext = _vm;

        }

        public string TheForeground
        {
            get { return (string)GetValue(TheForegroundProperty); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TheForegroundProperty =
            DependencyProperty.Register("TheForeground", typeof(string), typeof(MyCallsInQueueView), new PropertyMetadata(""));

        public string TheBackground
        {
            get { return _vm.TheBackground; }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TheBackgroundProperty =
            DependencyProperty.Register("TheBackground", typeof(string), typeof(MyCallsInQueueView), new PropertyMetadata(""));

        }
}
