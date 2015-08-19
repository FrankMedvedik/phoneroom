using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhoneLogic.Model 
{
    
    public class PhoneLogicTask 
    {
        [Display(Name = "Job")]
        public string JobFormatted
        {
            get
            {
                return StringFormatSvc.JobAndTaskFormatted(JobNum) + ":0" + TaskID;
                
            }
        }
        [Display(Name = "Task ID")]
        public int? TaskID { get; set; }

        public string JobNum { get; set; }

        [Display(Name = "Topic")]
        public string TaskName { get; set; }

        [Display(Name = "Description")]
        public string TaskDscr { get; set; }

        [Display(Name = "Type")]
        public string TaskTypeID { get; set; }

        [Display(Name = "Tollfree Number")]
        public string tollFreeNumber { get; set; }

        [Display(Name = "Toll Free #")]
        public string TollFreeFormatted
        {
            get
            {
                return StringFormatSvc.PhoneNumberFormatted(tollFreeNumber);
            }
        }
        public string RecruiterSIP { get; set; }
        [Display(Name = "First Msg")]
        public Nullable<System.DateTime> OldestMsg { get; set; }
        [Display(Name = "Last Msg")]
        public Nullable<System.DateTime> NewestMsg { get; set; }
        [Display(Name = "Msg Cnt")]
        public Nullable<int> MsgCnt { get; set; }
        [Display(Name = "Total Call Cnt")]
        public Nullable<int> CallCnt { get; set; }
        [Display(Name = "Incoming")]
        public Nullable<int> InboundCallCnt { get; set; }
        [Display(Name = "Outgoing")]
        public Nullable<int> OutboundCallCnt { get; set; }
        public Nullable<int> TotalCallDuration { get; set; }
        public Nullable<int> AvgCallDuration { get; set; }
        public Nullable<int> MaxCallDuration { get; set; }

        [Display(Name = "Unq Caller Cnt")]
        public Nullable<int> UniqueCallerCnt { get; set; }
        [Display(Name = "First Call")]
        public Nullable<System.DateTime> FirstCallTime { get; set; }
        [Display(Name = "Last Call")]
        public Nullable<System.DateTime> LastCallTime { get; set; }
        [Display(Name = "Total Time in Calls")]
        public string TotalCallDurationFormatted
        {
            get
            {
                return StringFormatSvc.DurationFormatted(TotalCallDuration.GetValueOrDefault(0));
                // var retval = string.Format("{0:00}:{1:00}:{2:00}", TotalCallDuration / 3600, (TotalCallDuration / 60) % 60, TotalCallDuration % 60);
                //if(retval == "::")
                //    retval = null;
                //return retval;

            }
        }
        [Display(Name = "Avg")]
        public string AvgCallDurationFormatted
        {
            get
            {
                return StringFormatSvc.DurationFormatted(AvgCallDuration.GetValueOrDefault(0));
                //string retval = string.Format("{0:00}:{1:00}:{2:00}", AvgCallDuration / 3600, (AvgCallDuration / 60) % 60, AvgCallDuration % 60);
                //if(retval == "::")
                //    retval = null;
                //return retval;
            }
        }
        [Display(Name = "Max")]
        public string MaxCallDurationFormatted
        {
            get
            {
                return StringFormatSvc.DurationFormatted(MaxCallDuration.GetValueOrDefault(0));
                //string retval = string.Format("{0:00}:{1:00}:{2:00}", MaxCallDuration/3600, (MaxCallDuration/60)%60,
                //    MaxCallDuration%60);
                //if (retval == "::")
                //    retval = null;
                //return retval;
            }
        }
    }
}