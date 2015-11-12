using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Areas.CallsRpts;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Helpers;
using PhoneLogic.Core.Services;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.RecruiterUtilization
{
    /// <summary>
    /// Description for RecruitersView.
    /// </summary>
    public partial class RecruitersView : UserControl
    {
        private RecruiterViewModel _vm = null;
        /// <summary>
        /// Initializes a new instance of the RecruiterView class.
        /// </summary>
        
        public RecruitersView()
        {
            InitializeComponent();
            _vm = new RecruiterViewModel();
            DataContext = _vm;
          //  av.Visibility = Visibility.Collapsed;
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {

                if (message.Notification == Notifications.RecruiterTimeSummaryDataRefreshed)
                {
                    TimeChart.Recruiters = _vm.Recruiters.Where(x => x.CallCnt > 0).ToList(); 
                    CallChart.Recruiters = _vm.Recruiters.Where(x => x.CallCnt > 0).ToList(); ;
                }
            });
            
        }
        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var g = new GlobalReportCriteria()
                {
                    Phoneroom = _vm.SelectedPhoneRoomName,
                    ReportDateRange = _vm.ReportDateRange
                };
                RecruitersDG.Export("recruiters"+ g.ToFormattedString('.'));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file - " + ex.Message);
            }
            
        }

        private void RecruitersDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedRecruiter != null)
            {
                av.sip = _vm.SelectedRecruiter.RecruiterSIP;
                av.RecruiterActivities = _vm.SelectedRecruiter.RecruiterActivities;
            //    av.Visibility = Visibility.Visible;
            }
            //else
            //    av.Visibility = Visibility.Collapsed;
        }

    }
}