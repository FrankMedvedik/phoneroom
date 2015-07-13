using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PhoneLogic.Model;
using PhoneLogic.Repository;
using PhoneLogic.UserAuth;

namespace PhoneLogic.Web.Controllers
{
    public class LyncCallByRecruitersController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();
        private PhoneRoomUsers p = new PhoneRoomUsers();

        // GET: api/LyncCallByRecruiter?sip=1&startDate=2&endDate=3
        public IEnumerable<LyncCallByRecruiter> GetLyncCallByRecruiters(string sip, DateTime startDate, DateTime endDate)
        {
            var Logs = db.rpt_GetLyncCallRecruiter(startDate,endDate).ToList();
            var recruiters = p.GetAllRecruiters();
                 var query = from l in Logs
                    join r in recruiters on l.RecruiterSIP equals r.sip
                    where r.sip == sip
                   select new LyncCallByRecruiter
                        {
                        PhoneRoom = r.PhoneRoom, 
                        DisplayName =  r.DisplayName,
                        EmailAddress = r.EmailAddress,
                        Description = r.Description,
                        RecruiterSIP  = l.RecruiterSIP,
                        JobCnt = l.JobCnt,
                        CallCnt = l.CallCnt,
                        IncomingCallCnt = l.IncomingCallCnt, 
                        OutgoingCallCnt = l.OutgoingCallCnt,
                        TotalCallDuration = l.TotalCallDuration, 
                        AvgCallDuration  = l.AvgCallDuration, 
                        MaxCallDuration = l.MaxCallDuration, 
                        CompleteCnt  = l.CompleteCnt, 
                        AbandondedCnt = l.AbandondedCnt, 
                        LeftMsgCnt  = l.LeftMsgCnt,
                        UniqueCallerCnt = l.UniqueCallerCnt, 
                        FirstCallTime  = l.FirstCallTime, 
                        LastCallTime = l.LastCallTime 
            };

            return query.ToList();
        }

        // GET: api/LyncCallByRecruiter?startDate=2&endDate=3
        public IEnumerable<LyncCallByRecruiter> GetLyncCallByRecruiters(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
            //var v = new List<RecruiterLyncCallLog>();
            //try
            //{
            //    var Logs = db.rpt_GetLyncCallLog(startDate, endDate).ToList();
            //    var recruiters = p.GetAllRecruiters();
            //    /* Add BACK CODE TO INCLUDE CALLS NOT ASSOCIATED TO RECRUITER */
                  
            //    var query = from l in Logs
            //                join r in recruiters on l.RecruiterSIP equals r.sip
            //                select new RecruiterLyncCallLog
            //                {
            //                    PhoneRoom = r.PhoneRoom,
            //                    DisplayName = r.DisplayName,
            //                    EmailAddress = r.EmailAddress,
            //                    Description = r.Description,
            //                    StartLogId = l.StartLogId,
            //                    CallId = l.CallID,
            //                    JobNumber = l.JobNumber,
            //                    CallerId = l.CallerId,
            //                    TollFreeNumber = l.TollFreeNumber,
            //                    CallStartTime = l.CallStartTime,
            //                    RecruiterConnectTime = l.RecruiterConnectTime,
            //                    CallEndTime = l.CallEndTime,
            //                    RecruiterSIP = l.RecruiterSIP,
            //                    CallDirection = l.CallDirection,
            //                    CallEndStatus = l.CallEndStatus
            //                };
            //    v = query.ToList();
            //}

            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //return v;
        }
   }
}
