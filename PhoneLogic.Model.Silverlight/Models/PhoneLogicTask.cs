using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneLogic.Model 
{
public class PhoneLogicTask
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

    private string _jobNum;

    public string JobNum
    {
        set
        {
         _jobNum = value;
        #if SILVERLIGHT
                    NotifyPropertyChanged();
        #endif
        }
        get { return _jobNum; }
    }
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
        [Display(Name = "Call Cnt")]
        public int? call_cnt { get; set; }
    }
    
}