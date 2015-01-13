using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using PhoneLogic.Model;


namespace PhoneLogic.Core.Services
{

    public static class PhoneCallSvc
    {
        public static async Task PlacePhoneCall(PhoneCall p)
        {
            await Post(p);

        }


        private static async Task Post(PhoneCall p)
        {
            string url = ApiWebSite.urlRoot + "PhoneCalls/";
            var postRequest = JsonConvert.SerializeObject(p);
            var uri = new Uri(url, UriKind.Absolute);
            var client = new WebClient();
            client.Headers["Content-Length"] = postRequest.Length.ToString(CultureInfo.InvariantCulture);
            client.Headers["Content-Type"] = "application/json";
            var result = await client.UploadStringTaskAsync(uri, "POST", postRequest);
        }



        public static Task<List<myCallback>> GetMyPhoneCalls(string sip)
        {
            throw new NotImplementedException();
    /*        var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(ApiWebSite.urlRoot + "PhoneCalls?SIP=" + sip));
            return JsonConvert.DeserializeObject<List<myCallback>>(data);
     */
        }

    }
}
