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
        // GET: api/LyncCallByRecruiter?startDate=1&endDate=2
        public IEnumerable<LyncCallByRecruiter> GetLyncCallByRecruiters(DateTime startDate, DateTime endDate)
        {
            var Logs = db.rpt_GetLyncCallRecruiter(startDate, endDate).ToList();
            var recruiters = p.GetAllRecruiters();
            
            var query = from l in Logs 
                        join r in recruiters  on l.RecruiterSIP equals r.sip into rr 
                         from oj in rr.DefaultIfEmpty()
                        select new LyncCallByRecruiter
                        {
                        PhoneRoom = (oj == null ? String.Empty : oj.PhoneRoom),
                        DisplayName = (oj == null ? String.Empty : oj.DisplayName),
                        EmailAddress = (oj == null ? String.Empty : oj.EmailAddress),
                        Description = (oj == null ? String.Empty : oj.Description),
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
   }
}
