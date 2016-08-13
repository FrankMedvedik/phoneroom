using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PhoneLogic.Core.Services
{
    public static class QuoteSrv
    {
        private static readonly string _swansonUrl = "http://ron-swanson-quotes.herokuapp.com/quotes";

        public static async Task<QuotableQuote> GetQuote()
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(_swansonUrl));
            var q = JsonConvert.DeserializeObject<QuotableQuote>(data);
            q.Author = "Ron Swanson";
            q.Source = _swansonUrl;
            return q;
        }
    }

    public class QuotableQuote
    {
        public string Author { get; set; }
        public string Quote { get; set; }
        public string Source { get; set; }
    }
}