using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PhoneLogic.Model;
using PhoneLogic.Repository;
using PhoneLogic.UserAuth;

namespace PhoneLogic.Web.Controllers
{
    public class LyncCallJobsController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();
        private PhoneRoomUsers p = new PhoneRoomUsers();

        // GET: api/LyncCallJobs?sip=1&startDate=2&endDate=3
        public IEnumerable<LyncCallJob> GetLyncCallJobs(string sip, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();

            //var Logs = db.rpt_GetLyncCallJobs(sip,startDate,endDate).ToList();
            //var recruiters = p.GetAllRecruiters();
            //     var query = from l in Logs
            //        join r in recruiters on l.RecruiterSIP equals r.sip
            //                 select new LyncCallJobs
            //        {
            //            PhoneRoom = r.PhoneRoom, 
            //            DisplayName =  r.DisplayName,
            //            EmailAddress = r.EmailAddress,
            //            Description = r.Description,
            //            StartLogId = l.StartLogId,
            //            CallId = l.CallID,
            //            JobNumber = l.JobNumber,
            //            CallerId = l.CallerId,
            //            TollFreeNumber = l.TollFreeNumber,
            //            CallStartTime = l.CallStartTime,
            //            RecruiterConnectTime  = l.RecruiterConnectTime ,
            //            CallEndTime = l.CallEndTime,
            //            RecruiterSIP = l.RecruiterSIP,
            //            CallDirection = l.CallDirection,
            //            CallEndStatus = l.CallEndStatus
            //                        };

            //return query.ToList();
        }

        // GET: api/LyncCallJobs?startDate=2&endDate=3
        public IEnumerable<LyncCallJob> GetLyncCallJobs(DateTime startDate, DateTime endDate)
        {
            var v = new List<LyncCallJob>();
            try
            {
                v  = db.rpt_GetLyncCallJob(startDate, endDate).ToList();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return v;
        }
   }
}


