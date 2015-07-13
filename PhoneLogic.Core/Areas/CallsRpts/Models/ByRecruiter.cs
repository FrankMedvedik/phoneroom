using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneLogic.Core.areas.CallsRpts.Models
{
    public class ByRecruiter
    {
        [Display(Name = "Phone Room")]
        public String PhoneRoom { get; set; }
        [Display(Name = "Recruiter")]
        public String DisplayName { get; set; }
        [Display(Name = "Email")]
        public String EmailAddress { get; set; }
        [Display(Name = "Title")]
        public int JobCnt { get; set; }
        public int CallCnt { get; set; }
        public int IncomingCallCnt { get; set; }
        public int OutgoingCallCnt { get; set; }
        public TimeSpan TotalCallDuration { get; set; }
        public TimeSpan AvgCallDuration { get; set; }
        public TimeSpan MaxCallDuration { get; set; }
        public int UniqueCallerCnt { get; set; }
        public int CompleteCnt { get; set; }
        public int AbandondedCnt { get; set; }
        public int LeftMsgCnt { get; set; }
        public DateTime FirstCallTime { get; set; }
        public DateTime LastCallTime{ get; set; }

    }
}