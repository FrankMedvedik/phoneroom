using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.RecruiterUtilization
{
    public class RecruiterViewModel : CollectionViewModelBase
    {
        public RecruiterViewModel()
        {
            Messenger.Default.Register<NotificationMessage<GlobalReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.GlobalReportCriteriaChanged)
                {
                    ReportDateRange = message.Content.ReportDateRange;
                    SelectedPhoneRoomName = message.Content.Phoneroom;
                    RefreshAll(null, null);
                }
            });
        }

        private string _selectedPhoneRoomName;

        public string SelectedPhoneRoomName
        {
            get { return _selectedPhoneRoomName; }
            set
            {
                _selectedPhoneRoomName = value;
                NotifyPropertyChanged();
                SelectedRecruiter = null;
            }
        }

        private bool _canRefresh = true;

        public Boolean CanRefresh
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables

        public ReportDateRange ReportDateRange = new ReportDateRange();

        #endregion

        #region CallSummaries

        private ObservableCollection<RecruiterTimeSummary> _recruiters =
            new ObservableCollection<RecruiterTimeSummary>();

        private RecruiterTimeSummary _selectedRecruiter;


        public ObservableCollection<RecruiterTimeSummary> Recruiters
        {
            get { return _recruiters; }
            set
            {
                var s = SelectedRecruiter;

                _recruiters = value;
                NotifyPropertyChanged();
                HeadingText = String.Format("{0} Phone Room Activity for {1} and {2} - for {3} Recruiters as of {4}",
                    SelectedPhoneRoomName, ReportDateRange.StartRptDate, ReportDateRange.EndRptDate, Recruiters.Count,
                    DateTime.Now.ToLongTimeString());
                //ShowGridData = true;

                if (s != null)
                {
                    SelectedRecruiter = Recruiters.First(x => x.RecruiterSIP == s.RecruiterSIP);
                }
            }
        }

        #endregion

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetRecruiters();
        }


        private string _headingText;

        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }

        public RecruiterTimeSummary SelectedRecruiter
        {
            get { return _selectedRecruiter; }
            set
            {
                _selectedRecruiter = value;
                NotifyPropertyChanged();
            }
        }

        public async void GetRecruiters()
        {
            //ShowGridData = false;
            HeadingText = "Loading...";
            try
            {
                List<RecruiterTimeSummary> l =
                    await
                        RecruiterUtilizationSvc.GetRecruiterTimeSummary(SelectedPhoneRoomName,
                            ReportDateRange.StartRptDate, ReportDateRange.EndRptDate);
                Recruiters =
                    new ObservableCollection<RecruiterTimeSummary>(l.OrderByDescending(x => x.CallTime).ToList());
                Messenger.Default.Send(new NotificationMessage(Notifications.RecruiterTimeSummaryDataRefreshed));
                LoadedOk = true;
                //ShowGridData = true;
            }
            catch (Exception e)
            {
                ShowGridData = false;
                LoadFailed(e);
                HeadingText = e.Message + " at " + DateTime.Now.ToLongTimeString();
            }
        }
    }
}