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
    public class PhoneroomRecruiterJobsController : ApiController
    {
        /* returns the univers of jobs and recruiters */
        // GET: api/PhoneRoomJobs?startDate=1&endDate=2
        public IEnumerable<PhoneroomRecruiterJob> GetPhoneRoomRecruiterJobs()
        {
            PhoneLogicEntities db = new PhoneLogicEntities();
            PhoneRoomUsers p = new PhoneRoomUsers();

            var jobRecruiters = db.vw_PhoneLogicTaskAgent.ToList();
            var recruiters = p.GetAllRecruiters();

            var query = from l in jobRecruiters
                join r in recruiters on l.sip equals r.sip into rr
                from oj in rr.DefaultIfEmpty()
                     select new PhoneroomRecruiterJob
                {
                    PhoneRoom = (oj == null ? String.Empty : oj.PhoneRoom),
                    DisplayName = (oj == null ? String.Empty : oj.DisplayName),
                    EmailAddress = (oj == null ? String.Empty : oj.EmailAddress),
                    Description = (oj == null ? String.Empty : oj.Description),
                    jobNum = l.jobNum,
                    sip = l.sip,
                    taskID = l.taskID,
                    taskDscr = l.taskDscr,
                    taskName = l.taskName,
                    taskTypeID = l.taskTypeID,
                    tollFreeNumber = l.tollFreeNumber
                };
            var z = query.ToList();
            return z;
        }

    }
}