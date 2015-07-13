
namespace PhoneLogic.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PhoneRoom
    {
        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        private int _GMTOffset;
        public int GMTOffset
        {
            get { return _GMTOffset; }
            set
            {
                _GMTOffset = value;
            }
        }
        public override string ToString()
        {
            return Code + ": " + Name;
        }
    }
}
