using System;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Model;
using PhoneLogic.Model.Models;
using PhoneLogic.ViewContracts.MVVMMessenger;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.Areas.Recruiters
{
    public class OutboundCallViewModel : ViewModelBase
    {
        public OutboundCallViewModel()
        {
            CanMakeCall = false;
            Messenger.Default.Register<NotificationMessage<PhoneLogicTask>>(this, message =>
            {
                if (message.Notification == Notifications.MySelectedPhoneLogicTaskChanged)
                {
                    Task = message.Content;
                }
            });
        }
        private Boolean _canMakeCall;
        public Boolean CanMakeCall
        {
            get { return _canMakeCall; }
            set
            {
                if (value != _canMakeCall)
                {
                    _canMakeCall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (value != _phoneNumber)
                {
                    _phoneNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private PhoneLogicTask _task;
        public PhoneLogicTask Task
        {
            get { return _task; }
            set
            {
                    _task = value;
                    NotifyPropertyChanged();
                    CanMakeCall = true;
                }
                
            }
        }
        
        
    }


