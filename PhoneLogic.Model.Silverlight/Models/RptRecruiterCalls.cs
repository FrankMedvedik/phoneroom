using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneLogic.Model
{
    public class RptRecruiterCalls
    {
        public string RecruiterSIP { get; set; }
        public string JobNumber { get; set; }
       
        [Display(Name = "Job")]
        public string JobFormatted
        {
            get
            {
                if (JobNumber == null) return null;
                return JobNumber.Substring(0, 4) + "-" + JobNumber.Substring(4,4);
            }
        }
        [Display(Name = "All Cnt")]
        public Nullable<int> AllCallCnt { get; set; }
        [Display(Name = "In Cnt")]
        public Nullable<int> InCallCnt { get; set; }
        [Display(Name = "Out Cnt")]
        public Nullable<int> OutCallCnt { get; set; }

        [Display(Name = "All Time")]
        public TimeSpan tsAllTotalDuration {
            get
            {
                return TimeSpan.FromSeconds(AllTotalDuration.GetValueOrDefault());                
            }
        }

        [Display(Name = "In Time")]
        public TimeSpan tsInTotalDuration
        {
            get
            {
                return TimeSpan.FromSeconds(InTotalDuration.GetValueOrDefault());
            }
        }

        [Display(Name = "Out Time")]
        public TimeSpan tsOutTotalDuration
        {
            get
            {
                return TimeSpan.FromSeconds(OutTotalDuration.GetValueOrDefault());
            }
        }


        [Display(Name = "All Max")]
        public TimeSpan tsAllMaxDuration
        {
            get
            {
                return TimeSpan.FromSeconds(AllMaxCallDuration.GetValueOrDefault());
            }
        }

        [Display(Name = "In Max")]
        public TimeSpan tsInMaxDuration
        {
            get
            {
                return TimeSpan.FromSeconds(InMaxCallDuration.GetValueOrDefault());
            }
        }

        [Display(Name = "Out Max")]
        public TimeSpan tsOutMaxDuration
        {
            get
            {
                return TimeSpan.FromSeconds(OutMaxCallDuration.GetValueOrDefault());
            }
        }

        [Display(Name = "All Avg")]
        public TimeSpan tsAllAvgDuration
        {
            get
            {
                return TimeSpan.FromSeconds(AllAvgCallDuration.GetValueOrDefault());
            }
        }

        [Display(Name = "In Avg")]
        public TimeSpan tsInAvgDuration
        {
            get
            {
                return TimeSpan.FromSeconds(InAvgCallDuration.GetValueOrDefault());
            }
        }

        [Display(Name = "Out Avg")]
        public TimeSpan tsOutAvgDuration
        {
            get
            {
                return TimeSpan.FromSeconds(OutAvgCallDuration.GetValueOrDefault());
            }
        }

        public Nullable<int> AllTotalDuration { get; set; }
        public Nullable<int> InTotalDuration { get; set; }
        public Nullable<int> OutTotalDuration { get; set; }
        public Nullable<int> AllAvgCallDuration { get; set; }
        public Nullable<int> AllMaxCallDuration { get; set; }
        public Nullable<int> InAvgCallDuration { get; set; }
        public Nullable<int> InMaxCallDuration { get; set; }
        public Nullable<int> OutAvgCallDuration { get; set; }
        public Nullable<int> OutMaxCallDuration { get; set; }
    }
}
    