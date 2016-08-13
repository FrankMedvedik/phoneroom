using System;
using System.ComponentModel.DataAnnotations;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.CallsRpts.Models
{
    public class ByRecruitersForJob
    {
        public string recruiterSip { get; set; }

        [Display(Name = "Phone Room")]
        public string PhoneRoom { get; set; }

        [Display(Name = "Recruiter")]
        public string DisplayName { get; set; }

        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        public string JobNumber { get; set; }

        [Display(Name = "Total Call Cnt")]
        public int? CallCnt { get; set; }

        [Display(Name = "Incoming")]
        public int? IncomingCallCnt { get; set; }

        [Display(Name = "Outgoing")]
        public int? OutgoingCallCnt { get; set; }

        public int? TotalCallDuration { get; set; }
        public int? AvgCallDuration { get; set; }
        public int? MaxCallDuration { get; set; }

        [Display(Name = "Unique Callers")]
        public int? UniqueCallerCnt { get; set; }

        [Display(Name = "Complete")]
        public int? CompleteCnt { get; set; }

        [Display(Name = "Hung Up")]
        public int? AbandonedCnt { get; set; }

        [Display(Name = "Left Msg")]
        public int? LeftMsgCnt { get; set; }

        public string TollFreeNumber { get; set; }

        [Display(Name = "First Call")]
        public DateTime? FirstCallTime { get; set; }

        [Display(Name = "Last Call")]
        public DateTime? LastCallTime { get; set; }

        [Display(Name = "Job")]
        public string JobFormatted
        {
            get { return StringFormatSvc.JobAndTaskFormatted(JobNumber); }
        }


        [Display(Name = "Toll Free #")]
        public string TollFreeFormatted
        {
            get
            {
                if (TollFreeNumber != null)
                    return string.Format("{0:(###) ###-####}", double.Parse(TollFreeNumber));
                return "";
            }
        }

        [Display(Name = "Total Time in Calls")]
        public string TotalCallDurationFormatted
        {
            get { return string.Format("{0:00}:{1:00}", TotalCallDuration/3600, TotalCallDuration/60%60); }
        }

        [Display(Name = "Avg")]
        public string AvgCallDurationFormatted
        {
            get { return string.Format("{0:00}:{1:00}", AvgCallDuration/3600, AvgCallDuration/60%60); }
        }

        [Display(Name = "Max")]
        public string MaxCallDurationFormatted
        {
            get { return string.Format("{0:00}:{1:00}", MaxCallDuration/3600, MaxCallDuration/60%60); }
        }

        [Display(Name = "Task Type")]
        public string TaskTypeID { get; set; }

        [Display(Name = "Task Name")]
        public string TaskName { get; set; }

        [Display(Name = "Task Desc")]
        public string TaskDscr { get; set; }
    }
}