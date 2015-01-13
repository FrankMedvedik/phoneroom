
namespace PhoneLogic.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

#if SILVERLIGHT
    public class RecruiterLog : INotifyPropertyChanged
#else
    public class RecruiterLog
#endif
    {
        [Display(Name = "Event")]
        public string agentevent { get; set; }
        public string agentid { get; set; }
        public string callerid { get; set; }
        [Display(Name = "Event Description")]
        public string eventdscr { get; set; }
        [Display(Name = "Event Time")]
        public System.DateTime eventtime { get; set; }
        public string jobnum { get; set; }
        public int logid { get; set; }
        [Display(Name = "Task")]
        public Nullable<int> taskid { get; set; }
        [Display(Name = "Job")]
        public string jobFormatted
        {
            get
            {
                return jobnum.Substring(0, 4) + "-" + jobnum.Substring(4);
            }
        }
        
        [Display(Name = "Phone #")]
        public string phoneFormatted
        {
            get
            {
                return String.Format("{0:(###) ###-####}", double.Parse(callerid));
            }
        }
    }
}


