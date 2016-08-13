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

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public class RecruiterViewModel : CollectionViewModelBase
    {
        public enum RefreshModes
        {
            AllRecruiters,
            ActiveRecruiters
        }

        private RefreshModes _currentRefreshMode;


        private string _headingText;

        private string _selectedPhoneRoomName;

        public ReportDateRange ReportDateRange = new ReportDateRange();
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

        public RefreshModes CurrentRefreshMode
        {
            get { return _currentRefreshMode; }
            set
            {
                _currentRefreshMode = value;
                NotifyPropertyChanged();
            }
        }

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

        public bool CanRefresh { get; set; } = true;

        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }

        public LyncCallByRecruiter SelectedRecruiter
        {
            get { return _selectedRecruiter; }
            set
            {
                _selectedRecruiter = value;
                NotifyPropertyChanged();
            }
        }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetRecruiters();
            FilterByPhoneRoom();
        }

        public async void GetRecruiters()
        {
            ShowGridData = false;
            HeadingText = "Loading...";
            try
            {
                List<LyncCallByRecruiter> l;

                if (CurrentRefreshMode == RefreshModes.AllRecruiters)
                    l =
                        await
                            LyncCallLogSvc.GetRecruitersPlusCallSummary(ReportDateRange.StartRptDate,
                                ReportDateRange.EndRptDate);
                else
                    l =
                        await
                            LyncCallLogSvc.GetLyncCallsByRecruiter(ReportDateRange.StartRptDate,
                                ReportDateRange.EndRptDate);

                Recruiters = new ObservableCollection<LyncCallByRecruiter>(l);
                LoadedOk = true;
                ShowGridData = true;
            }
            catch (Exception e)
            {
                LoadFailed(e);
                HeadingText = e.Message;
            }
        }

        #region CallSummaries

        private ObservableCollection<LyncCallByRecruiter> _recruiters = new ObservableCollection<LyncCallByRecruiter>();
        private LyncCallByRecruiter _selectedRecruiter;


        public ObservableCollection<LyncCallByRecruiter> Recruiters
        {
            get { return _recruiters; }
            set
            {
                var s = SelectedRecruiter;

                _recruiters = value;
                NotifyPropertyChanged();
                FilterByPhoneRoom();

                if (s != null)
                {
                    SelectedRecruiter = FilteredRecruiters.First(x => x.RecruiterSIP == s.RecruiterSIP);
                }
            }
        }

        #region Filters

        private ObservableCollection<LyncCallByRecruiter> _filteredRecruiters =
            new ObservableCollection<LyncCallByRecruiter>();

        public ObservableCollection<LyncCallByRecruiter> FilteredRecruiters
        {
            get { return _filteredRecruiters; }
            set
            {
                _filteredRecruiters = value;
                NotifyPropertyChanged();
            }
        }

        private void FilterByPhoneRoom()
        {
            var fr = new List<LyncCallByRecruiter>();
            if ((SelectedPhoneRoomName == null) || (SelectedPhoneRoomName == "All"))
                FilteredRecruiters = Recruiters;
            else
            {
                fr = (from fobjs in Recruiters
                    where fobjs.PhoneRoom == SelectedPhoneRoomName
                    select fobjs).ToList();
                FilteredRecruiters = new ObservableCollection<LyncCallByRecruiter>(fr);
            }

            HeadingText = string.Format("{0} Phone Room Activity between {1} and {2} - for {3} Recruiters as of {4}",
                SelectedPhoneRoomName, ReportDateRange.StartRptDate, ReportDateRange.EndRptDate,
                FilteredRecruiters.Count, DateTime.Now.ToShortTimeString());
            ShowGridData = true;
        }

        #endregion

        #endregion
    }
}