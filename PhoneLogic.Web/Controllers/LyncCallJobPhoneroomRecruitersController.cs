using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PhoneLogic.Model;
using PhoneLogic.Repository;
using PhoneLogic.UserAuth;

namespace PhoneLogic.Web.Controllers
{
    public class LyncCallJobPhoneroomRecruitersController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();
        private PhoneRoomUsers p = new PhoneRoomUsers();

        // GET: api/LyncCallJobPhoneRoomRecruiters?job=1&startDate=2&endDate=3
        public IEnumerable<LyncCallJobPhoneRoomRecruiter> GetLyncCallJobPhoneRoomRecruiters(string job,
            DateTime startDate, DateTime endDate)
        {
            var JobRecruiters = db.rpt_GetLyncCallJobRecruiters(job, startDate, endDate).ToList();
            var recruiters = p.GetAllRecruiters();
            var query = from l in JobRecruiters
                join r in recruiters on l.recruiterSip equals r.sip into rr
                from oj in rr.DefaultIfEmpty()
                select new LyncCallJobPhoneRoomRecruiter
                {
                    PhoneRoom =  (oj == null ? String.Empty : oj.PhoneRoom),
                    DisplayName = (oj == null ? String.Empty : oj.DisplayName),
                    EmailAddress = (oj == null ? String.Empty : oj.EmailAddress),
                    Description = (oj == null ? String.Empty : oj.Description),
                    recruiterSip = l.recruiterSip,
                    CallCnt = l.CallCnt,
                    IncomingCallCnt = l.IncomingCallCnt,
                    OutgoingCallCnt = l.OutgoingCallCnt,
                    TotalCallDuration = l.TotalCallDuration,
                    AvgCallDuration = l.AvgCallDuration,
                    MaxCallDuration = l.MaxCallDuration,
                    UniqueCallerCnt = l.UniqueCallerCnt,
                    CompleteCnt = l.CompleteCnt,
                    AbandonedCnt = l.AbandonedCnt,
                    LeftMsgCnt = l.LeftMsgCnt,
                    FirstCallTime = l.FirstCallTime,
                    LastCallTime = l.LastCallTime,
                    JobNumber = l.JobNumber
                };

            var z = query.ToList();
            return z;
        }

    }
}