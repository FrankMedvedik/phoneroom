using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.CallRpt.Model;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.Areas.CallsRpts
{
    public class RptDateRangeViewModel : ViewModelBase
    {
        public RptDateRangeViewModel()
        {
            StartRptDate = DateTime.Today;
            EndRptDate = DateTime.Today.AddDays(1);
        }

        private string  _notificationMessage = Notifications.CallRptDateRangeChanged;
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

        private RelayCommand _sendRptDateRangeCommand;

        public RelayCommand SendRptDateRangeCommand
        {
            get
            {
                return _sendRptDateRangeCommand
                    ?? (_sendRptDateRangeCommand = new RelayCommand(
                    () =>
                    {
                        CallRptDateRange crs = new CallRptDateRange()
                        {
                            EndRptDate = this.EndRptDate,
                            StartRptDate = this.StartRptDate
                        };
                        Messenger.Default.Send(new NotificationMessage<CallRptDateRange>(this, crs, NotificationMessage));
                    }));
            }
        }
        #endregion
 
    }
}