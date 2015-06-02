using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Lync.Model;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Views
{
    public partial class OutboundCallView : UserControl
    {
        private OutboundCallViewModel _vm; 
        
        public OutboundCallView()
        {
            InitializeComponent();
            _vm = new OutboundCallViewModel();
            DataContext = _vm;
            
        }

        public PhoneLogicTask Task
        {
            get { return _vm.Task; }
            set
            {
                SetValue(TaskProperty, value);
                _vm.Task = value;
            }
        }

        public DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(PhoneLogicTask), typeof(OutboundCallView), new PropertyMetadata(new PhoneLogicTask()));


        private async void Call_Click(object sender, RoutedEventArgs e)
        {
            tbErrors.Text = "";
            System.Windows.Browser.HtmlPage.Plugin.Focus();
            btnCall.Focus();
                if (String.IsNullOrWhiteSpace(tbOutboundPhone.Text))
                    tbErrors.Text = "Phone number is required";
            if ((tbOutboundPhone.Text.Length != 10))
                tbErrors.Text = "Phone number invalid";

            

            if (tbErrors.Text == "")
            {
                _vm.CanMakeCall = false;
                StartTimer();
                string job = String.Format("{0}:0{1}", _vm.Task.JobNum, _vm.Task.taskID);
                //if (MessageBox.Show(
                //    String.Format("Job {0} Phone {1} sip {2}", job, _vm.PhoneNumber, LyncClient.GetClient().Self.Contact.Uri), 
                //    "Confirm",
                //    MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                //    return;

                //tbErrors.Foreground = new SolidColorBrush(Color.FromArgb(255,255,255,255));
                tbErrors.Text = "Your call has been placed";
                await LyncSvc.RecruiterDialOut(job, tbOutboundPhone.Text, 0); // zero because it is not a calllback...
            }
            System.Windows.Browser.HtmlPage.Plugin.Focus();
            tbOutboundPhone.Focus();

        }

        public void StartTimer()
        {
            System.Windows.Threading.DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, UserInterfaceTimings.OutboundCallButtonInactivateTime, 0); // 5 seconds
            myDispatcherTimer.Tick += new EventHandler(Each_Tick);
            myDispatcherTimer.Start();
        }

        // A variable to count with.
        int i = 0;

        public void Each_Tick(object o, EventArgs sender)
        {
            _vm.CanMakeCall = true;
            tbErrors.Text ="";
        }

    }
}
