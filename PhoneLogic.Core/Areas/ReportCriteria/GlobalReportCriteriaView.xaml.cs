﻿using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.ReportCriteria
{
    /// <summary>
    /// Description for GlobalReportCriteriaView.
    /// </summary>
    public partial class GlobalReportCriteriaView : UserControl
    {
        private Visibility _showControl;
        private GlobalReportCriteria grc{ get; set; }

        /// <summary>
        /// Initializes a new instance of the GlobalReportCriteriaView class.
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
                }
            });
        }

        public Visibility ShowControl
        {
            get { return _showControl; }
            set { _showControl = value; }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            grc.ReportDateRange = DR.DateRange;
            Messenger.Default.Send(new NotificationMessage<GlobalReportCriteria>(this, grc,
                Notifications.GlobalReportCriteriaChanged));
        }
    }
}