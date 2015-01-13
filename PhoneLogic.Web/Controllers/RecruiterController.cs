using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhoneLogic.Model;
using PhoneLogic.UserAuth;

namespace PhoneLogic.Web.Controllers
{
    public class RecruiterController : ApiController
    {
        private PhoneRoomUsers p = new PhoneRoomUsers();

        // GET: api/Recruiters
        public IEnumerable<Recruiter> Get()
        {
            return p.GetAllRecruiters();
        }
    }
}
