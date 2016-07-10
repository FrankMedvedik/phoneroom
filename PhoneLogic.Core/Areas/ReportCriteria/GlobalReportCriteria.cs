using System.Collections.Generic;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.ReportCriteria
{
    public class GlobalReportCriteria
    {
        public ReportDateRange ReportDateRange = new ReportDateRange();
        public string Phoneroom { get; set; }

        public Dictionary<string, PhoneroomRecruiterJob> RecruiterJobs { get; set; } 
        public List<Recruiter> PhoneroomRecruiters { get; set; }
        public List<PhoneLogicTask> PhoneroomJobs { get; set; }

        public string ToFormattedString(char token)
        {
            var v = new List<KeyValuePair<string, string>>();
            string s = string.Empty;
            v.Add(new KeyValuePair<string, string>("Phoneroom", Phoneroom));
            v.Add(new KeyValuePair<string, string>("StartDate", ReportDateRange.StartRptDate.ToString("yyyyMMdd")));
            v.Add(new KeyValuePair<string, string>("EndDate", ReportDateRange.EndRptDate.ToString("yyyyMMdd")));
            foreach (var a in v)
            {
                s = string.Concat(s, token, a.Key, token, a.Value);
            }
            return s;
        }
    }
}