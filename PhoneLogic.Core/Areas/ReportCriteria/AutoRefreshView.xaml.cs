using System;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.ReportCriteria
{
    public partial class AutoRefreshView : UserControl
    {
        private Visibility _showControl;
        private System.Windows.Threading.DispatcherTimer _timer;

        public AutoRefreshView()
        {
            InitializeComponent();
            DataContext = this;
            btnRefresh.IsEnabled = true;
        }

        protected void StartAutoRefresh(int refreshIntervalInSeconds)
        {
            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Tick += RefreshAll;
            _timer.Interval = new TimeSpan(0, 0, refreshIntervalInSeconds);
            _timer.Start();
        }

        private void RefreshAll(object sender, EventArgs e)
        {
            if (AutoRefresh)
            {
                SendUpdateMessage();
            }
        }

        public bool AutoRefresh { get; set; }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            SendUpdateMessage();
        }

        private void chkAutoRefresh_Checked(object sender, RoutedEventArgs e)
        {
            AutoRefresh = true;
            StartAutoRefresh(60);
        }

        private void SendUpdateMessage()
        {
            Messenger.Default.Send(new NotificationMessage(this,
                Notifications.AutoRefreshNow));
        }

        private void chkAutoRefresh_Unchecked(object sender, RoutedEventArgs e)
        {
            _timer.Tick -= RefreshAll;
            _timer.Stop();
        }
    }
}