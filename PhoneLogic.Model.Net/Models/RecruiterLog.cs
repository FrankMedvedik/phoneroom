
namespace PhoneLogic.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;


#if SILVERLIGHT
    public class Recruiter : INotifyPropertyChanged
#else
    public class RecruiterEvents
#endif
    {
        [Display(Name = "SIP")]
        public String sip { get; set; }
        public string jobNum { get; set; }
        [Display(Name = "Task")]
        public int taskID { get; set; }
        [Display(Name = "Job")]
        public string jobFormatted
        {
            get
            {
                return jobNum.Substring(0, 4) + "-" + jobNum.Substring(4);
            }
        }
        [Display(Name = "Event")]
        public String Event { get; set; }
        [Display(Name = "Start Time")]
        public DateTime StartTime{ get; set; }
        [Display(Name = "Duration")]
        public int Duration { get; set; }
        [Display(Name = "Event Description")]
        public String EventDesc { get; set; }
        [Display(Name = "Phone")]
        public string callerID { get; set; }
    }
}

