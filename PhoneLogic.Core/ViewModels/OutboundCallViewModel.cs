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
        private string _phoneNumber;
        [Display(Name = "Phone")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                CanMakeCall = false;
                if (value != _phoneNumber)
                {
                    if (String.IsNullOrWhiteSpace(value))
                        throw new Exception("Phone number required");
                    if ((value.Length != 10))
                        throw new Exception("Phone number invalid");
                    _phoneNumber = value;
                    CanMakeCall = true;
                    NotifyPropertyChanged();
                }
            }
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

