using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PhoneLogic.Core.Areas.InboundCallAlerts
{
    public partial class ToastNotification : UserControl
    {
        private readonly DispatcherTimer _timer;

        private string _textMsg = "";

        private ToastNotification(double timespan)
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(timespan);
            _timer.Tick += new EventHandler(TimerTick);
            this.LayoutRoot.Width = Application.Current.Host.Content.ActualWidth;

        }

        private string _pauseMessage = "";

        public string TextMsg
        {
            get { return _textMsg; }
            set { _textMsg = value; }
        }

        public string PauseMessage
        {
            get { return _pauseMessage; }
            set { _pauseMessage = value; }
        }

        /// <summary>
        /// public constructor
        /// </summary>
        /// <param name="timespan"></param>
        /// <returns></returns>
        public static ToastNotification CreateToast(double timespan)
        {

            if (_notification == null)
            {
                _notification = new ToastNotification(timespan);
            }
            return _notification;
        }

        /// <summary>
        /// Diaplay the Toast
        /// </summary>
        /// <param name="message"></param>
        public void ShowMessage(string message)
        {
            PauseMessage = "Click to pause.";
            MessagePaused.Text = PauseMessage;

            TextMsg = message;
            MessageTB.Text = TextMsg;

            NotificationPopup.IsOpen = true;

            if (_timer.IsEnabled)
            {
                _timer.Stop();
            }
            _timer.Start();

            VisualStateManager.GoToState(this, "Normal", false);
        }

        private static ToastNotification _notification;

        private void TimerTick(object sender, EventArgs e)
        {
            VisualStateManager.GoToState(this, "Hidden", false);
            NotificationPopup.IsOpen = false;
            _timer.Stop();
        }

        /// <summary>
        /// Click to pause or close toast
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayoutRoot_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                PauseMessage = "Toast is paused - Click here to close.";
                MessagePaused.Text = PauseMessage;
            }
            else
            {
                VisualStateManager.GoToState(this, "Hidden", false);
                NotificationPopup.IsOpen = false;
                PauseMessage = "Click to pause.";
            }

        }

    }
}