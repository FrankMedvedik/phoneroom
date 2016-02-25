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
    public class OpenCallbacksController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();

        // GET: api/OpenCallbacks
        public List<CallbackRpt> GetOpenCallbacks()
        {
            var l = db.rpt_GetOpenCallbackRpt().ToList();
            return  l;
        }

        // GET: api/OpenCallbacks?jobNum=1&taskId=2
        public List<callbackDTO> GetJobCallbacks(string jobNum, string taskId)
        {
            List<callbackDTO> list = db.rpt_GetOpenJobCallbacks(jobNum, taskId).ToList();
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

    }
}