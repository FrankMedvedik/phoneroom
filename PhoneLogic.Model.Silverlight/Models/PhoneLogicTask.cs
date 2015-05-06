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

        [Display(Name = "Oldest Call")]
        public DateTime? oldest_call { get; set; }

        [Display(Name = "Most Recent Call")]
        public DateTime? newest_call { get; set; }

        [Display(Name = "# Messages")]
        public int? call_cnt { get; set; }

    }
}
