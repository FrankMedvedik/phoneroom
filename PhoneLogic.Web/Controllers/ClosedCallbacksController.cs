using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PhoneLogic.Model;
using PhoneLogic.Repository;

namespace PhoneLogic.Web.Controllers
{
    public class ClosedCallbacksController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();

        //// GET: api/ClosedCallbacks?startDate=1&endDate=2
        public List<CallbackRpt> GetClosedCallbacks(Int64 startDate, Int64 endDate)
        {   
            var l = db.rpt_GetClosedCallbackRpt(new DateTime(startDate), new DateTime(endDate)).ToList();
            return l;
        }

        // GET: api/Callbacks?jobNum=1&taskId=2&startDate=3&endDate=4
        public List<callbackDTO> GetClosedJobCallbacks(string jobNum, string taskId, Int64 startDate, Int64 endDate)
        {
            List<callbackDTO> list = db.rpt_GetClosedJobCallbacks(jobNum, taskId, new DateTime(startDate), new DateTime(endDate)).ToList();
            return list;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool callbackExists(int id)
        {
            return db.callbacks.Count(e => e.callbackID == id) > 0;
        }
    }
}