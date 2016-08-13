using System;
using System.ComponentModel.DataAnnotations;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.CallsRpts.Models
{
    public class ByJob
    {
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

        [Display(Name = "Recruiter Cnt")]
        public int? RecruiterCnt { get; set; }

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
        public string FirstCallTimeFormatted
        {
            get { return StringFormatSvc.TimeFormatted(FirstCallTime.GetValueOrDefault(DateTime.MinValue)); }
        }

        [Display(Name = "Last Call")]
        public string LastCallTimeFormatted
        {
            get { return StringFormatSvc.TimeFormatted(FirstCallTime.GetValueOrDefault(DateTime.MinValue)); }
        }

        public DateTime? FirstCallTime { get; set; }
        public DateTime? LastCallTime { get; set; }

        [Display(Name = "Job")]
        public string JobFormatted
        {
            get { return StringFormatSvc.JobAndTaskFormatted(JobNumber); }
        }

        public int TaskID
        {
            get { return StringFormatSvc.TaskFromJobNumber(JobNumber); }
        }

        [Display(Name = "Toll Free #")]
        public string TollFreeFormatted
        {
            get { return StringFormatSvc.PhoneNumberFormatted(TollFreeNumber); }
        }

        [Display(Name = "Total Time in Calls")]
        public string TotalCallDurationFormatted
        {
            get { return StringFormatSvc.DurationFormatted(TotalCallDuration.GetValueOrDefault(0)); }
        }

        [Display(Name = "Avg")]
        public string AvgCallDurationFormatted
        {
            get { return StringFormatSvc.DurationFormatted(AvgCallDuration.GetValueOrDefault(0)); }
        }

        [Display(Name = "Max")]
        public string MaxCallDurationFormatted
        {
            get { return StringFormatSvc.DurationFormatted(MaxCallDuration.GetValueOrDefault(0)); }
        }

        [Display(Name = "Task Type")]
        public string TaskTypeID { get; set; }

        [Display(Name = "Task Name")]
        public string TaskName { get; set; }

        [Display(Name = "Task Desc")]
        public string TaskDscr { get; set; }
    }
}