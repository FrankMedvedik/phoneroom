namespace PhoneLogic.Model
{
    using System.ComponentModel;
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


#if SILVERLIGHT
    public class callback : INotifyPropertyChanged

#else
    public class CallbackDto
#endif
{
        public int callbackID { get; set; }
        public string jobNum { get; set; }
        public int taskID { get; set; }
        public string phoneNum { get; set; }
        public string msgScr { get; set; }
        public DateTime timeEntered { get; set; }
        public DateTime? statusDate { get; set; }
        public int? msgLength { get; set; }
        public string status { get; set; }
        public string SIP { get; set; }
        public string EventDesc { get; set; }
    }
}
