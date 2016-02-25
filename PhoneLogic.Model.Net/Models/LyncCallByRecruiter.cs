using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneLogic.Model
{
   public class LyncCallByRecruiter
    {
        [Display(Name = "Phone Room")]
        public String PhoneRoom { get; set; }
        [Display(Name = "Recruiter")]
        public String DisplayName { get; set; }
        [Display(Name = "Email")]
        public String EmailAddress { get; set; }
        [Display(Name = "Title")]
        public String Description { get; set; }
       [Display(Name = "Status")]
        public string RecruiterSIP { get; set; }
       [Display(Name = "Job Cnt")]
        public Nullable<int> JobCnt { get; set; }
        [Display(Name = "Total Call Cnt")]
        public Nullable<int> CallCnt { get; set; }
        [Display(Name = "Incoming")]
        public Nullable<int> IncomingCallCnt { get; set; }
        [Display(Name = "Outgoing")]
        public Nullable<int> OutgoingCallCnt { get; set; }
        public Nullable<int> TotalCallDuration { get; set; }
        public Nullable<int> AvgCallDuration { get; set; }
        public Nullable<int> MaxCallDuration { get; set; }
        [Display(Name = "Unique Callers")]
        public Nullable<int> UniqueCallerCnt { get; set; }
        [Display(Name = "Complete")]
        public Nullable<int> CompleteCnt { get; set; }
        public Nullable<int> LeftMsgCnt { get; set; }
        public Nullable<int> AbandondedCnt { get; set; }

       [Display(Name = "First Call")]
       public string FirstCallTimeFormatted
       {
           get { return StringFormatSvc.TimeFormatted(FirstCallTime.GetValueOrDefault(DateTime.MinValue)); } 
       }

       [Display(Name = "Last Call")]
       public string LastCallTimeFormatted
       {
           get { return StringFormatSvc.TimeFormatted(LastCallTime.GetValueOrDefault(DateTime.MinValue)); }
       }

       public Nullable<System.DateTime> FirstCallTime { get; set; }
        public Nullable<System.DateTime> LastCallTime { get; set; }  
        [Display(Name = "Total Time in Calls")]
        public string TotalCallDurationFormatted
        {
            get
            {
                return string.Format("{0:00}:{1:00}", TotalCallDuration / 3600, (TotalCallDuration / 60) % 60);
            }
        }
        [Display(Name = "Avg")]
        public string AvgCallDurationFormatted
        {
            get
            {
                return string.Format("{0:00}:{1:00}", AvgCallDuration / 3600, (AvgCallDuration / 60) % 60);
            }
        }
        [Display(Name = "Max")]
        public string MaxCallDurationFormatted
        {
            get
            {
                return string.Format("{0:00}:{1:00}", MaxCallDuration / 3600, (MaxCallDuration / 60) % 60);
            }
        }
   
   }
}

 
