using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhoneLogic.Model
{
    public class PhoneCall 
    {
        [Display(Name = "Phone")]
        public string phoneNum { get; set; }

        public string SIP { get; set; }

        [Display(Name = "Call Start Date")]
        public DateTime StartCallDate { get; set; }

        [Display(Name = "Job")]
        public string jobNum { get; set; }

        [Display(Name = "Task")]
        public int taskID { get; set; }

        [Display(Name = "Event Desc")]
        public string EventDesc { get; set; }

        public int callbackID { get; set; }

        public string JobFormatted
        {
            get { return jobNum + ":0" + taskID; }
        }
    }
}
