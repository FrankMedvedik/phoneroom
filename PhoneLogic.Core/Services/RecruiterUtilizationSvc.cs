using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using PhoneLogic.Model;



namespace PhoneLogic.Core.Services
{
    public static class RecruiterUtilizationSvc
    {
        public static async Task<List<RecruiterTimeSummary>> GetRecruiterTimeSummaryTest(String Phoneroom, DateTime startDate, DateTime endDate)
        {

            return   JsonConvert.DeserializeObject<List<RecruiterTimeSummary>>(RecruiterTimeTestData);
        }

        public static async Task<List<RecruiterTimeSummary>> GetRecruiterTimeSummary(String phoneroom,
            DateTime startDate, DateTime endDate)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(
                new Uri(ConditionalConfiguration.apiUrl + "RecruiterTimeSummary?phoneroom=" + phoneroom + "&startDate=" + startDate.Ticks
                        + "&endDate=" + endDate.Ticks));
            try
            {
                var z = JsonConvert.DeserializeObject<List<RecruiterTimeSummary>>(data);
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

        public static async Task<List<RecruiterActivity>> GetRecruiterActivity(string recruiterSIP, DateTime startDate, DateTime endDate)
        {
                return JsonConvert.DeserializeObject<List<RecruiterActivity>>(RecruiterActivityTestData);
        }

        private static string RecruiterActivityTestData = "";

        private static string RecruiterTimeTestData =
            "[{\"$id\":\"1\",\"RecruiterSIP\":\"sip:abarrios@reckner.com\",\"PhoneRoom\":\"Milwaukee\",\"DisplayName\":\"Alexandra Barrios\",\"EmailAddress\":\"abarrios@reckner.com\",\"Description\":null,\"OutboundCallCnt\":6,\"InboundCallCnt\":3,\"IdleTime\":100,\"CallTime\":50},{\"$id\":\"2\",\"RecruiterSIP\":\"sip:akarnowski@reckner.com\",\"PhoneRoom\":\"Milwaukee\",\"DisplayName\":\"Allyson Karnowski\",\"EmailAddress\":\"akarnowski@reckner.com\",\"Description\":\"MKE Project Manager\",\"OutboundCallCnt\":6,\"InboundCallCnt\":2,\"IdleTime\":40,\"CallTime\":40},{\"$id\":\"3\",\"RecruiterSIP\":\"sip:agervais@reckner.com\",\"PhoneRoom\":\"Milwaukee\",\"DisplayName\":\"Amber Gervais\",\"EmailAddress\":\"agervais@reckner.com\",\"Description\":\"MKE Recruiter\",\"OutboundCallCnt\":4,\"InboundCallCnt\":7,\"IdleTime\":10,\"CallTime\":303},{\"$id\":\"4\",\"RecruiterSIP\":\"sip:akoleske@reckner.com\",\"PhoneRoom\":\"Milwaukee\",\"DisplayName\":\"Amy Koleske\",\"EmailAddress\":\"akoleske@reckner.com\",\"Description\":\"MKE Recruiter\",\"OutboundCallCnt\":8,\"InboundCallCnt\":9,\"IdleTime\":1000,\"CallTime\":30},{\"$id\":\"5\",\"RecruiterSIP\":\"sip:arostankowski@reckner.com\",\"PhoneRoom\":\"Milwaukee\",\"DisplayName\":\"Amy Rostankowski\",\"EmailAddress\":\"arostankowski@reckner.com\",\"Description\":\"MKE Recruiter\",\"OutboundCallCnt\":6,\"InboundCallCnt\":4,\"IdleTime\":300,\"CallTime\":900},{\"$id\":\"7\",\"RecruiterSIP\":\"sip:amedrow@reckner.com\",\"PhoneRoom\":\"Milwaukee\",\"DisplayName\":\"Anne Medrow\",\"EmailAddress\":\"amedrow@reckner.com\",\"Description\":\"MKE Recruiter\",\"OutboundCallCnt\":7,\"InboundCallCnt\":9,\"IdleTime\":1000,\"CallTime\":400},{\"$id\":\"9\",\"RecruiterSIP\":\"sip:csosa@reckner.com\",\"PhoneRoom\":\"Milwaukee\",\"DisplayName\":\"Carmen Sosa\",\"EmailAddress\":\"csosa@reckner.com\",\"Description\":\"MKE Recruiter\",\"OutboundCallCnt\":6,\"InboundCallCnt\":2,\"IdleTime\":1000,\"CallTime\":59},{\"$id\":\"10\",\"RecruiterSIP\":\"sip:crekowski@reckner.com\",\"PhoneRoom\":\"Milwaukee\",\"DisplayName\":\"Carol Rekowski\",\"EmailAddress\":\"crekowski@reckner.com\",\"Description\":\"MKE Recruiter\",\"OutboundCallCnt\":9,\"InboundCallCnt\":1,\"IdleTime\":300,\"CallTime\":400},{\"$id\":\"11\",\"RecruiterSIP\":\"sip:dbonilla@reckner.com\",\"PhoneRoom\":\"Milwaukee\",\"DisplayName\":\"Darlene Bonilla\",\"EmailAddress\":\"dbonilla@reckner.com\",\"Description\":\"MKE Recruiter\",\"OutboundCallCnt\":50,\"InboundCallCnt\":2,\"IdleTime\":900,\"CallTime\":3400},{\"$id\":\"12\",\"RecruiterSIP\":\"sip:duehling@reckner.com\",\"PhoneRoom\":\"Milwaukee\",\"DisplayName\":\"Denise Uehling\",\"EmailAddress\":\"duehling@reckner.com\",\"Description\":\"MKE Recruiter\",\"OutboundCallCnt\":19,\"InboundCallCnt\":3,\"IdleTime\":909,\"CallTime\":1000},{\"$id\":\"17\",\"RecruiterSIP\":\"sip:hbraley@reckner.com\",\"PhoneRoom\":\"Milwaukee\",\"DisplayName\":\"Holly Braley\",\"EmailAddress\":\"hbraley@reckner.com\",\"Description\":\"MKE Recruiter\",\"OutboundCallCnt\":55,\"InboundCallCnt\":3,\"IdleTime\":999,\"CallTime\":193},{\"$id\":\"18\",\"RecruiterSIP\":\"sip:iwallace@reckner.com\",\"PhoneRoom\":\"Chalfont\",\"DisplayName\":\"Ida Wallace\",\"EmailAddress\":\"iwallace@reckner.com\",\"Description\":\"Chalfont Recruiter (Formally Ida Santiago)\",\"OutboundCallCnt\":12,\"InboundCallCnt\":5,\"IdleTime\":8773,\"CallTime\":674}]";
    }
}

