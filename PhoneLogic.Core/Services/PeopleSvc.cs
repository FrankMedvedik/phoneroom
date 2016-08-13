using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Services
{
    public static class PeopleSvc
    {
        public static async Task<List<person>> GetPeople(string phoneNum)
        {
            var url = ConditionalConfiguration.apiUrl + "People?phoneNumber={" + phoneNum + "}";
            var uri = new Uri(url, UriKind.Absolute);
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(url, UriKind.Absolute));
            return JsonConvert.DeserializeObject<List<person>>(data);
        }
    }
}