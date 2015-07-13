using System;

namespace PhoneLogic.Model
{
    public class LyncCallSummaryByJob
    {

        public String JobNum { get; set; }
        public int CallCnt { get; set; }
        public int InboundCallCnt { get; set; }
        public int OutboundCallCnt { get; set; }
        public TimeSpan TotalCallDuration { get; set; }
        public TimeSpan AvgCallDuration { get; set; }
        public TimeSpan MaxCallDuration { get; set; }
        public int RecruiterCnt { get; set; }
        public int UniqueCallerCnt { get; set; }
        public int CompleteCnt { get; set; }
        public int AbandondedCnt { get; set; }
        public int TollFreeNumCnt { get; set; }
        public DateTime FirstCallDate { get; set; }
        public DateTime LastCallDate { get; set; }
        public DateTime MsgLeftCnt { get; set; }

    }

    public class LyncCallSummaryByRecruiter
    {
        public String DisplayName { get; set; }
        public int CallCnt { get; set; }
        public int InboundCallCnt { get; set; }
        public int OutboundCallCnt { get; set; }
        public TimeSpan TotalCallDuration { get; set; }
        public TimeSpan AvgCallDuration { get; set; }
        public TimeSpan MaxCallDuration { get; set; }
        public int JobCnt { get; set; }
        public int UniqueCallerCnt { get; set; }
        public int CompleteCnt { get; set; }
        public int AbandondedCnt { get; set; }
        public DateTime FirstCallDate { get; set; }
        public DateTime LastCallDate { get; set; }

    }

    public class LyncCallSummaryByJobsForRecruiter
    {
        public String JobNum { get; set; }
        public int CallCnt { get; set; }
        public int InboundCallCnt { get; set; }
        public int OutboundCallCnt { get; set; }
        public TimeSpan TotalCallDuration { get; set; }
        public TimeSpan AvgCallDuration { get; set; }
        public TimeSpan MaxCallDuration { get; set; }
        public int UniqueCallerCnt { get; set; }
        public int CompleteCnt { get; set; }
        public int AbandondedCnt { get; set; }
        public DateTime FirstCallDate { get; set; }
        public DateTime LastCallDate { get; set; }

    }


    public class LyncCallSummaryByRecruitersForJob
    {
        public String DisplayName { get; set; }
        public int CallCnt { get; set; }
        public int InboundCallCnt { get; set; }
        public int OutboundCallCnt { get; set; }
        public TimeSpan TotalCallDuration { get; set; }
        public TimeSpan AvgCallDuration { get; set; }
        public TimeSpan MaxCallDuration { get; set; }
        public int UniqueCallerCnt { get; set; }
        public int CompleteCnt { get; set; }
        public int AbandondedCnt { get; set; }
        public DateTime FirstCallDate { get; set; }
        public DateTime LastCallDate { get; set; }
    }
}

