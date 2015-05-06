namespace PhoneLogic.Model
{
    using System.ComponentModel;
    using System;

    public class returnedCall
    {
        public int callbackID { get; set; }
        public string SIP { get; set; }
        public DateTime startCallDate { get; set; }
        public Nullable<DateTime> endCallDate { get; set; }
        public string phoneNum { get; set; }
    }
}
