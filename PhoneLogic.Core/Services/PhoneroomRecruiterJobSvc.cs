using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Services
{
    public static class PhoneroomRecruiterJobSvc
    {
        public static async Task<List<PhoneroomRecruiterJob>> GetAllPhoneroomRecruiterJobs()
        {
            var client = new WebClient();
            var data =
                await
                    client.DownloadStringTaskAsync(
                        new Uri(ConditionalConfiguration.apiUrl + "PhoneroomRecruiterJobs"));
            var c = JsonConvert.DeserializeObject<List<PhoneroomRecruiterJob>>(data);
            return c;
        }
    }
}