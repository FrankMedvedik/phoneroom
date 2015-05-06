
namespace PhoneLogic.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class RecruiterLog
    {
        
        public string recruiterSip { get; set; }
        public string callId { get; set; }
        public string callerid { get; set; }
        public string tollFreeNumber { get; set; }
        [Display(Name = "Event Time")]
        public System.DateTime eventTime { get; set; }
        public string jobnumber { get; set; }
        public int logid { get; set; }
        [Display(Name = "Job")]
        public string jobFormatted
        {
            get
            {
                return (String.IsNullOrWhiteSpace(jobnumber)) ? jobnumber : jobnumber.Substring(0, 4) + "-" + jobnumber.Substring(4,4);
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
                return (String.IsNullOrWhiteSpace(callerid)) ? callerid : String.Format("{0:(###) ###-####}", double.Parse(callerid));
            }
        }
    }
}


