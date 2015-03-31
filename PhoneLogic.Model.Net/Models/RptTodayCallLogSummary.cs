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
        [Display(Name = "Description")]
        public string taskDscr { get; set; }
        [Display(Name = "Task Type")]
        public string taskTypeID { get; set; }
        public string taskStatusID { get; set; }
        public string tollfreenumber { get; set; }

        [Display(Name = "In")]
        public Nullable<int> InboundCalls { get; set; }
        [Display(Name = "No Answer")]
        public Nullable<int> NoAgents { get; set; }
        [Display(Name = "Hung up")]
        public Nullable<int> Abandoned { get; set; }
        [Display(Name = "Message")]
        public Nullable<int> LeftMessage { get; set; }
        [Display(Name = "CallBack")]
        public Nullable<int> CallBack { get; set; }
        [Display(Name = "Out")]
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
