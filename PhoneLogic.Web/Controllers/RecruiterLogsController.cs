using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PhoneLogic.Repository;

namespace PhoneLogic.Web.Controllers
{
    public class RecruiterLogsController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();

        // GET: api/recruiterLogs?sip=1&startDate=2&endDate=3
        public List<recruiterLogDTO> GetRecruiterLogs(string sip, DateTime startDate, DateTime endDate)
        {
            return db.rpt_GetRecruiterLog(sip,startDate,endDate).ToList();
        }
   }
}