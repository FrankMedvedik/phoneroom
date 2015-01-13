namespace PhoneLogic.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;


#if SILVERLIGHT
    public class myCallback : INotifyPropertyChanged

#else
    public class jobCallStat
    {
#endif
        [Display(Name = "Job")]
        public string jobFormatted
        {
            get
            {
                return jobNum.Substring(0, 4) + "-" + jobNum.Substring(4);
            }
        }
        public string jobNum { get; set; }
        [Display(Name = "Task ID")]
        public int taskID { get; set; }
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }
        [Display(Name = "Task Type")]
        public string TaskTypeID { get; set; }
        [Display(Name = "Task Status")]
        public string TaskStatusID { get; set; }
        [Display(Name = "Oldest Call")]
        public Nullable<DateTime> oldest_call { get; set; }
        [Display(Name = "Most Recent Call")]
        public Nullable<DateTime> newest_call { get; set; }
        [Display(Name = "Call Cnt")]
        public Nullable<int> call_cnt { get; set; }
    }
}

