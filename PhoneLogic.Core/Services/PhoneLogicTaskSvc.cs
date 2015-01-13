using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using PhoneLogic.Model;


namespace PhoneLogic.Core.Services
{
    public static class PhoneLogicTaskSvc
    {
        public static async Task<PhoneLogicTask> GetTask(string jobNum, string taskId)
        {
            int task = Convert.ToInt32(taskId);
            var client = new WebClient();
            var data =
                await
                    client.DownloadStringTaskAsync(
                        new Uri(ApiWebSite.urlRoot + "PhoneLogicTasks?jobNum=" + jobNum + "&taskId=" + task));
            var c = JsonConvert.DeserializeObject<List<PhoneLogicTask>>(data);
            var t = c[0];
            return t;
        }

        public static async Task<List<PhoneLogicTask>> GetMyTasks(string sip)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(ApiWebSite.urlRoot + "PhoneLogicTasks?SIP=" + sip));
            return JsonConvert.DeserializeObject<List<PhoneLogicTask>>(data);
        }



        /*
            public static async Task<List<PhoneLogicTask>> GetMyTasks(string sip)
            {
                try
                {
                    var client = new WebClient();
                    var x = new Uri(ApiWebSite.urlRoot + "PhoneLogicTasks?sip=" + sip);
                    var data = await client.DownloadStringTaskAsync(x);

                    var a = JsonConvert.DeserializeObject<List<PhoneLogicTask>>(data);
                    return a;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);

                    if (e.InnerException == null)
                        Console.Write(e.Message + e.StackTrace);
                    else
                        Console.Write(e.Message + e.InnerException.Message + e.StackTrace);
                    return null;
                }
            }
       
                public static List<PhoneLogicTask> GetMyTasks(string sip)
                {
                    return JsonConvert.DeserializeObject<List<PhoneLogicTask>>(Data); 
                }

                private const string Data = "[{\"$id\":\"1\",\"jobNum\":\"20090002\",\"taskID\":1,\"taskName\":\"Arrhythmia(NB)\",\"taskDscr\":\"TLD\",\"TaskStartDate\":\"2009-01-05T00:00:00\",\"TaskEndDate\":\"2009-01-19T00:00:00\",\"taskTypeID\":\"MEDICAL\",\"taskStatusID\":\"RECRUIT\",\"isActive\":true,\"menuID\":-1,\"canEnhanceQueue\":false,\"canZeroOut\":false,\"stopCallbackTime\":\"2009-01-05T00:00:00\",\"stopQueueTime\":\"2009-01-05T00:00:00\",\"closedMsgID\":1,\"closedHits\":0,\"internetURL\":\"\",\"catiURL\":\"\",\"created\":\"2009-01-05T13:54:23.423\",\"updated\":\"2014-08-02T09:53:27.237\",\"oldest_call\":\"2014-08-14T16:53:41.18\",\"newest_call\":\"2014-10-27T14:51:14.78\",\"call_cnt\":11},{\"$id\":\"2\",\"jobNum\":\"20121618\",\"taskID\":1,\"taskName\":\"Pepe Fragrance-NC (SA)\",\"taskDscr\":\"Pepe Fragrance-NC, CLT in MKE, recruited by MKE and web\",\"TaskStartDate\":\"2012-11-26T00:00:00\",\"TaskEndDate\":\"2012-12-21T00:00:00\",\"taskTypeID\":\"CLT\",\"taskStatusID\":\"RECRUIT\",\"isActive\":true,\"menuID\":-1,\"canEnhanceQueue\":false,\"canZeroOut\":false,\"stopCallbackTime\":\"2012-11-26T00:00:00\",\"stopQueueTime\":\"2012-11-26T00:00:00\",\"closedMsgID\":1,\"closedHits\":11,\"internetURL\":\"\",\"catiURL\":\"\",\"created\":\"2012-11-26T13:52:01.157\",\"updated\":\"2014-04-23T12:14:28.257\",\"oldest_call\":\"2014-09-04T12:07:53.1\",\"newest_call\":\"2014-09-04T12:07:53.1\",\"call_cnt\":2}]";
       */ 
    }
}
