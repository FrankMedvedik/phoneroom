using System;
using System.ComponentModel.DataAnnotations;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.CallsRpts.Models
{
    public class ByJobForRecruiter
    {
        public String JobNum { get; set; }
        public int CallCnt { get; set; }
        public int IncomingCallCnt { get; set; }
        public int OutgoingCallCnt { get; set; }
        public TimeSpan TotalCallDuration { get; set; }
        public TimeSpan AvgCallDuration { get; set; }
        public TimeSpan MaxCallDuration { get; set; }
        public int RecruiterCnt { get; set; }
        public int UniqueCallerCnt { get; set; }
        public int CompleteCnt { get; set; }
        public int AbandondedCnt { get; set; }
        public int LeftMsgCnt { get; set; }
        public String TollFreeNumber { get; set; }
        [Display(Name = "Toll Free #")]
        public string TollFreeFormatted
        {
            get
            {
                return StringFormatSvc.PhoneNumberFormatted(TollFreeNumber);
            }
        }

        public DateTime FirstCallTime { get; set; }
        public DateTime LastCallTime { get; set; }

    }
}