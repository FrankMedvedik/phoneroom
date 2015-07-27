using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#if SILVERLIGHT
using Silverlight.Base.MVVMBaseTypes;
#endif

namespace PhoneLogic.Model 
{
    
#if SILVERLIGHT
    public class PhoneLogicTask : ViewModelBase
#else
    public class PhoneLogicTask
#endif
    {
        [Display(Name = "Job")]
        public string jobFormatted
        {
            get
            {
                if (JobNum == null) return null;
                return JobNum.Substring(0, 4) + "-" + JobNum.Substring(4);
            }
        }
        [Display(Name = "Task ID")]
        public int? taskID { get; set; }

        public string JobNum { get; set; }

        public int TaskId;

        [Display(Name = "Topic")]
        public string taskName { get; set; }

        [Display(Name = "Description")]
        public string taskDscr { get; set; }

        [Display(Name = "Type")]
        public string taskTypeID { get; set; }

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
                return string.Format("{0:00}:{1:00}:{2:00}", TotalCallDuration / 3600, (TotalCallDuration / 60) % 60, TotalCallDuration % 60);
            }
        }
        [Display(Name = "Avg")]
        public string AvgCallDurationFormatted
        {
            get
            {
                return string.Format("{0:00}:{1:00}:{2:00}", AvgCallDuration / 3600, (AvgCallDuration / 60) % 60, AvgCallDuration % 60);
            }
        }
        [Display(Name = "Max")]
        public string MaxCallDurationFormatted
        {
            get
            {
                return string.Format("{0:00}:{1:00}:{2:00}", MaxCallDuration / 3600, (MaxCallDuration / 60) % 60, MaxCallDuration % 60);
            }
        }
    }
}