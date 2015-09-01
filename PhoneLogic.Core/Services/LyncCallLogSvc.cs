using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Model;



namespace PhoneLogic.Core.Services
{
    public static class LyncCallLogSvc
    {
        public static async Task<List<Call>> GetCalls(String sip, DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "Calls?sip=" + sip + "&startDate=" + startDate.Ticks
                        + "&endDate=" + endDate.Ticks));
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


        public static async Task<List<Call>> GetCalls(String CallerID)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "Calls?callerId=" + CallerID));
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
                new Uri(ConditionalConfiguration.apiUrl + "Calls?&startDate=" + startDate.Ticks + "&endDate=" + endDate.Ticks));
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
                new Uri(ConditionalConfiguration.apiUrl + "LyncCallJobs?startDate=" + startDate.Ticks + "&endDate=" + endDate.Ticks));
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


        public static async Task<List<ByRecruitersForJob>> GetLynJobsForRecruiter( DateTime startDate,
            DateTime endDate, string sip)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "LyncCallJobPhoneRoomRecruiters?startDate=" +
                        startDate.Ticks + "&endDate=" + endDate.Ticks + "&sip=" + sip));
            try
            {
                var z = JsonConvert.DeserializeObject<List<ByRecruitersForJob>>(data);
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



        public static async Task<List<ByRecruitersForJob>> GetLynRecruitersForJob(string job, DateTime startDate,
            DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "LyncCallJobPhoneRoomRecruiters?job=" + job + "&startDate=" +
                        startDate.Ticks + "&endDate=" + endDate.Ticks));
            try
            {
                var z = JsonConvert.DeserializeObject<List<ByRecruitersForJob>>(data);
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



        public static async Task<List<Call>> GetLyncCallLog(string jobNum, string sip, DateTime startDate,
            DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "Calls?jobNum=" + jobNum + "&sip=" + sip + "&startDate=" +
                        startDate.Ticks + "&endDate=" + endDate.Ticks));
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


        public static async Task<List<LyncCallByRecruiter>> GetLyncCallsByRecruiter(DateTime startRptDate,
            DateTime endRptDate)
        {
            {
                try
                {
                    var client = new WebClient();
                    var data = await client.DownloadStringTaskAsync(
                        new Uri(ConditionalConfiguration.apiUrl + "LyncCallByRecruiters?startDate=" + startRptDate.Ticks +
                                "&endDate=" + endRptDate.Ticks +"&primaryEntity=Log"));
                    var z = JsonConvert.DeserializeObject<List<LyncCallByRecruiter>>(data);
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

        public static async Task<List<LyncCallByRecruiter>> GetRecruitersPlusCallSummary(DateTime startRptDate,
            DateTime endRptDate)
        {
            {
                try
                {
                    var client = new WebClient();
                    var data = await client.DownloadStringTaskAsync(
                        new Uri(ConditionalConfiguration.apiUrl + "LyncCallByRecruiters?startDate=" + startRptDate.Ticks +
                                "&endDate=" + endRptDate.Ticks + "&primaryEntity=Recruiter"));
                    var z = JsonConvert.DeserializeObject<List<LyncCallByRecruiter>>(data);
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
}

