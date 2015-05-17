using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Lync.Model;
using Newtonsoft.Json;
using PhoneLogic.Core.Models;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.ViewModels
{
    public class OutboundCallViewModel : ViewModelBase
    {
   
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

