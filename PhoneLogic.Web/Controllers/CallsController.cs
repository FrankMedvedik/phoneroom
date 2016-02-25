using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PhoneLogic.CallBusinessIntelligence;
using PhoneLogic.Model;
using PhoneLogic.Repository;
using PhoneLogic.UserAuth;

namespace PhoneLogic.Web.Controllers
{
    public class CallsController : ApiController
    {
        private PhoneLogicEntities db = new PhoneLogicEntities();
        private PhoneRoomUsers p = new PhoneRoomUsers();
        private RecruiterUtilization r = new RecruiterUtilization();
        // GET: api/Calls?sip=1&startDate=2&endDate=3
        public IEnumerable<Call> GetCalls(string sip, Int64 startDate, Int64 endDate)
        {
            return r.GetCalls(sip, new DateTime(startDate) , new DateTime(endDate));
        }



        public IEnumerable<Call> GetCalls(string phoneNumber)
        {
            var Logs = db.rpt_GetCallerLyncCallLog(phoneNumber).ToList();
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
                            TaskDscr = l.taskdscr,
                            TaskTypeID = l.TaskTypeId,
                            TaskName = l.taskName,
                            CallerId = l.CallerId,
                            CallerId_Region = l.CallerId_Region,
                            CallerId_UTC_code = l.CallerId_UTC_code,
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
        public IEnumerable<Call> GetCalls(string jobNum, string sip, Int64 startDate, Int64 endDate)
        {
            var Logs = db.rpt_GetLyncCallJobRecruiterLog(jobNum, sip, new DateTime(startDate), new DateTime(endDate)).ToList();
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
                    TaskDscr = l.taskdscr,
                    TaskTypeID = l.TaskTypeId,
                    TaskName = l.taskName,
                    CallerId = l.CallerId,
                    CallerId_Region = l.CallerId_Region,
                    CallerId_UTC_code = l.CallerId_UTC_code,
                    TollFreeNumber = l.TollFreeNumber,
                    CallStartTime = l.CallStartTime,
                    RecruiterConnectTime  = l.RecruiterConnectTime ,
                    CallEndTime = l.CallEndTime,
                    RecruiterSIP = l.RecruiterSIP,
                    CallDirection = l.CallDirection,
                    CallEndStatus = l.CallEndStatus,
                    CallDuration = l.CallDuration
                };

            return query.OrderBy(x => x.CallStartTime).ToList();
        }

        // GET: api/Calls?startDate=2&endDate=3
        public IEnumerable<Call> GetCalls(Int64 startDate, Int64 endDate)
        {
            var v = new List<Call>();
            try
            {
                var Logs = db.rpt_GetLyncCallLog(new DateTime(startDate), new DateTime(endDate)).ToList();
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
                                TaskDscr = l.taskdscr,
                                TaskTypeID = l.TaskTypeId,
                                TaskName = l.taskName,
                                CallerId = l.CallerId,
                                CallerId_Region = l.CallerId_Region,
                                CallerId_UTC_code = l.CallerId_UTC_code,
                                TollFreeNumber = l.TollFreeNumber,
                                CallStartTime = l.CallStartTime,
                                RecruiterConnectTime = l.RecruiterConnectTime,
                                CallEndTime = l.CallEndTime,
                                RecruiterSIP = l.RecruiterSIP,
                                CallDirection = l.CallDirection,
                                CallEndStatus = l.CallEndStatus,
                                CallDuration = l.CallDuration
                            };
                v = query.OrderBy(x => x.CallStartTime).ToList();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return v;
        }

        // GET: api/Calls?startDate=1&endDate=2&phoneNumber=3
        public IEnumerable<Call> GetCalls(Int64 startDate, Int64 endDate, string phoneNumber)
        {
            var v = new List<Call>();
            try
            {
                var s = new DateTime(startDate);
                var e = new DateTime(endDate);
                var Logs = db.rpt_GetLyncCallsforPhoneInDateRange(phoneNumber,s, e).ToList();
                //var Logs = db.rpt_GetLyncCallsforPhoneInDateRange(phoneNumber, new DateTime(startDate), new DateTime(endDate)).ToList();
                var recruiters = p.GetAllRecruiters();
                /* Add BACK CODE TO INCLUDE CALLS NOT ASSOCIATED TO RECRUITER */



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
                                TaskDscr = l.taskdscr,
                                TaskTypeID = l.TaskTypeId,
                                TaskName = l.taskName,
                                CallerId = l.CallerId,
                                CallerId_Region = l.CallerId_Region,
                                CallerId_UTC_code = l.CallerId_UTC_code,
                                TollFreeNumber = l.TollFreeNumber,
                                CallStartTime = l.CallStartTime,
                                RecruiterConnectTime = l.RecruiterConnectTime,
                                CallEndTime = l.CallEndTime,
                                RecruiterSIP = l.RecruiterSIP,
                                CallDirection = l.CallDirection,
                                CallEndStatus = l.CallEndStatus,
                                CallDuration = l.CallDuration
                            };
                v = query.OrderBy(x => x.CallStartTime).ToList();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return v;
        }
    }
}

