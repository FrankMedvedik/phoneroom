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

    public static class CallbackSvc
    {
        public static async Task StartCall(CallbackDto cb)
        {
            cb.SIP = LyncClient.GetClient().Self.Contact.Uri;
            cb.status = "Call In Progress";
            cb.statusDate = DateTime.Now;
            await Put(cb);

        }

        public static async Task EndCall(CallbackDto cb)
        {
            cb.status = string.Empty;
            cb.SIP = string.Empty;
            cb.statusDate = DateTime.Now;
            await Put(cb);

        }

        public static async Task Close(CallbackDto cb)
        {
            cb.status = "Closed";
            cb.statusDate = DateTime.Now;
            await Put(cb);
        }


        private static async Task Put(CallbackDto cb)
        {
            string uriTemplate = ConditionalConfiguration.apiUrl + "Callbacks/{0}";
            var postRequest = JsonConvert.SerializeObject(cb);
            var url = string.Format(uriTemplate, cb.callbackID);
            var uri = new Uri(url, UriKind.Absolute);
            var client = new WebClient();
            client.Headers["Content-Length"] = postRequest.Length.ToString(CultureInfo.InvariantCulture);
            client.Headers["Content-Type"] = "application/json";
            var result = await client.UploadStringTaskAsync(uri, "POST", postRequest);
        }

        
        public  static async Task<List<myCallback>> GetMyCallbacks(string sip)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(ConditionalConfiguration.apiUrl + "Callbacks?SIP=" + sip));
            return JsonConvert.DeserializeObject<List<myCallback>>(data);
        }

        public static async Task<List<myCallback>> GetJobCallbacks(string jobNum, string taskId, DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(ConditionalConfiguration.apiUrl 
                + "Callbacks" 
                + "?jobNum="+jobNum
                + "&taskId="+taskId
                + "&startDate=" + startDate
                + "&endDate=" + endDate
                ));
            return JsonConvert.DeserializeObject<List<myCallback>>(data);
        }

        public static async Task<List<CallbackRpt>> GetCallbackRpt(DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(ConditionalConfiguration.apiUrl
                + "Callbacks" 
                + "?startDate=" + startDate
                + "&endDate=" + endDate
                ));
            return JsonConvert.DeserializeObject<List<CallbackRpt>>(data);
        }


        public static async Task<CallbackDto> GetMyCallback(int id)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(ConditionalConfiguration.apiUrl + "Callbacks?id=" + id));
            return JsonConvert.DeserializeObject<CallbackDto>(data);
        }

    }
}
