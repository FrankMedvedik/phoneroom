using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Lync.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using PhoneLogic.Model;
using PhoneLogic.Model.Models;


namespace PhoneLogic.Core.Services
{

    public static class ClosedCallbackSvc
    {
        public static async Task<List<myCallback>> GetClosedJobCallbacks(string jobNum, string taskId, DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(ConditionalConfiguration.apiUrl 
                + "ClosedCallbacks" 
                + "?jobNum="+jobNum
                + "&taskId="+taskId
                + "&startDate=" + startDate.Ticks
                + "&endDate=" + endDate.Ticks
                ));
            return JsonConvert.DeserializeObject<List<myCallback>>(data);
        }

        public static async Task<List<CallbackRpt>> GetClosedCallbackRpt(DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(ConditionalConfiguration.apiUrl
                + "ClosedCallbacks" 
                + "?startDate=" + startDate.Ticks
                + "&endDate=" + endDate.Ticks
                ));
            return JsonConvert.DeserializeObject<List<CallbackRpt>>(data);
        }


      
    }
}
