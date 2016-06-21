
namespace PhoneLogic.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Recruiter
    {
        [Display(Name = "SIP")]
        public string sip
        {
            get
            {
                return "sip:" + EmailAddress;
            }
        }
        [Display(Name = "Phone Room")]
        public String PhoneRoom { get; set; }
        [Display(Name = "Name")]
        public String DisplayName { get; set; }
        [Display(Name = "Email")]
        public String EmailAddress { get; set; }
        [Display(Name = "Title")]
        public String Description { get; set; }
        [Display(Name = "CallCnt")]
        public int CallCnt { get; set; }


        //[Display(Name = "Out")]
        //public int OutboundCallCnt{ get; set; }
        //[Display(Name = "In")]
        //public int InboundCallCnt { get; set; }
        //[Display(Name = "Total Idle Time")]
        //public Double TotalIdleTime { get; set; }
        //[Display(Name = "Avg Idle Time")]
        //public Double AvgIdleTime { get; set; }
        //[Display(Name = "Max Idle Time")]
        //public Double MaxIdleTime { get; set; }
    }
}

