namespace PhoneLogic.Model
{
    using System.ComponentModel;
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

#if SILVERLIGHT
    public class returnedCall : INotifyPropertyChanged
#else
    public class returnedCall
#endif
    {
        public int callbackID { get; set; }
        public string SIP { get; set; }
        public DateTime startCallDate { get; set; }
        public Nullable<DateTime> endCallDate { get; set; }
        public string phoneNum { get; set; }
    }
}
