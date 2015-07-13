//using System;
//using System.Collections.Generic;
//using System.Linq;
//using PhoneLogic.Core.areas.CallsRpts.Models;
//using PhoneLogic.Model;

//namespace PhoneLogic.Core.Areas.CallsRpts
//{
//    public static class CallSummarySvc
//    {
//        public static List<ByJob> RptByJob(IEnumerable<Call> calls, String PhoneRoom)
//        {
            
//            var JobsQuery = calls
//                //.Where(w=> w.PhoneRoom == PhoneRoom || w.PhoneRoom == "")
//                .GroupBy(w => w.JobFormatted)
//                .Select(g => new ByJob
//               { 
//                    JobNum = g.Key, 
//                    CallCnt = g.Count(),
//                    InboundCallCnt = g.Count(),  // need embedded where to identifiy inbound
//                    OutboundCallCnt =  g.Count(), // need embedded where to identifiy inbound
//                    TotalCallDuration = g.Sum(g => g.CallEndTime.s - g.CallStartTime),
//        AvgCallDuration { get; set; }
//        MaxCallDuration { get; set; }
//        RecruiterCnt { get; set; }
//        UniqueCallerCnt { get; set; }
//        CompleteCnt { get; set; }
//        AbandondedCnt { get; set; }
//        TollFreeNumCnt { get; set; }
//        FirstCallDate { get; set; }
//        LastCallDate { get; set; }
//        MsgLeftCnt { get; set; }
//               });
//            var a = JobsQuery.ToList();
//            return a;
//        }

//        public static List<ByRecruiter> RptByRecruiter(List<Call> c, String PhoneRoom)
//        {
//            return new List<ByRecruiter>();
//        }
        
//        public static List<ByJobsForRecruiter> RptByJobForRecruiter(List<Call> c, String PhoneRoom, String RecruiterSIP)
//        {
//            return new List<ByJobsForRecruiter>();
//        }

//        public static List<ByRecruitersForJob> RptByRecruitersForJob(List<Call> c, String PhoneRoom, String JobNumberFormatted)
//        {

//            return new List<ByRecruitersForJob>();
//        }
//    }
//}
