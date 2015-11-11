using System;

namespace PhoneLogic.CallRpt.Model
{
    public class CallRptSettings
    {
        public DateTime StartRptDate {get; set;}
        public DateTime EndRptDate { get; set; }
        public string SelectedPhoneRoomName {get; set;}
        public string SelectedJobNum {get; set;}
        public string SelectedRecruiter {get; set;}
        public string SelectedRecruiterJob {get; set;}
        public string SelectedJobRecruiter {get; set;}

    }
}
