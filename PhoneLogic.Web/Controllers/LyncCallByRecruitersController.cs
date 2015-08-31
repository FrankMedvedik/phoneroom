using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhoneLogic.Model;
using PhoneLogic.Repository;
using PhoneLogic.UserAuth;

namespace PhoneLogic.Web.Controllers
{
    public class LyncCallByRecruitersController : ApiController
    {            

        // GET: api/LyncCallByRecruiters?sip=1&startDate=2&endDate=3
        public IEnumerable<LyncCallByRecruiter> GetLyncCallByRecruiters(string sip, Int64 startDate, Int64 endDate)
        {   
            
            PhoneLogicEntities db = new PhoneLogicEntities();
            PhoneRoomUsers p = new PhoneRoomUsers();
            var logs = db.rpt_GetLyncCallRecruiters(new DateTime(startDate), new DateTime(endDate)).ToList();
            var recruiters = p.GetAllRecruiters();
            if (recruiters == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("recruiters list is null"),
                    ReasonPhrase = "GetLyncCallByRecruiters"
                };
                throw new HttpResponseException(resp);
            }

            if (!logs.Any())
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("database phone list is null"),
                    ReasonPhrase = "GetLyncCallByRecruiters"
                };
                throw new HttpResponseException(resp);
            }

                 var query = from l in logs
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
        // GET: api/LyncCallByRecruiters?startDate=1&endDate=2&primaryEntity=3
        public IEnumerable<LyncCallByRecruiter> GetLyncCallByRecruiters(Int64 startDate, Int64 endDate, string primaryEntity)
        {
            PhoneRoomUsers p;
            List<LyncCallRecruiter> logs;

            using (PhoneLogicEntities db = new PhoneLogicEntities())
            {
                p = new PhoneRoomUsers();

                logs = db.rpt_GetLyncCallRecruiters(new DateTime(startDate), new DateTime(endDate)).ToList();
            }
            var recruiters = p.GetAllRecruiters();
            IEnumerable<LyncCallByRecruiter> lst = new List<LyncCallByRecruiter>();

            if (primaryEntity == "Recruiter")
            {

                var query = from r in recruiters
                    join l in logs on r.sip equals l.RecruiterSIP into rr
                    from oj in rr.DefaultIfEmpty()
                    select new LyncCallByRecruiter
                    {
                        PhoneRoom = r.PhoneRoom,
                        DisplayName = r.DisplayName,
                        EmailAddress = r.EmailAddress,
                        Description = r.Description,
                        RecruiterSIP = r.sip,
                        JobCnt = (oj == null ? 0 : oj.JobCnt),
                        CallCnt = (oj == null ? 0 : oj.CallCnt),
                        IncomingCallCnt = (oj == null ? 0 : oj.IncomingCallCnt),
                        OutgoingCallCnt = (oj == null ? 0 : oj.OutgoingCallCnt),
                        TotalCallDuration = (oj == null ? 0 : oj.TotalCallDuration),
                        AvgCallDuration = (oj == null ? 0 : oj.AvgCallDuration),
                        MaxCallDuration = (oj == null ? 0 : oj.MaxCallDuration),
                        CompleteCnt = (oj == null ? 0 : oj.CompleteCnt),
                        AbandondedCnt = (oj == null ? 0 : oj.AbandondedCnt),
                        LeftMsgCnt = (oj == null ? 0 : oj.LeftMsgCnt),
                        UniqueCallerCnt = (oj == null ? 0 : oj.UniqueCallerCnt),
                        FirstCallTime = (oj == null ? null : oj.FirstCallTime),
                        LastCallTime = (oj == null ? null : oj.LastCallTime)
                    };
                lst = query.ToList();
            }
            if (primaryEntity == "Log")
            {
                var query = from l in logs
                    join r in recruiters on l.RecruiterSIP equals r.sip into rr
                    from oj in rr.DefaultIfEmpty()
                    select new LyncCallByRecruiter
                    {
                        PhoneRoom = (oj == null ? String.Empty : oj.PhoneRoom),
                        DisplayName = (oj == null ? String.Empty : oj.DisplayName),
                        EmailAddress = (oj == null ? String.Empty : oj.EmailAddress),
                        Description = (oj == null ? String.Empty : oj.Description),
                        RecruiterSIP = l.RecruiterSIP,
                        JobCnt = l.JobCnt,
                        CallCnt = l.CallCnt,
                        IncomingCallCnt = l.IncomingCallCnt,
                        OutgoingCallCnt = l.OutgoingCallCnt,
                        TotalCallDuration = l.TotalCallDuration,
                        AvgCallDuration = l.AvgCallDuration,
                        MaxCallDuration = l.MaxCallDuration,
                        CompleteCnt = l.CompleteCnt,
                        AbandondedCnt = l.AbandondedCnt,
                        LeftMsgCnt = l.LeftMsgCnt,
                        UniqueCallerCnt = l.UniqueCallerCnt,
                        FirstCallTime = l.FirstCallTime,
                        LastCallTime = l.LastCallTime
                    };
                lst = query.ToList();
            }

            return lst;

        }

            
   }
}
