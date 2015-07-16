using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PhoneLogic.Model;
using PhoneLogic.Repository;
using PhoneLogic.UserAuth;

namespace PhoneLogic.Web.Controllers
{
    public class CallsController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();
        private PhoneRoomUsers p = new PhoneRoomUsers();

        // GET: api/Calls?sip=1&startDate=2&endDate=3
        public IEnumerable<Call> GetCalls(string sip, DateTime startDate, DateTime endDate)
        {
            var Logs = db.rpt_GetRecruiterLyncCallLog(sip, startDate, endDate).ToList();
            var recruiters = p.GetAllRecruiters();


            var query = from l in Logs
                        join r in recruiters on l.RecruiterSIP equals r.sip into rr
                        from oj in rr.DefaultIfEmpty()
                        select new Call
                        {
                            PhoneRoom = (oj == null ? String.Empty : oj.PhoneRoom),
                            DisplayName = (oj == null ? String.Empty : oj.DisplayName),
                            EmailAddress = (oj == null ? String.Empty : oj.EmailAddress),
                            Description = (oj == null ? String.Empty : oj.Description),
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
                            CallEndStatus = l.CallEndStatus,
                            CallDuration = l.CallDuration
                        };

            return query.ToList();
        }

        // GET: api/Calls?jobNum=0&sip=1&startDate=2&endDate=3
        public IEnumerable<Call> GetCalls(string jobNum, string sip, DateTime startDate, DateTime endDate)
        {
            var Logs = db.rpt_GetLyncCallJobRecruiterLog(jobNum, sip, startDate, endDate).ToList();
            var recruiters = p.GetAllRecruiters();


     var query = from l in Logs
                join r in recruiters on l.RecruiterSIP equals r.sip into rr
                from oj in rr.DefaultIfEmpty()
                select new Call
                {
                    PhoneRoom =  (oj == null ? String.Empty : oj.PhoneRoom),
                    DisplayName = (oj == null ? String.Empty : oj.DisplayName),
                    EmailAddress = (oj == null ? String.Empty : oj.EmailAddress),
                    Description = (oj == null ? String.Empty : oj.Description),
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
                    CallEndStatus = l.CallEndStatus,
                    CallDuration = l.CallDuration
                };

            return query.ToList();
        }

        // GET: api/Calls?startDate=2&endDate=3
        public IEnumerable<Call> GetCalls(DateTime startDate, DateTime endDate)
        {
            var v = new List<Call>();
            try
            {
                var Logs = db.rpt_GetLyncCallLog(startDate, endDate).ToList();
                var recruiters = p.GetAllRecruiters();
                /* Add BACK CODE TO INCLUDE CALLS NOT ASSOCIATED TO RECRUITER */
                  
                var query = from l in Logs
                            join r in recruiters on l.RecruiterSIP equals r.sip
                            select new Call
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
                                CallEndStatus = l.CallEndStatus,
                                CallDuration = l.CallDuration
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

