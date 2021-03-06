﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.ReportCriteria
{
    /// <summary>
    ///     Description for GlobalReportCriteriaView.
    /// </summary>
    public partial class GlobalReportCriteriaView : UserControl
    {
        private DispatcherTimer _timer;

        /// <summary>
        ///     Initializes a new instance of the GlobalReportCriteriaView class.
        /// </summary>
        public GlobalReportCriteriaView()
        {
            InitializeComponent();
            DataContext = this;
            Messenger.Default.Register<NotificationMessage<GlobalReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.PhoneroomChanged)
                {
                    grc = message.Content;
                    grc.ReportDateRange = DR.DateRange;
                    btnRefresh.IsEnabled = true;
                    RefreshAll(null, null);
                }
            });
        }

        private GlobalReportCriteria grc { get; set; }

        public bool AutoRefresh { get; set; }

        protected void StartAutoRefresh(int refreshIntervalInSeconds)
        {
            _timer = new DispatcherTimer();
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
            grc.ReportDateRange = DR.DateRange;
            Messenger.Default.Send(new NotificationMessage<GlobalReportCriteria>(this, grc,
                Notifications.GlobalReportCriteriaChanged));
        }

        private void chkAutoRefresh_Unchecked(object sender, RoutedEventArgs e)
        {
            _timer.Tick -= RefreshAll;
            _timer.Stop();
        }
    }
}