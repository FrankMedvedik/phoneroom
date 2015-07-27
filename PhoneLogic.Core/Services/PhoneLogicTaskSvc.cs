using System;
using System.Net;
using System.Threading.Tasks;
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
                        new Uri(ConditionalConfiguration.apiUrl + "PhoneLogicTasks?jobNum=" + jobNum + "&taskId=" + task));
            var c = JsonConvert.DeserializeObject<List<PhoneLogicTask>>(data);
            var t = c[0];
            return t;
        }

        //public static async Task<List<PhoneLogicTask>> GetMyJobs(string sip)
        //{
        //    var client = new WebClient();
        //    var data = await client.DownloadStringTaskAsync(new Uri(ConditionalConfiguration.apiUrl + "PhoneLogicTasks?SIP=" + sip));
        //    return JsonConvert.DeserializeObject<List<PhoneLogicTask>>(data);
        //}


        public static async Task<List<PhoneLogicTask>> GetMyJobs(string sip)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(ConditionalConfiguration.apiUrl + "PhoneLogicTasksRpt?SIP=" + sip));
            return JsonConvert.DeserializeObject<List<PhoneLogicTask>>(data);
        }

    }
}
