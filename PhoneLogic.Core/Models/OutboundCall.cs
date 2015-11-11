using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using PhoneNumbers;
using Silverlight.Base;

namespace PhoneLogic.Core.Models
{

    public class OutboundCall 
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        private string _phoneNumber;
        [Display(Name = "Phone")] 
        public string PhoneNumber { 
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

        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            var e = PropertyChanged;
            if (e != null) e(this, new PropertyChangedEventArgs(name));
        }

    }
}
