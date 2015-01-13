
namespace PhoneLogic.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

#if SILVERLIGHT
    public class PhoneRoom : INotifyPropertyChanged
#else
    public class PhoneRoom
#endif
    {
        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                #if SILVERLIGHT
                NotifyPropertyChanged();
                #endif
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                #if SILVERLIGHT
                NotifyPropertyChanged();
                #endif
            }
        }

        public override string ToString()
        {
            return Code + ": " + Name;
        }
    }
}
