using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using PhoneLogic.Model;


namespace PhoneLogic.Core.Services
{

    public static class CallbackSvc
    {
        public static async Task StartCall(CallbackDto cb)
        {
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
        /*

        private static async Task Put(CallbackDto cb)
        {
            string uriTemplate = ApiWebSite.urlRoot + "Callbacks/{0}";
            var putRequest = JsonConvert.SerializeObject(cb);
            var uri = new Uri(string.Format(uriTemplate, cb.callbackID), UriKind.Absolute);
            var client = new WebClient();
            client.Headers["Content-Length"] = putRequest.Length.ToString(CultureInfo.InvariantCulture);
            client.Headers["Content-Type"] = "application/json";
            var result = await client.UploadStringTaskAsync(uri, "PUT", putRequest);
        }

                private static async Task Put(PhoneCall p)
                {
                    string url = ApiWebSite.urlRoot + "PhoneCalls/";
                    var postRequest = JsonConvert.SerializeObject(p);
                    var uri = new Uri(url, UriKind.Absolute);
                    var client = new WebClient();
                    client.Headers["Content-Length"] = postRequest.Length.ToString(CultureInfo.InvariantCulture);
                    client.Headers["Content-Type"] = "application/json";
                    var result = await client.UploadStringTaskAsync(uri, "POST", postRequest);
                }


        */
                private static async Task Put(CallbackDto cb)
                {
                    string uriTemplate = ApiWebSite.urlRoot + "Callbacks/{0}";
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
            var data = await client.DownloadStringTaskAsync(new Uri(ApiWebSite.urlRoot + "Callbacks?SIP=" + sip));
            return JsonConvert.DeserializeObject<List<myCallback>>(data);
        }

        public static async Task<CallbackDto> GetMyCallback(int id)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(ApiWebSite.urlRoot + "Callbacks?id=" + id));
            return JsonConvert.DeserializeObject<CallbackDto>(data);
        }

    }
}
