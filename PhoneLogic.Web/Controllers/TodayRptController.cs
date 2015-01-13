using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using PhoneLogic.Repository;


namespace PhoneLogic.Web.Controllers
{
    public class TodayRptController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();
        // GET: api/Rpt/TodayCallLogSummary
        [ResponseType(typeof(rptGetTodayCallLogSummaryResult))]
        public IEnumerable<rptGetTodayCallLogSummaryResult> GetTodayCallLogSummary()
        {
            return db.rpt_GetTodayCallLogSummary();
        }
    
    }
}