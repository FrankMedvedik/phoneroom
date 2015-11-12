using System;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.ViewContracts.MVVMMessenger;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.Areas.ReportCriteria
{
    public class ReportDateRangeViewModel : ViewModelBase
    {
        public ReportDateRangeViewModel()
        {
            TimeSpan startTime = new TimeSpan(08, 00, 0);
            TimeSpan endTime = new TimeSpan(17, 00, 0);
            _startRptDate = DateTime.Today + startTime;
            _endRptDate = DateTime.Today + endTime;
            Messenger.Default.Send(new NotificationMessage(this, _notificationMessage));
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

        private DateTime _startRptDate;
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

        private DateTime _endRptDate;
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

        private void chkAutoEndTime_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        public void FloatEndTime()
        {
            throw new NotImplementedException();
        }
    }

}
