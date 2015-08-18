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
        /// <summary>
        /// Initializes a new instance of the GlobalReportCriteriaView class.
        /// </summary>
        public GlobalReportCriteriaView()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if ((message.Notification == Notifications.DateRangeChanged)
                    || (message.Notification == Notifications.PhoneroomChanged))
                {
                    if (PR.SelectedPhoneroom == null)
                        spCriteria.Visibility = Visibility.Collapsed;
                    else
                    {
                        spCriteria.Visibility = Visibility.Visible;
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
            });

        }
    }
}
