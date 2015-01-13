using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using PhoneLogic.Repository;

namespace PhoneLogic.Web.Controllers
{
    public class InboundCallRptController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();

        // GET: api/inboundCallRpt/InboundCallsByHour?startDate=1&endDate=2
        [ResponseType(typeof(rptInboundCallByHourResult))]
        public IEnumerable<rptInboundCallByHourResult> GetInboundCallsByHour(DateTime startDate, DateTime endDate)
        {
            return db.rpt_InboundCallByHour(startDate, endDate);
        }
    }
}