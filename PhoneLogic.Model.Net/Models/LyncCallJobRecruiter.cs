using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneLogic.Model
{
   public class LyncCallJobPhoneRoomRecruiter
    {
        [Display(Name = "Phone Room")]
        public String PhoneRoom { get; set; }
        [Display(Name = "Recruiter")]
        public String DisplayName { get; set; }
        [Display(Name = "Email")]
        public String EmailAddress { get; set; }

       [Display(Name = "Job")]
       public string JobNumber { get; set; }
        [Display(Name = "Task ID")]
        public int taskID { get; set; }
        [Display(Name = "Task")]
        public string taskName { get; set; }
        [Display(Name = "Task Type")]
        public string TaskTypeId { get; set; }
        [Display(Name = "Task Desc")]
        public string taskDscr { get; set; }
        //[Display(Name = "Title")]
        public String Description { get; set; }
        public string recruiterSip { get; set; }
        public Nullable<int> CallCnt { get; set; }
        public Nullable<int> IncomingCallCnt { get; set; }
        public Nullable<int> OutgoingCallCnt { get; set; }
        public Nullable<int> TotalCallDuration { get; set; }
        public Nullable<int> AvgCallDuration { get; set; }
        public Nullable<int> MaxCallDuration { get; set; }
        public Nullable<int> UniqueCallerCnt { get; set; }
        public Nullable<int> CompleteCnt { get; set; }
        public Nullable<int> AbandonedCnt { get; set; }
        public Nullable<int> LeftMsgCnt { get; set; }
        public Nullable<System.DateTime> FirstCallTime { get; set; }
        public Nullable<System.DateTime> LastCallTime { get; set; }
    }
}

 
