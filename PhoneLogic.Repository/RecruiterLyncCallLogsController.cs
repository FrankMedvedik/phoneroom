using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PhoneLogic.Model;
using PhoneLogic.Repository;
using PhoneLogic.UserAuth;

namespace PhoneLogic.Web.Controllers
{
    public class RecruiterLyncCallLogsController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();
        private PhoneRoomUsers p = new PhoneRoomUsers();

        // GET: api/RecruiterLyncCallLog?sip=1&startDate=2&endDate=3
        public IEnumerable<RecruiterLyncCallLog> GetRecruiterCallLogs(string sip, DateTime startDate, DateTime endDate)
        {
            var Logs = db.rpt_GetRecruiterLyncCallLog(sip,startDate,endDate).ToList();
            var recruiters = p.GetAllRecruiters();
                 var query = from l in Logs
                    join r in recruiters on l.RecruiterSIP equals r.sip
                             select new RecruiterLyncCallLog
                    {
                        PhoneRoom = r.PhoneRoom, 
                        DisplayName =  r.DisplayName,
                        EmailAddress = r.EmailAddress,
                        Description = r.Description,
                        StartLogId = l.StartLogId,
                        CallId = l.CallID,
                        JobNumber = l.JobNumber,
                        CallerId = l.CallerId,
                        TollFreeNumber = l.TollFreeNumber,
                        CallStartTime = l.CallStartTime,
                        RecruiterConnectTime  = l.RecruiterConnectTime ,
                        CallEndTime = l.CallEndTime,
                        RecruiterSIP = l.RecruiterSIP,
                        CallDirection = l.CallDirection,
                        CallEndStatus = l.CallEndStatus
                                    };

            return query.ToList();
        }

        // GET: api/RecruiterLyncCallLog?startDate=2&endDate=3
        public IEnumerable<RecruiterLyncCallLog> GetRecruiterCallLogs(DateTime startDate, DateTime endDate)
        {
            var v = new List<RecruiterLyncCallLog>();
            try
            {
                var Logs = db.rpt_GetLyncCallLog(startDate, endDate).ToList();
                var recruiters = p.GetAllRecruiters();
                /* Add BACK CODE TO INCLUDE CALLS NOT ASSOCIATED TO RECRUITER */
                  
                var query = from l in Logs
                            join r in recruiters on l.RecruiterSIP equals r.sip
                            select new RecruiterLyncCallLog
                            {
                                PhoneRoom = r.PhoneRoom,
                                DisplayName = r.DisplayName,
                                EmailAddress = r.EmailAddress,
                                Description = r.Description,
                                StartLogId = l.StartLogId,
                                CallId = l.CallID,
                                JobNumber = l.JobNumber,
                                CallerId = l.CallerId,
                                TollFreeNumber = l.TollFreeNumber,
                                CallStartTime = l.CallStartTime,
                                RecruiterConnectTime = l.RecruiterConnectTime,
                                CallEndTime = l.CallEndTime,
                                RecruiterSIP = l.RecruiterSIP,
                                CallDirection = l.CallDirection,
                                CallEndStatus = l.CallEndStatus
                            };
                v = query.ToList();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return v;
        }
   }
}


//var Logs = db.rpt_GetLyncCallLog(startDate, endDate).ToList();
//var recruiters = p.GetAllRecruiters();
//var query = from l in Logs
//    join r in recruiters on l.RecruiterSIP equals r.sip into rl
//    from x in rl.DefaultIfEmpty()
//    select new RecruiterLyncCallLog
//    {
//        PhoneRoom = x.PhoneRoom ?? "",
//        DisplayName = x.DisplayName ?? "",
//        EmailAddress = x.EmailAddress ?? "",
//        Description = x.Description ?? "",
//        StartLogId = l.StartLogId,
//        CallId = l.CallID,
//        JobNumber = l.JobNumber,
//        CallerId = l.CallerId,
//        TollFreeNumber = l.TollFreeNumber,
//        CallStartTime = l.CallStartTime,
//        RecruiterConnectTime = l.RecruiterConnectTime,
//        CallEndTime = l.CallEndTime,
//        RecruiterSIP = l.RecruiterSIP,
//        CallDirection = l.CallDirection,
//        CallEndStatus = l.CallEndStatus
//    };
//v = query.ToList();
