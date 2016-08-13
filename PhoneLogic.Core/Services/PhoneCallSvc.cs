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
        public static async Task LogPhoneCall(PhoneCall p)
        {
            await Post(p);
        }


        private static async Task Post(PhoneCall p)
        {
            var url = ConditionalConfiguration.apiUrl + "PhoneCalls/";
            var postRequest = JsonConvert.SerializeObject(p);
            var uri = new Uri(url, UriKind.Absolute);
            var client = new WebClient();
            client.Headers["Content-Length"] = postRequest.Length.ToString(CultureInfo.InvariantCulture);
            client.Headers["Content-Type"] = "application/json";
            var result = await client.UploadStringTaskAsync(uri, "POST", postRequest);
        }
    }
}