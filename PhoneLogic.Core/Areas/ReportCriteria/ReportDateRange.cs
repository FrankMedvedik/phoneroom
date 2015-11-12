using System;
using System.Collections.Generic;

namespace PhoneLogic.Core.Areas.ReportCriteria
{
    public class ReportDateRange 
    {
        public DateTime EndRptDate { get; set; }
        public DateTime StartRptDate { get; set; }
        public string ToFormattedString(char token)
        {
            var v = new List<KeyValuePair<string, string>>();
            string s = string.Empty;
            v.Add(new KeyValuePair<string, string>("StartDate", StartRptDate.ToString("yyyyMMdd")));
            v.Add(new KeyValuePair<string, string>("EndDate", EndRptDate.ToString("yyyyMMdd")));
            foreach (var a in v)
            {
                s = string.Concat(s, token, a.Key, token, a.Value);
            }
            return s;
        }
    }
}