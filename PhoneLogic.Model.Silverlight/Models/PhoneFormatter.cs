﻿
namespace PhoneLogic.Model
{
    using System.Text;
    using System;
    using System.Collections.Generic;

#if SILVERLIGHT
    using System.ComponentModel;
    public class Phone : INotifyPropertyChanged
#else
    public class PhoneFormatter
#endif
    {
        public static String StripPhoneNumber(string PhoneNumber)
        {
            {
                if (PhoneNumber == null)
                    return "";
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (char c in PhoneNumber)
                    {
                        if (c >= '0' && c <= '9')
                        { sb.Append(c); }
                    }
                    return sb.ToString();
                }
            }
        }
        private string _inputPhoneNumber;
        public string inputPhoneNumber 
        {
            get { return _inputPhoneNumber; }
            set
            {
                _inputPhoneNumber= StripPhoneNumber(value);
#if SILVERLIGHT
            NotifyPropertyChanged("inputPhoneNumber");
#endif
            }
        }
        private string _outputPhoneNumber;
        public string outputPhoneNumber 
        {
            get { return _outputPhoneNumber; }
            set
            {
                _outputPhoneNumber  = value;
#if SILVERLIGHT
            NotifyPropertyChanged("outputPhoneNumber");
#endif
            }   
        }
                
        private string _formatterType;
        public string formatterType
        {
            get { return _formatterType; }
            set
            {
                _formatterType = value;
#if SILVERLIGHT
            NotifyPropertyChanged("formatterType");
#endif
            }
        }
    }
}