using System;
using System.ComponentModel.DataAnnotations;


namespace PhoneLogic.Model
{
    public class RptTodayCallLogSummary
    {
        public string jobNum { get; set; }
        public int taskID { get; set; }
        [Display(Name = "Topic")]
        public string taskName { get; set; }
        [Display(Name = "Topic Description")]
        public string taskDscr { get; set; }
        [Display(Name = "Task Type")]
        public string taskTypeID { get; set; }
        public string taskStatusID { get; set; }
        public string tollfreenumber { get; set; }

        [Display(Name = "Inbound")]
        public Nullable<int> InboundCalls { get; set; }
        [Display(Name = "No Recruiters")]
        public Nullable<int> NoAgents { get; set; }
        
        [Display(Name = "Abandoned")]
        public Nullable<int> Abandoned { get; set; }
        [Display(Name = "Left Message")]
        public Nullable<int> LeftMessage { get; set; }
        [Display(Name = "Callback")]
        public Nullable<int> CallBack { get; set; }
        [Display(Name = "OutBound")]
        public Nullable<int> PlacedCall { get; set; }
        [Display(Name = "Job")]
        public string jobFormatted
        {
            get
            {
                return jobNum.Substring(0, 4) + "-" + jobNum.Substring(4);
            }
        }
        [Display(Name = "Toll Free #")]
        public string tollFreeFormatted
        {
            get
            {
                if (tollfreenumber != null)
                    return String.Format("{0:(###) ###-####}", double.Parse(tollfreenumber));
                else
                    return "";
            }
        }

    }
}
