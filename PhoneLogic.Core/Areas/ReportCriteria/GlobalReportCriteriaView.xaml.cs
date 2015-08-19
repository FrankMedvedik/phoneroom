using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.ReportCriteria
{
    /// <summary>
    /// Description for GlobalReportCriteriaView.
    /// </summary>
    public partial class GlobalReportCriteriaView : UserControl
    {
        private Visibility _showControl;

        /// <summary>
        /// Initializes a new instance of the GlobalReportCriteriaView class.
        /// </summary>
        public GlobalReportCriteriaView()
        {
            InitializeComponent();
            DataContext = this;
            //Messenger.Default.Register<NotificationMessage>(this, message =>
            //{
            //    if ((message.Notification == Notifications.DateRangeChanged)
            //        || (message.Notification == Notifications.PhoneroomChanged))
            //    {
            //        if (PR.SelectedPhoneroom == null)
            //            ShowControl = Visibility.Collapsed;
            //        else
            //        {
            //            ShowControl = Visibility.Visible;
            //            var rc = new GlobalReportCriteria()
            //            {
            //                Phoneroom = PR.SelectedPhoneroom,
            //                PhoneroomRecruiters = PR.PhoneroomRecruiters,
            //                PhoneroomJobs = PR.PhoneroomJobs,
            //                ReportDateRange = DR.DateRange,
            //            };
            //            Messenger.Default.Send(new NotificationMessage<GlobalReportCriteria>(this, rc,
            //                Notifications.GlobalReportCriteriaChanged));
            //        }
            //    }
            //});


        }

        private Visibility ShowControl
        {
            get { return _showControl; }
            set { _showControl = value; }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            var rc = new GlobalReportCriteria()
            {
                Phoneroom = PR.SelectedPhoneroom,
                PhoneroomRecruiters = PR.PhoneroomRecruiters,
                PhoneroomJobs = PR.PhoneroomJobs,
                ReportDateRange = DR.DateRange,
            };
            Messenger.Default.Send(new NotificationMessage<GlobalReportCriteria>(this, rc,
                Notifications.GlobalReportCriteriaChanged));
        }
    }
}