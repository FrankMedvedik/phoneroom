using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PhoneLogic.Model;
using PhoneLogic.Repository;
using PhoneLogic.UserAuth;

namespace PhoneLogic.Web.Controllers
{
    public class RecruiterController : ApiController
    {
        private PhoneRoomUsers p = new PhoneRoomUsers();
        private PhoneLogicEntities db = new PhoneLogicEntities();
        // GET: api/Recruiters
        public IEnumerable<Recruiter> Get()
        {

            return p.GetAllRecruiters();
        }
        public IEnumerable<Recruiter> Get(Int64 startDateTicks, Int64 endDateTicks)
        {
            var arList = db.rpt_GetActiveRecruiters(new DateTime(startDateTicks), new DateTime(endDateTicks));
            var rlist = p.GetAllRecruiters();
            var query = from l in arList
                        join r in rlist on l.RecruiterSIP equals r.sip
                        select new Recruiter
                        {
                            PhoneRoom = r.PhoneRoom,
                            DisplayName = r.DisplayName,
                            EmailAddress = r.EmailAddress,
                            Description = r.Description,
                            CallCnt = l.CallCnt.GetValueOrDefault(0)
                        };

            return query.ToList();
        }
    }
}
