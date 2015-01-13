using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PhoneLogic.Repository;

namespace PhoneLogic.Web.Controllers
{
    public class AgentLogsController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();

        // GET: api/agentLogs?sip=1&startDate=2&endDate=3
        public List<agentLogDTO> GetAgentLogs(string sip, DateTime startDate, DateTime endDate)
        {
            return db.rpt_GetAgentLog(sip,startDate,endDate).ToList();
        }
   }
}