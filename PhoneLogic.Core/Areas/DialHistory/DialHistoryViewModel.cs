using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.DialHistory
{
    public class DialHistoryViewModel : CollectionViewModelBase
    {
        public DialHistoryViewModel()
        {
            AllCalls = true;
            //Messenger.Default.Register<NotificationMessage<DialHistoryMessage>>(this, message =>
            //{
            //    if (message.Notification == Notifications.PhoneNumberChanged)
            //    {
            //        PhoneNumber = message.Content.PhoneNumber;
            //        StartDate = message.Content.StartDate;
            //        EndDate = message.Content.StartDate;
            //        GetCalls();
            //    }
            //});
            Messenger.Default.Register<NotificationMessage<string>>(this, message =>
                     {
                         if (message.Notification == Notifications.PhoneNumberChanged)
                         {
                             PhoneNumber = message.Content;
                             GetCalls();
                         }
                     });
        }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        private Call _selectedCall;
        public Call SelectedCall
        {
            get { return _selectedCall; }
            set
            {
                _selectedCall = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<Call>(this, SelectedCall, Notifications.DialHistoryCallChanged));
            }
        }

        private string _phoneNumber= null;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                NotifyPropertyChanged();
            }
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


        private ObservableCollection<Call> _calls = new ObservableCollection<Call>();

        public ObservableCollection<Call> Calls
        {
            get { return _calls; }
            set
            {
                _calls = value;
                NotifyPropertyChanged();
            }
        }
        private bool _showInput = true;

        public bool ShowInput
        {
            get { return _showInput; }
            set { _showInput = value; NotifyPropertyChanged(); }
        }

   

        protected override void RefreshAll(object sender, EventArgs e)
        {
                GetCalls();
        }

        public async void GetCalls()
        {
            ShowGridData = false;
            HeadingText = "";
            var ro = new List<Call>();
            HeadingText = "Loading...";
            try
            {
                if (PhoneNumber != null)
                {
                    if(AllCalls)
                        ro = await LyncCallLogSvc.GetCalls(PhoneNumberSvc.GetNumbers(PhoneNumber));
                    else
                        ro = await LyncCallLogSvc.GetCallsToPhoneNumber(PhoneNumberSvc.GetNumbers(PhoneNumber), StartDate, EndDate);

                    Calls = new ObservableCollection<Call>(ro.OrderByDescending(x => x.CallStartTime));
                    if (Calls.Count > 0)
                    {
                        HeadingText = String.Format(" {0} has {1} calls", StringFormatSvc.PhoneNumberFormatted(PhoneNumber), Calls.Count());
                        ShowGridData = true;
                    }
                    else
                    {
                        HeadingText = "No calls";
                        ShowGridData = false;
                    }
                    LoadedOk = true;
                    
                }
            }
            catch (Exception e)
            {
                LoadFailed(e);
                HeadingText = e.Message;
            }
        }

        public bool AllCalls { get; set; }
    }

    //public class DialHistoryMessage
    //{
    //    public string PhoneNumber { get; set; }
    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //}
}


