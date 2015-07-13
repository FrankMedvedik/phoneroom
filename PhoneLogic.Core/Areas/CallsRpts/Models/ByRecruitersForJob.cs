using System;

namespace PhoneLogic.Core.areas.CallsRpts.Models
{
    public class ByRecruitersForJob
    {
        public String DisplayName { get; set; }
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
        public DateTime LastCallTime { get; set; }
    }
}

