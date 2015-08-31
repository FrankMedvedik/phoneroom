using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using PhoneLogic.Repository;


namespace PhoneLogic.Web.Controllers
{
    public class JobActivityRptController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();
        // GET: api/JobCallActivityRpt?startDate=1&endDate=2
        [ResponseType(typeof(rptGetJobCallActivityResult))]
        public IEnumerable<rptGetJobCallActivityResult> GetJobActivityRpt(Int64 startDateTicks, Int64 endDateTicks)
        {
            return  db.rpt_GetJobCallActivity(new DateTime(startDateTicks), new DateTime(endDateTicks));
        }

    }
}