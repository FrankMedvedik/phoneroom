using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using PhoneLogic.Repository;

namespace PhoneLogic.Web.Controllers
{
    public class PhoneLogicTasksRptController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();


        // GET: api/PhoneLogicTasksRpt?sip=sip:yourname@reckner.com
        public IEnumerable<rptMyPhoneLogicTask> GetPhoneLogicTask(string sip)
        {
            return db.rpt_getMyPhoneLogicTasks(sip).ToList();
        }
    }
}