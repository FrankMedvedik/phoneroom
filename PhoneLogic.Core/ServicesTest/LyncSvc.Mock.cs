using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
#if DEBUGTEST
using PhoneLogic.Core.AppServiceReference;
#elif DEBUGPROD
using PhoneLogic.Core.ProdServiceReference;
#endif
using PhoneLogic.Model;
using QueueSummary = PhoneLogic.Model.QueueSummary;

namespace PhoneLogic.Core.ServicesTest
{
    public class LyncSvc
    {

        public static List<QueueSummary> GetMyQueuedCalls()
        {   
            var lqs = new List<QueueSummary>();
            lqs.Add(
                new QueueSummary
                {
                    InQueue = 100,
                    JobNumber = "9999-9999"
                });

            lqs.Add(new QueueSummary
                {
                    InQueue = 0,
                    JobNumber = "1111-1111"
                });
        
            return lqs;
            }


        public static List<string> GetAvailableRecruiters()
        {
            try
            {
                var proxy = new PhoneLogicServiceClient();
                // Parameters that you pass to the BeginDoWork method go right after EndDoWork. 
                Object state = "test";
                var channel = proxy.ChannelFactory.CreateChannel();
                var t = Task<List<String>>.Factory.FromAsync(channel.BeginGetRecruitersAvailable,
                    channel.EndGetRecruitersAvailable, state);
                return t.Result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private static List<Recruiter> GetRecruiters()
        {
            List<Recruiter> Recruiters = new List<Recruiter>();
            Recruiters.Add(new Recruiter() { EmailAddress = "dreckner@reckner.com", InboundCallCnt = 20, OutboundCallCnt = 6, TotalIdleTime = 90 });
            Recruiters.Add(new Recruiter() { EmailAddress = "fmedvedik@reckner.com", InboundCallCnt = 16, OutboundCallCnt = 45, TotalIdleTime = 9 });
            Recruiters.Add(new Recruiter() { EmailAddress = "mdoughtery@reckner.com", InboundCallCnt = 145, OutboundCallCnt = 11, TotalIdleTime = 19 });
            Recruiters.Add(new Recruiter() { EmailAddress = "jByers@reckner.com", InboundCallCnt = 2, OutboundCallCnt = 22, TotalIdleTime = 35 });
            Recruiters.Add(new Recruiter() { EmailAddress = "ldiehl@reckner.com", InboundCallCnt = 12, OutboundCallCnt = 70, TotalIdleTime = 110 });
            Recruiters.Add(new Recruiter() { EmailAddress = "klittlefield@reckner.com", InboundCallCnt = 20, OutboundCallCnt = 45, TotalIdleTime = 28 });
            Recruiters.Add(new Recruiter() { EmailAddress = "jlync@reckner.com", InboundCallCnt = 9, OutboundCallCnt = 30, TotalIdleTime = 14 });
            Recruiters.Add(new Recruiter() { EmailAddress = "flync@reckner.com", InboundCallCnt = 81, OutboundCallCnt = 13, TotalIdleTime = 17 });
            Recruiters.Add(new Recruiter() { EmailAddress = "krohm@reckner.com", InboundCallCnt = 3, OutboundCallCnt = 3, TotalIdleTime = 9 });
            return Recruiters;
        }


}
}
