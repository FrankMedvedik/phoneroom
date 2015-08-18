using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Areas.ReportCriteria;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.PhoneRooms
{
    public class ActiveCallsViewModel : CollectionViewModelBase
    {

        #region ActiveCalls

        ObservableCollection<ActiveCallDetail> _activeCalls;

        public ObservableCollection<ActiveCallDetail> ActiveCalls
        {
            get { return _activeCalls; }
            set
            {
                if (_activeCalls != value)
                {
                    _activeCalls = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("TheBackground");
                    NotifyPropertyChanged("TheForeground");
                    NotifyPropertyChanged("FilteredActiveCalls");
                }
            }
        }

        public ObservableCollection<ActiveCallDetail> FilteredActiveCalls
        {
            get { 
                return new ObservableCollection<ActiveCallDetail>(from c in _activeCalls
                join b in MyRecruiters on c.RecruiterUri equals b.sip
                select c);
            }
        }

        #endregion


        // sets up the 
        public ActiveCallsViewModel()
        {
            StartAutoRefresh(ApiRefreshFrequency.LyncApi);

            Messenger.Default.Register<NotificationMessage<GlobalReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.GlobalReportCriteriaChanged)
                {
                    MyRecruiters = message.Content.PhoneroomRecruiters;
                    RefreshAll(null, null);
                }
            });
            RefreshAll();

        }

        public List<Recruiter> MyRecruiters { get; set; }

        protected override void RefreshAll(object sender, EventArgs e)
        {
            GetActiveCalls();
        }


        #region SelectedActiveCall

        private ActiveCallDetail _selectedActiveCall;
        public ActiveCallDetail SelectedActiveCall
        {
            get
            {
                return _selectedActiveCall;
            }
            set
            {
                _selectedActiveCall = value;
                NotifyPropertyChanged();
            }
        }
        
        #endregion

        public async void GetActiveCalls()
        {
            try
            {
                LoadDate = DateTime.Now;
                var cq = await LyncSvc.GetActiveCallsDetail();
                if (cq.Count > 0)
                {
                    ActiveCalls = cq;
                    ShowGridData = true;
                }
                else
                    ShowGridData = false;
                LoadedOk = true;
            }
            catch (Exception e)
            {
                LoadFailed(e);
            }
        }
        #region DisplayColors




        public string TheBackground
        {
            get
            {
                var callCnt = 0;
                try
                {
                    callCnt = ActiveCalls.Count;
                }
                catch (Exception e)
                {
                    callCnt = 0;
                }

                return ColorMappingSvc.GetBackground(callCnt);
            }
        }

        public string TheForeground
        {
            get {
                var callCnt = 0;
                try
                {
                    callCnt = ActiveCalls.Count;
                }
                catch (Exception e)
                {
                    callCnt = 0;
                }

                return ColorMappingSvc.GetForeground(callCnt);
            }
        }
        #endregion




    }
}
