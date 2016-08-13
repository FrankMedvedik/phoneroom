using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneLogic.Model;
using PhoneLogic.Repository;
using PhoneLogic.UserAuth;

namespace PhoneLogic.CallBusinessIntelligence
{
    public class RecruiterUtilization
    {
        private static string _callActivityType = "Call";
        private static string _otherActivityType = "Other";
        private static string _inboundCallActivity = "Incoming";
        private static string _outboundCallActivity = "Outgoing";

        private PhoneLogicEntities db = new PhoneLogicEntities();
        private PhoneRoomUsers p = new PhoneRoomUsers();

        public IEnumerable<RecruiterTimeSummary> PhoneroomTimeSummary(string phoneRoomName, DateTime startTime, DateTime endTime)
        {
            PhoneRoomUsers usrs = new PhoneRoomUsers();
            var rs = usrs.GetPhoneRoom(phoneRoomName);
            List<RecruiterTimeSummary> all = new List<RecruiterTimeSummary>();
            foreach (var r in rs)
            {
                var s = GetRecruiterTimeSummary("sip:" + r.EmailAddress, startTime, endTime);
                s.ReviewDate = DateTime.Now;
                s.PhoneRoom = phoneRoomName;
                s.RecruiterSIP = "sip:" + r.EmailAddress;
                s.EmailAddress = r.EmailAddress;
                s.DisplayName = r.DisplayName;
                s.Description = r.Description;
                all.Add(s);
            }

            return  all;
        }

        public RecruiterTimeSummary GetRecruiterTimeSummary(string sip, DateTime startTime, DateTime endTime)
        {
            RecruiterTimeSummary r = new RecruiterTimeSummary();
            r.RecruiterActivities = GetRecruiterActivityRpt(sip, startTime, endTime);
            if (r.RecruiterActivities.Count > 0)
            {
                r.CallTime = new TimeSpan(r.RecruiterActivities.Where(x => x.ActivityType == _callActivityType).Sum(x => x.Duration.Ticks));
                r.InboundCallCnt =
                    r.RecruiterActivities.Where(x => x.ActivityType == _callActivityType)
                        .Count(x => x.ActivityName == _inboundCallActivity);
                r.OutboundCallCnt =
                    r.RecruiterActivities.Where(x => x.ActivityType == _callActivityType)
                        .Count(x => x.ActivityName == _outboundCallActivity);
                r.IdleTime = new TimeSpan(r.RecruiterActivities.Where(x => x.ActivityType == _otherActivityType).Sum(x => x.Duration.Ticks));
            }
            return r;

        }

        public IEnumerable<Call> GetCalls(string sip, DateTime startDate, DateTime endDate)
        {
            if (sip == null)
                sip = "";
            var Logs = db.rpt_GetRecruiterLyncCallLog(sip, startDate, endDate).ToList();
            var recruiters = p.GetAllRecruiters();


            var query = from l in Logs
                join r in recruiters on l.RecruiterSIP equals r.sip into rr
                from oj in rr.DefaultIfEmpty()
                orderby l.CallStartTime
                select new Call
                {
                    PhoneRoom = (oj == null ? String.Empty : oj.PhoneRoom),
                    DisplayName = (oj == null ? String.Empty : oj.DisplayName),
                    EmailAddress = (oj == null ? String.Empty : oj.EmailAddress),
                    Description = (oj == null ? String.Empty : oj.Description),
                    StartLogId = l.StartLogId,
                    CallId = l.CallID,
                    JobNumber = l.JobNumber,
                    TaskDscr = l.taskDscr,
                    TaskTypeID = l.taskTypeID,
                    TaskName = l.taskName,
                    CallerId = l.CallerId,
                    CallerId_Region = l.CallerId_region,
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

        public List<RecruiterActivity> GetRecruiterActivityRpt(string sip , DateTime StartTime, DateTime EndTime )
        {
            var activities = new List<RecruiterActivity>();
            RecruiterActivity idleActivityTemplate;
            RecruiterActivity callActivityTemplate;
//            RecruiterActivity previousActivity;
            RecruiterActivity currentActivity;
            var calls = GetCalls(sip, StartTime, EndTime).OrderBy(x => x.CallStartTime).ToArray();

            // fetch first call use as actvitity template for "idle activities" 
            // fetch first call into previous actvitity  
            // add first call to activities 
            // add first idle to activities 
            // fetch second call into current actvitity  
            // festch second call if available or current time idl idle to activities 

            // fetch next call get duration between first and second calls 
            // insert idle activity for time between prev and current call 
            // at end duration from end of prev call to NOW() is end time for duration of current activity 

            if (!calls.Any()) return activities;


            callActivityTemplate = new RecruiterActivity()
            {
                RecruiterSIP = calls.First().RecruiterSIP,
                ActivityType = _callActivityType,
                ReviewDate = StartTime,
                Description = "call time ",
                DisplayName = calls.First().DisplayName,
                EmailAddress = calls.First().EmailAddress,
                PhoneRoom = calls.First().PhoneRoom
                // we need to fill this in when we create an call activity
                //Duration = (int)(DateTime.Now.Subtract(previousActivity.EndTime.GetValueOrDefault(DateTime.Now))).Ticks,
                //EndTime = DateTime.Now,
                //StartTime = DateTime.Now,
                // ActivityName = calls.[].CallDirection,

            };

            idleActivityTemplate = new RecruiterActivity()
            {
                RecruiterSIP = calls.First().RecruiterSIP,
                ActivityName = "Other",
                ActivityType = "Other",
                ReviewDate = StartTime,
                Description = "non call time ",
                DisplayName = calls.First().DisplayName,
                EmailAddress = calls.First().EmailAddress,
                PhoneRoom = calls.First().PhoneRoom
                // we need to fill this in when we create an idle activity
                //Duration = (int)(DateTime.Now.Subtract(previousActivity.EndTime.GetValueOrDefault(DateTime.Now))).Ticks,
                //EndTime = DateTime.Now,
                //StartTime = DateTime.Now,
            };

            // from start of review to first call we need an idle 
            var IdleActivity = ObjectCopier.Clone(idleActivityTemplate);
            IdleActivity.EndTime = calls[0].CallStartTime.GetValueOrDefault(EndTime);
            IdleActivity.StartTime = StartTime;
            // i don't know wht this is backwards
            //IdleActivity.Duration = (int)(IdleActivity.StartTime.GetValueOrDefault(DateTime.Now).Subtract(IdleActivity.EndTime.GetValueOrDefault(DateTime.Now))).Ticks;
            IdleActivity.Duration = (IdleActivity.EndTime.Subtract(IdleActivity.StartTime));
            
            activities.Add(IdleActivity);

            currentActivity = ObjectCopier.Clone(callActivityTemplate);
            currentActivity.Duration = TimeSpan.FromSeconds(calls[0].CallDuration.GetValueOrDefault(0));
            currentActivity.EndTime = calls[0].CallEndTime.GetValueOrDefault(DateTime.MinValue);
            currentActivity.StartTime = calls[0].CallStartTime.GetValueOrDefault(StartTime);

            activities.Add(currentActivity);

            if (calls.Count() < 2) // only one call :(
            {
                // just add the call and the idle activity that is ready to go
                var lastActivity = ObjectCopier.Clone(idleActivityTemplate);
                lastActivity.EndTime = EndTime;
                lastActivity.StartTime = calls.First().CallEndTime.GetValueOrDefault(EndTime);
                lastActivity.Duration = (EndTime.Subtract(lastActivity.StartTime));

                activities.Add(lastActivity);
                return activities;
            }

            // more that one!
            // set index to one ! zero and walk the rest of collection
            for (var i = 1; i < calls.Count(); i++)
            {
                // add idle  (current - prev) 
                var idleActivity = ObjectCopier.Clone(idleActivityTemplate);
                idleActivity.EndTime = calls[i].CallStartTime.GetValueOrDefault(EndTime);
                idleActivity.StartTime = calls[i - 1].CallEndTime.GetValueOrDefault(EndTime);
                idleActivity.Duration = idleActivity.EndTime.Subtract(idleActivity.StartTime);
                activities.Add(idleActivity);

                // add current 

                currentActivity = ObjectCopier.Clone(callActivityTemplate);
                currentActivity.ActivityName = calls[i].CallDirection;
                currentActivity.EndTime = calls[i].CallEndTime.GetValueOrDefault(EndTime);
                currentActivity.StartTime = calls[i].CallStartTime.GetValueOrDefault(EndTime);
                currentActivity.Duration = TimeSpan.FromSeconds(calls[i].CallDuration.GetValueOrDefault(0)); //currentActivity.EndTime.Subtract(currentActivity.StartTime)));
                activities.Add(currentActivity);

            }
            // bring us up to now with some idle time...
            currentActivity = ObjectCopier.Clone(idleActivityTemplate);
            currentActivity.EndTime = EndTime;
            currentActivity.StartTime = calls[calls.Count() - 1].CallEndTime.GetValueOrDefault(EndTime);
            currentActivity.Duration = (currentActivity.EndTime.Subtract(currentActivity.StartTime));

            activities.Add(currentActivity);
            return activities;

        }
    }
}