using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using PhoneLogic.Repository;

namespace PhoneLogic.Web.Controllers
{
    public class PhoneLogicTasksController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();


        // GET: api/PhoneLogicTask?job=1&task=1
        [ResponseType(typeof(PhoneLogicTaskDTO))]
        public  IEnumerable<PhoneLogicTaskDTO> GetPhoneLogicTask(string jobNum, int taskId)
        {
            var phoneLogicTask = db.GetPhoneLogicTask(jobNum, taskId);
            return phoneLogicTask;
        }


        // GET: api/PhoneLogicTask?sip=sip:yourname@reckner.com
        public IEnumerable<PhoneLogicTaskDTO> GetPhoneLogicTask(string sip)
        {
            return db.GetMyPhoneLogicTasks(sip).ToList();
        }

        // GET: api/PhoneLogicTask/InboundCallsByHour?startDate=1&endDate=2
        [ResponseType(typeof(rptInboundCallByHourResult))]
        public IEnumerable<rptInboundCallByHourResult> GetInboundCallsByHour(DateTime startDate, DateTime endDate)
        {
            return db.rpt_InboundCallByHour(startDate, endDate);
        }
    }
}