using System;
using System.ComponentModel.DataAnnotations;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Areas.CallsRpts.Models
{
    public class ByJob
    {

        public string JobNumber { get; set; }
        [Display(Name = "Total Call Cnt")]
        public Nullable<int> CallCnt { get; set; }
        [Display(Name = "Incoming")]
        public Nullable<int> IncomingCallCnt { get; set; }
        [Display(Name = "Outgoing")]
        public Nullable<int> OutgoingCallCnt { get; set; }
        public Nullable<int> TotalCallDuration { get; set; }
        public Nullable<int> AvgCallDuration { get; set; }
        public Nullable<int> MaxCallDuration { get; set; }
        [Display(Name = "Recruiter Cnt")]
        public Nullable<int> RecruiterCnt { get; set; }
        [Display(Name = "Unique Callers")]
        public Nullable<int> UniqueCallerCnt { get; set; }
        [Display(Name = "Complete")]
        public Nullable<int> CompleteCnt { get; set; }
        [Display(Name = "Hung Up")]
        public Nullable<int> AbandonedCnt { get; set; }
        [Display(Name = "Left Msg")]
        public Nullable<int> LeftMsgCnt { get; set; }
        public string TollFreeNumber { get; set; }
        [Display(Name = "First Call")]
        public Nullable<System.DateTime> FirstCallTime { get; set; }
        [Display(Name = "Last Call")]
        public Nullable<System.DateTime> LastCallTime { get; set; }
        [Display(Name = "Job")]
        public string JobFormatted
        {
            get
            {
                return StringFormatSvc.JobAndTaskFormatted(JobNumber);
            }
        }

        public int TaskID
        {
            get { return StringFormatSvc.TaskFromJobNumber(JobNumber); }
        }

        [Display(Name = "Toll Free #")]
        public string TollFreeFormatted
        {
            get
            {
                return StringFormatSvc.PhoneNumberFormatted(TollFreeNumber);
            }
        }

        [Display(Name = "Total Time in Calls")]
        public string TotalCallDurationFormatted
        {
            get
            {
                var retval = string.Format("{0:00}:{1:00}:{2:00}", TotalCallDuration / 3600, (TotalCallDuration / 60) % 60, TotalCallDuration % 60);
                if (retval == "::")
                    retval = null;
                return retval;

            }
        }
        [Display(Name = "Avg")]
        public string AvgCallDurationFormatted
        {
            get
            {
                string retval = string.Format("{0:00}:{1:00}:{2:00}", AvgCallDuration / 3600, (AvgCallDuration / 60) % 60, AvgCallDuration % 60);
                if (retval == "::")
                    retval = null;
                return retval;
            }
        }
        [Display(Name = "Max")]
        public string MaxCallDurationFormatted
        {
            get
            {
                string retval = string.Format("{0:00}:{1:00}:{2:00}", MaxCallDuration / 3600, (MaxCallDuration / 60) % 60,
                    MaxCallDuration % 60);
                if (retval == "::")
                    retval = null;
                return retval;
            }
        }
    }
}