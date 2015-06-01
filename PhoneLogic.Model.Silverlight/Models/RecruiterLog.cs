
namespace PhoneLogic.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class RecruiterLog
    {
        public int StartLogId { get; set; }
        public string JobNumber { get; set; }
        [Display(Name = "Caller ID")]
        public string callerId { get; set; }
        public string callId { get; set; }
        public System.DateTime callStartTime { get; set; }
        public System.DateTime recruiterConnectTime { get; set; }
        [Display(Name = "Wait Time")]
        public string preconnectDuration { get; set; }
        [Display(Name = "Call Length")]
        public string recruiterCallDuration { get; set; }
        public string recruiterSip { get; set; }
        public string totalCallDuration { get; set; }
        public System.DateTime callEndTime { get; set; }
        public string tollFreeNumber { get; set; }

        [Display(Name = "Job")]
        public string jobFormatted
        {
            get
            {
                return (String.IsNullOrWhiteSpace(JobNumber)) ? JobNumber : JobNumber.Substring(0, 4) + "-" + JobNumber.Substring(4, 4);
            }
        }
 
        [Display(Name = "Toll Free #")]
        public string tollFreeNumberFormatted
        {
            get
            {
                return (String.IsNullOrWhiteSpace(tollFreeNumber)) ? tollFreeNumber : String.Format("{0:(###) ###-####}", double.Parse(tollFreeNumber));
            }
        }
        [Display(Name = "Phone #")]
        public string phoneFormatted
        {
            get
            {
                return (String.IsNullOrWhiteSpace(callerId)) ? callerId : String.Format("{0:(###) ###-####}", double.Parse(callerId));
            }
        }
    }
}


