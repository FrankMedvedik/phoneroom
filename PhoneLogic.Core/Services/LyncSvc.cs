using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Lync.Model;
using System.Collections.Generic;
using PhoneLogic.Core;
using PhoneLogic.Core.Models;
using PhoneNumbers;
#if DEBUGTEST
using sr = PhoneLogic.Core.ProdServiceReference;
using PhoneLogic.Core.ProdServiceReference;
//#if DEBUGTEST
// using xx = PhoneLogic.Core.AppServiceReference;
//using PhoneLogic.Core.AppServiceReference;
#elif DEBUGPROD
//using xx = PhoneLogic.Core.AppServiceReference;
using sr = PhoneLogic.Core.ProdServiceReference;
using PhoneLogic.Core.ProdServiceReference;
#else
using sr = PhoneLogic.Core.ProdServiceReference;
using PhoneLogic.Core.ProdServiceReference;

#endif
using PhoneLogic.Core.Services;
using PhoneLogic.Model;
using QueueSummary = PhoneLogic.Model.QueueSummary;

//using QueueSummary = PhoneLogic.Model.QueueSummary;


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
            //var x = GetMyQueueSummary();
            //List<string> la = await GetAvailableRecruiters();
            //List<string> lo = await GetOnlineRecruiters();
            //List<QueueDetail> lqd = GetQueueDetail("20140001:1");
            var t = GetJobSummary();

        }
      
        public async static Task<ObservableCollection<PhoneLogic.Model.QueueSummary>> GetMyQueueSummary()
        {
            var wqs = new ObservableCollection<PhoneLogic.Model.QueueSummary>();
                var proxy = new PhoneLogicServiceClient();
                Object state = "test";
                var channel = proxy.ChannelFactory.CreateChannel();
                var t = await Task<List<sr.QueueSummary>>.Factory.FromAsync(channel.BeginGetMyQueueSummary,
                    channel.EndGetMyQueueSummary, LyncClient.GetClient().Self.Contact.Uri, state);
            if (t.Count > 0)
            {
                foreach (var s in t)
                    wqs.Add(new PhoneLogic.Model.QueueSummary()
                        {JobNumber = s.JobNumber,
                         InQueue = s.InQueue});
            }
            return wqs;
        }

        public async static Task<ObservableCollection<PhoneLogic.Model.QueueSummary>> GetAllQueueSummary()
        {
            var proxy = new PhoneLogicServiceClient();
            Object state = "test";
            var channel = proxy.ChannelFactory.CreateChannel();
            var t = await Task<List<sr.QueueDetail>>.Factory.FromAsync(channel.BeginGetAllCallsInQueue,
                channel.EndGetAllCallsInQueue, state);
            var lst = new List<PhoneLogic.Model.QueueSummary>();
            if (t.Count > 0)
            {
                lst = t
                    .GroupBy(n => n.JobNumber)
                    .Select(n => new PhoneLogic.Model.QueueSummary
                    {
                        JobNumber = n.Key,
                        InQueue = n.Count()
                    }).ToList();
            }
            return new ObservableCollection<PhoneLogic.Model.QueueSummary>(lst);
        }

        

        public async static Task<ObservableCollection<JobCallSummary>> GetJobSummary()
        {
            var tc = new ObservableCollection<JobCallSummary>();
            var proxy = new PhoneLogicServiceClient();
            var channel = proxy.ChannelFactory.CreateChannel();
            Object state = "test";
            try
            {
                var t = await Task<List<JobSummary>>.Factory.FromAsync(channel.BeginGetJobSummary,
                    channel.EndGetJobSummary, state);
                foreach (var s in t)
                    tc.Add(new JobCallSummary()
                    {
                        JobNumber = s.JobNumber,
                        InQueue = s.InQueue,
                        Abandoned = s.Abandoned,
                        Callback = s.Callback,
                        InboundCalls = s.InboundCalls,
                        LeftMessage = s.LeftMessage,
                        NoAgents = s.NoAgents,
                        OutboundCall = s.OutboundCall,
                        PlacedCall = s.PlacedCall,
                        TollFreeNumber = s.TollFreeNumber
                    });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return tc;
        }
        
        public async static Task<ObservableCollection<ActiveCallDetail>> GetActiveCallsDetail()
        {
            var tc = new ObservableCollection<ActiveCallDetail>();
            var proxy = new PhoneLogicServiceClient();
            var channel = proxy.ChannelFactory.CreateChannel();
            Object state = "test";
            try
            {
                var t = await Task<List<ActiveCall>>.Factory.FromAsync(channel.BeginGetActiveCalls,
                    channel.EndGetActiveCalls, state);
                foreach (var s in t)
                    tc.Add(new ActiveCallDetail()
                    {
                        CallerId = s.CallerId,
                        ConferenceUri = s.ConferenceUri,
                        Id = s.Id,
                        JobNumber = s.JobNumber,
                        RecruiterUri = s.RecruiterUri,
                        TimeIn = s.TimeIn
                    });
                }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return tc;
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
        

        public async static Task RecruiterDialOut(String JobFormatted,  String PhoneNumber, int CallbackId)
        {
            if (LyncClient.GetClient() == null) return ;
            var proxy = new PhoneLogicServiceClient();

            Object state = "test";
            var channel = proxy.ChannelFactory.CreateChannel();
            IAsyncResult result = channel.BeginRecruiterDialOut(LyncClient.GetClient().Self.Contact.Uri, JobFormatted, PhoneNumber,
                            CallbackId, channel.EndRecruiterDialOut, state);

        }

        public async static Task TransferCall(String callId, String sip)
        {
            var proxy = new PhoneLogicServiceClient();
            Object state = "test";
            var channel = proxy.ChannelFactory.CreateChannel();
            IAsyncResult result = channel.BeginTransferCall(callId,sip,channel.EndTransferCall, state);

        }

        public async static Task<List<QueueDetail>> GetMyQueuedCalls()
        {   var t = new List<QueueDetail>();
            try
            {
                if (LyncClient.GetClient() == null) return null;
                var proxy = new PhoneLogicServiceClient();
                Object state = "test";
                var channel = proxy.ChannelFactory.CreateChannel();
                t = await Task<List<QueueDetail>>.Factory.FromAsync(channel.BeginGetMyQueueDetail,
                    channel.EndGetMyQueueDetail, LyncClient.GetClient().Self.Contact.Uri, state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return t;
        }

        public async static Task PullFromQueue(string callId)
        {

            if (LyncClient.GetClient() == null) return;
            var proxy = new PhoneLogicServiceClient();
            Object state = "test";
            var channel = proxy.ChannelFactory.CreateChannel();
            IAsyncResult result = channel.BeginPullFromQueue(callId, LyncClient.GetClient().Self.Contact.Uri, channel.EndPullFromQueue, state);
        }
    }

