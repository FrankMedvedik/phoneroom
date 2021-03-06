﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Services
{
    public static class RecruiterSvc
    {
        public static async Task<List<RecruiterLog>> GetRecruiterLog(string sip, DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "RecruiterCallLogs?sip=" + sip + "&startDate=" +
                        startDate.Ticks
                        + "&endDate=" + endDate.Ticks));
            try
            {
                var z = JsonConvert.DeserializeObject<List<RecruiterLog>>(data);
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

        public static async Task<List<Recruiter>> GetActiveRecruiters(DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "Recruiter?startDate=" + startDate.Ticks + "&endDate=" +
                        endDate.Ticks));
            try
            {
                var z = JsonConvert.DeserializeObject<List<Recruiter>>(data);
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

        public static async Task<List<RptRecruiterCalls>> GetRecruiterCallRpt(DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "recruiterCallRpt?&startDate=" + startDate.Ticks + "&endDate=" +
                        endDate.Ticks));
            try
            {
                var z = JsonConvert.DeserializeObject<List<RptRecruiterCalls>>(data);
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


        public static async Task<List<Recruiter>> GetRecruiters()
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "recruiter"));
            try
            {
                var z = JsonConvert.DeserializeObject<List<Recruiter>>(data);
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