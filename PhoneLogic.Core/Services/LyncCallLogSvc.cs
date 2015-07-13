using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using PhoneLogic.Core.areas.CallsRpts.Models;
using PhoneLogic.Model;



namespace PhoneLogic.Core.Services
{
    public static class LyncCallLogSvc
    {
        public static async Task<List<Call>> GetCall(String sip, DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "RecruiterLynCallLogs?sip=" + sip + "&startDate=" + startDate
                        + "&endDate=" + endDate));
            try
            {
                var z = JsonConvert.DeserializeObject<List<Call>>(data);
                return z;
            }
            catch (Exception e)
            {
                if (e.InnerException == null)
                    Console.Write(e.Message + e.StackTrace);
                else
                    Console.Write(e.Message + e.InnerException.Message + e.StackTrace);
                return null;
            }

        }


        public static async Task<List<Call>> GetLyncCallLog(DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "RecruiterLyncCallLogs?&startDate=" + startDate + "&endDate=" + endDate));
            try
            {
                var z = JsonConvert.DeserializeObject<List<Call>>(data);
                return z;
            }
            catch (Exception e)
            {
                if (e.InnerException == null)
                    Console.Write(e.Message + e.StackTrace);
                else
                    Console.Write(e.Message + e.InnerException.Message + e.StackTrace);
                return null;
            }

        }


        public static async Task<List<ByJob>> GetLynCallsByJob(DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "LyncCallJobs?startDate=" + startDate + "&endDate=" + endDate));
            try
            {
                var z = JsonConvert.DeserializeObject<List<ByJob>>(data);
                return z;
            }
            catch (Exception e)
            {
                if (e.InnerException == null)
                    Console.Write(e.Message + e.StackTrace);
                else
                    Console.Write(e.Message + e.InnerException.Message + e.StackTrace);
                return null;
            }

        }


        public static async Task<List<ByRecruiter>> GetLyncCallByRecruiter(DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "LyncCallByRecruiters?&startDate=" + startDate + "&endDate=" + endDate));
            try
            {
                var z = JsonConvert.DeserializeObject<List<ByRecruiter>>(data);
                return z;
            }
            catch (Exception e)
            {
                if (e.InnerException == null)
                    Console.Write(e.Message + e.StackTrace);
                else
                    Console.Write(e.Message + e.InnerException.Message + e.StackTrace);
                return null;
            }

        }




    }
}

