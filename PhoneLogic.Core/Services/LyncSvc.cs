using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Lync.Model;
using System.Collections.Generic;
using PhoneLogic.Core.ServiceReference;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;
using QueueSummary = PhoneLogic.Model.QueueSummary;


namespace PhoneLogic.Core.Services
{
    public enum RecruiterStatus
    {
        Available,
        Online
    }
}


    public static class LyncSvc
    {


        public async static void testAll()
        {
            //int lqs = GetMyQueuedCalls();
            var x = GetMyQueueSummary();
            List<string> la = await GetAvailableRecruiters();
            List<string> lo = await GetOnlineRecruiters();
          //  List <JobSummary> ljs = GetJobSummary();
            List<QueueDetail> lqd = GetQueueDetail("20140001:1");

        }
      
        //public static int GetMyQueuedCalls()
        //{
        //    var proxy = new PhoneLogicServiceClient();
        //    // Parameters that you pass to the BeginDoWork method go right after EndDoWork. 
        //    Object state = "test";
        //    var channel = proxy.ChannelFactory.CreateChannel();
        //    var t = Task<int>.Factory.FromAsync(channel.BeginGetMyQueuedCalls,
        //        channel.EndGetMyQueuedCalls, LyncClient.GetClient().Self.Contact.Uri, state);
        //    return t.Result;
        //}

        public async static Task<ObservableCollection<QueueSummary>> GetMyQueueSummary()
        {
            var wqs = new ObservableCollection<QueueSummary>();
                var proxy = new PhoneLogicServiceClient();
                Object state = "test";
                var channel = proxy.ChannelFactory.CreateChannel();
                var t = await Task<List<PhoneLogic.Core.ServiceReference.QueueSummary>>.Factory.FromAsync(channel.BeginGetMyQueueSummary,
                    channel.EndGetMyQueueSummary, LyncClient.GetClient().Self.Contact.Uri, state);
            if (t.Count > 0)
            {
                var lqs = t;
                foreach (var s in lqs)
                    wqs.Add(new QueueSummary()
                        {JobNumber = s.JobNumber,
                         InQueue = s.InQueue});
            }
            return wqs;
        }

        //public static List<QueueSummary> GetMyQueuedCalls()
        //{
        //    var proxy = new PhoneLogicServiceClient();
        //    // Parameters that you pass to the BeginDoWork method go right after EndDoWork. 
        //    Object state = "test";
        //    var channel = proxy.ChannelFactory.CreateChannel();

        //    var t = Task<List<QueueSummary>>.Factory.FromAsync(channel.BeginGetMyQueuedCalls,
        //        channel.EndGetMyQueuedCalls, LyncClient.GetClient().Self.Contact.Uri, state);


        //    if(List<QueueSummary>t.Result)
        //    return t.Result;
        //}


        public async static Task<List<String>> GetAvailableRecruiters()
        {
            return await GetRecruiters(RecruiterStatus.Available);
        }


        public async static Task<List<String>> GetOnlineRecruiters()
        {
            return await GetRecruiters(RecruiterStatus.Online);
        }

        public static List<JobSummary> GetJobSummary()
        {
                var proxy = new PhoneLogicServiceClient();
                Object state = "test";
                var channel = proxy.ChannelFactory.CreateChannel();
                var t = Task<List<JobSummary>>.Factory.FromAsync(channel.BeginGetJobSummary,
                    channel.EndGetJobSummary, state);
                var x = t.Result;
                MessageBox.Show(x.ToString());
                return t.Result;
        }

        public static List<QueueDetail> GetQueueDetail(string jobNum)
        {
                var proxy = new PhoneLogicServiceClient();
                Object state = "test";
                var channel = proxy.ChannelFactory.CreateChannel();
                var t = Task<List<QueueDetail>>.Factory.FromAsync(channel.BeginGetQueueDetail,
                    channel.EndGetQueueDetail,jobNum, state);
                var x = t.Result;
                MessageBox.Show(x.ToString());
                return t.Result;
        }

        public async static Task<List<String>> GetRecruiters(RecruiterStatus status)
        {
            var proxy = new PhoneLogicServiceClient();
            Object state = "test";
            var channel = proxy.ChannelFactory.CreateChannel();
            var l = new List<String>();

            switch (status)
            {
                case RecruiterStatus.Available:
                    var t = await Task<List<String>>.Factory.FromAsync(channel.BeginGetRecruitersAvailable,
                        channel.EndGetRecruitersAvailable, state);
                    l = t;
                    break;

                case RecruiterStatus.Online:
                    var x = await Task<List<String>>.Factory.FromAsync(channel.BeginGetRecruitersOnline,
                        channel.EndGetRecruitersOnline, state);
                    l = x;
                    break;
            }
            return l.OrderBy(q => q.Substring(5)).ToList();
        }
    }
