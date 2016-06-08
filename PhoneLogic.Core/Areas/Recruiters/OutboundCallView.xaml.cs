using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Areas.Callbacks;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.Recruiters
{
    public partial class OutboundCallView : UserControl
    {
        private OutboundCallViewModel _vm;

        public OutboundCallView()
        {
            InitializeComponent();
            _vm = new OutboundCallViewModel();
            DataContext = _vm;
            //cid.spPhoneLookUp.Visibility = System.Windows.Visibility.Collapsed;
            Messenger.Default.Register<NotificationMessage<string>>(this, message =>
            {
                if (message.Notification == Notifications.PhoneNumberChanged)
                {
                    _vm.PhoneNumber = message.Content;
                    PlaceCall();
                }
            });
        }

        public Boolean CanRefresh
        {
            get { return _vm.CanMakeCall; }
        }

        // Using a DependencyProperty as the backing store for CanRefresh.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanRefreshProperty =
            DependencyProperty.Register("CanRefresh", typeof(Boolean), typeof(MyCallbacksView),
                new PropertyMetadata(false));

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
            DependencyProperty.Register("Task", typeof(PhoneLogicTask), typeof(OutboundCallView),
                new PropertyMetadata(new PhoneLogicTask()));


        private void ValidateCallerId()
        {
            tbErrors.Text = "";
            System.Windows.Browser.HtmlPage.Plugin.Focus();
            //btnCall.Focus();
            if (String.IsNullOrWhiteSpace(_vm.PhoneNumber))
                tbErrors.Text = "Phone number is required";
            if ((PhoneNumberSvc.GetNumbers(_vm.PhoneNumber).Length != 10))
                tbErrors.Text = "Phone number invalid";
        }

        private async void PlaceCall()
        {
            ValidateCallerId();

            if (tbErrors.Text == "")
            {
                _vm.CanMakeCall = false;
                StartTimer();
                string job = String.Format("{0}:0{1}", _vm.Task.JobNum, _vm.Task.TaskID);
                tbErrors.Text = "Your call has been placed";
                await LyncSvc.RecruiterDialOut(job, PhoneNumberSvc.GetNumbers(_vm.PhoneNumber), 0);
                    // zero because it is not a calllback...
            }
            //System.Windows.Browser.HtmlPage.Plugin.Focus();
            //tbOutboundPhone.Focus();
        }

        public void StartTimer()
        {
            System.Windows.Threading.DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, UserInterfaceTimings.OutboundCallButtonInactivateTime, 0);
                // 5 seconds
            myDispatcherTimer.Tick += new EventHandler(Each_Tick);
            myDispatcherTimer.Start();
        }

        // A variable to count with.
        int i = 0;

        public void Each_Tick(object o, EventArgs sender)
        {
            _vm.CanMakeCall = true;
            tbErrors.Text = "";
        }
    }
}