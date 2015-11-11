using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneLogic.Model
{
    public class RecruiterTimeSummary
    {
        [Display(Name = "ReviewDate")]
        public System.DateTime ReviewDate { get; set; }

        [Display(Name = "Phone Room")]
        public String PhoneRoom { get; set; }

        [Display(Name = "Recruiter")]
        public String DisplayName { get; set; }

        [Display(Name = "Email")]
        public String EmailAddress { get; set; }

        [Display(Name = "Title")]
        public String Description { get; set; }
        [Display(Name = "SIP")]
        public string RecruiterSIP { get; set; }

        [Display(Name = "Inbound")]
        public int InboundCallCnt { get; set; }

        [Display(Name = "Outbound")]
        public int OutboundCallCnt { get; set; }

        [Display(Name = "Call Cnt")]
        public int CallCnt => OutboundCallCnt + InboundCallCnt;

        [Display(Name = "Call Time")]
        public TimeSpan CallTime { get; set; }
        [Display(Name = "Idle Time")]
        public TimeSpan IdleTime { get; set; }

        [Display(Name = "Call Time")]
        public string TotalCallDurationFormatted
        {
            get { return StringFormatSvc.DurationFormatted(CallTime); }
            
        }

        [Display(Name = "Idle Time")]
        public string TotalIdleDurationFormatted
        {
            get { return StringFormatSvc.DurationFormatted(IdleTime); }
        }

        [Display(Name = "Recruiter Activities")]
        public List<RecruiterActivity> RecruiterActivities { get; set; }

    }
}