using System;
using System.Windows;
using PhoneLogic.Model;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.ViewModels
{
    public class OutboundCallViewModel : ViewModelBase
    {
        public OutboundCallViewModel()
        {
            CanMakeCall = true;
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
        private PhoneLogicTask _task;
        public PhoneLogicTask Task
        {
            get { return _task; }
            set
            {
                if (_task != value)
                {
                    _task = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        
    }
}

