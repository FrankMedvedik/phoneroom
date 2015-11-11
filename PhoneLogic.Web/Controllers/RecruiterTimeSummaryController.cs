using System;
using System.Collections.Generic;
using System.Web.Http;
using PhoneLogic.CallBusinessIntelligence;
using PhoneLogic.Model;

namespace PhoneLogic.Web.Controllers
{
    public class RecruiterTimeSummaryController : ApiController
    {
        private RecruiterUtilization r = new RecruiterUtilization();
        // GET: api/RecruiterTimeSummary?startDate=0&endDate=1&sip=2
        public RecruiterTimeSummary Get(Int64 startDate, Int64 endDate, string sip)
        {
            return r.GetRecruiterTimeSummary(sip, new DateTime(startDate), new DateTime(endDate));
        }
        // GET: api/RecruiterTimeSummary?phoneRoom=0&startDate=1&endDate=2
        public IEnumerable<RecruiterTimeSummary> Get(string phoneroom, Int64 startDate, Int64 endDate)
        {
            return r.PhoneroomTimeSummary(phoneroom, new DateTime(startDate), new DateTime(endDate));
        }
    }
}
