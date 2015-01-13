using System;
using System.Collections.Generic;
using System.Net;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using PhoneLogic.Model;
using System.Threading.Tasks;

namespace PhoneLogic.Core.Services
{

    public static class PeopleSvc 
    {
        public static async Task<List<person>> GetPeople(string phoneNum)
        {

            var url = ApiWebSite.urlRoot + "People?phoneNumber={" + phoneNum + "}";
            var uri = new Uri(url, UriKind.Absolute);
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(url, UriKind.Absolute));
            return JsonConvert.DeserializeObject<System.Collections.Generic.List<person>>(data);
        }
    }
}