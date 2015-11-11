using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneLogic.Model;

namespace PhoneLogic.CallBusinessIntelligence
{
    internal class RecruiterActivityFactory
    {
        public static string CallActivityType = "Call";
        public static string OtherActivityType = "Other";
        public static string InboundCallActivity = "Incoming";
        public static string OutboundCallActivity = "Outgoing";

        public RecruiterActivityFactory(UserPrincipal recruiter)
        {
            _recruiter = recruiter;
        }

        private UserPrincipal _recruiter;

        public RecruiterActivity CreateNewCallActivity(Call c)
        {
            return new RecruiterActivity()
            {
                RecruiterSIP = "sip:" + _recruiter.EmailAddress,
                ActivityType = CallActivityType,
                ReviewDate = c.CallStartTime.GetValueOrDefault(), // what default
                Description = "call time ",
                DisplayName = _recruiter.DisplayName,
                EmailAddress = _recruiter.EmailAddress,
                //PhoneRoom = _recruiter.PhoneRoom
                // we need to fill this in when we create an call activity
                //Duration = (int)(DateTime.Now.Subtract(previousActivity.EndTime.GetValueOrDefault(DateTime.Now))).Ticks,
                //EndTime = DateTime.Now,
                //StartTime = DateTime.Now,
                // ActivityName = calls.[].CallDirection,

            };


            return new RecruiterActivity();

        }

        public RecruiterActivity CreateNewIdleActivity(string sip, DateTime startTime, DateTime endTime)
        {
            return new RecruiterActivity();

        }




        //    idleActivityTemplate = new RecruiterActivity()
        //    {
        //        RecruiterSIP = calls.First().RecruiterSIP,
        //            ActivityName = "Other",
        //            ActivityType = "Other",
        //            ReviewDate = StartTime,
        //            Description = "non call time ",
        //            DisplayName = calls.First().DisplayName,
        //            EmailAddress = calls.First().EmailAddress,
        //            PhoneRoom = calls.First().PhoneRoom
        //            // we need to fill this in when we create an idle activity
        //            //Duration = (int)(DateTime.Now.Subtract(previousActivity.EndTime.GetValueOrDefault(DateTime.Now))).Ticks,
        //            //EndTime = DateTime.Now,
        //            //StartTime = DateTime.Now,
        //    };
        //}
    }
}