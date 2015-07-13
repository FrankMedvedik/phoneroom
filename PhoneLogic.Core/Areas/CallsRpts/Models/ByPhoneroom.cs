using System;

namespace PhoneLogic.Core.areas.CallsRpts.Models
{
    public class ByPhoneroom
    {
        public String JobCnt { get; set; }
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
        public int TollFreeNumCnt { get; set; }
        public DateTime FirstCallTime { get; set; }
        public DateTime LastCallTime { get; set; }
        public DateTime MsgLeftCnt { get; set; }

    }
}