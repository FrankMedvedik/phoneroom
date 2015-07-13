using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.areas.phoneroom
{
    public class RecruiterCallRptViewModel : CollectionViewModelBase
    {
        public RecruiterCallRptViewModel()
        {
            
            
            StartRptDate = DateTime.Now.AddDays(-1);
            EndRptDate = DateTime.Now.AddDays(1) ;
            GetRecruiterCallRpt();
            Messenger.Default.Register<NotificationMessage>(this, HandleNotification);
        }

        private void HandleNotification(NotificationMessage message)
        {
            if (message.Notification == Notifications.PauseRefresh)
            {
                CanRefresh = false;
            }
            if (message.Notification == Notifications.ResumeRefresh)
            {
                CanRefresh = true;
            }
        }
        private bool _canRefresh;
        public Boolean CanRefresh       
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables
        private DateTime _startRptDate = DateTime.Now.AddDays(-1);
        public DateTime StartRptDate
        {
            get { return _startRptDate; }
            set
            {
                _startRptDate = value;
                NotifyPropertyChanged();
            }
        }

        private DateTime _endRptDate = DateTime.Now;
        public DateTime EndRptDate
        {
            get { return _endRptDate; }
            set
            {
                _endRptDate = value;
                NotifyPropertyChanged();
            }
        }

        private string _selectedPhoneRoomName;
        public string SelectedPhoneRoomName
        {
            get { return _selectedPhoneRoomName; }
            set
            {
                _selectedPhoneRoomName = value;
                NotifyPropertyChanged();
                //SelectedRecruiter = null;
                //RefreshFilteredData();
            }
        }


        #endregion

        protected override void RefreshAll(object sender, EventArgs e)
        {
            if (CanRefresh)
            {
                GetRecruiterCallRpt();
            }
        }

        #region RptRecruiterCalls

        private ObservableCollection<RptRecruiterCalls> _rptRecruiterCalls = new ObservableCollection<RptRecruiterCalls>();

        public ObservableCollection<RptRecruiterCalls> RptRecruiterCalls 
        {
            get
            {
                return _rptRecruiterCalls;
            }
            set
            {
                _rptRecruiterCalls = value;
                NotifyPropertyChanged();
            }
        }


        public async void GetRecruiterCallRpt()
        {
            try
            {
                var ro = await RecruiterSvc.GetRecruiterCallRpt(StartRptDate, EndRptDate);
                if (ro.Count > 0)
                {
                    ShowGridData = true;
                    RptRecruiterCalls = new ObservableCollection<RptRecruiterCalls>(ro);
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

        #endregion


    }
}