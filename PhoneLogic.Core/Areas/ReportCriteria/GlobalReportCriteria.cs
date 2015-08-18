using System.Collections.Generic;
using PhoneLogic.Core.Areas.CallsRpts.Models;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.ReportCriteria
{
    public class GlobalReportCriteria
    {
        public ReportDateRange ReportDateRange = new ReportDateRange();
        public string Phoneroom {get; set;}
        
       public Dictionary<string, PhoneroomRecruiterJob> RecruiterJobs{get; set;}
        public List<Recruiter> PhoneroomRecruiters { get; set; }
        public List<PhoneLogicTask> PhoneroomJobs { get; set; }

    }

    
}
