using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using PhoneLogic.Model;


namespace PhoneLogic.Core.Services
{
    public static class RptSvc
    {
        public static async Task<List<RptJobActivity>> GetJobActivityRpt(DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                     new Uri(ApiWebSite.urlRoot + "JobActivityRpt?startDate="+startDate
                            + "&endDate="+ endDate));
            return JsonConvert.DeserializeObject<List<RptJobActivity>>(data);
        }

        public static async Task<List<RptTodayCallLogSummary>> GetTodaysCallLogSummary()
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                     new Uri(ApiWebSite.urlRoot + "TodayRpt"));
            return JsonConvert.DeserializeObject<List<RptTodayCallLogSummary>>(data);
        }

        public static async Task<List<rptInboundCallByHour>> GetInboundCallsByHour(DateTime startDate,DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                     new Uri(ApiWebSite.urlRoot + "InboundCallRpt?StartDate=" + startDate + "&EndDate="+endDate));
            return JsonConvert.DeserializeObject<List<rptInboundCallByHour>>(data);
        }


    }
}
