using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            string url = ConditionalConfiguration.apiUrl + "PhoneCalls/";
            string postRequest = JsonConvert.SerializeObject(p);
            var uri = new Uri(url, UriKind.Absolute);
            var client = new WebClient();
            client.Headers["Content-Length"] = postRequest.Length.ToString(CultureInfo.InvariantCulture);
            client.Headers["Content-Type"] = "application/json";
            string result = await client.UploadStringTaskAsync(uri, "POST", postRequest);
        }
    }
}