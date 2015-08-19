﻿using System;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.ViewContracts.MVVMMessenger;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.Areas.ReportCriteria
{
    public class ReportDateRangeViewModel : ViewModelBase
    {
        public ReportDateRangeViewModel()
        {
            StartRptDate = DateTime.Today;
            EndRptDate = DateTime.Today.AddDays(1);
        }

        private string  _notificationMessage = Notifications.DateRangeChanged;
        public string  NotificationMessage
        {
            get { return _notificationMessage; }
            set
            {
                _notificationMessage = value; 
                
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
                Messenger.Default.Send(new NotificationMessage(this, _notificationMessage));
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
                Messenger.Default.Send(new NotificationMessage(this, _notificationMessage));
            }
        }

       }
        #endregion
 
}