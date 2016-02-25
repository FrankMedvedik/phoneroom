using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PhoneLogic.Model.Models
{
    public class CallbackRpt
    {
        public string jobNum { get; set; }
       [Display(Name = "Task")]
        public int taskID { get; set; }
       [Display(Name = "Topic")]
        public string taskName { get; set; }
        [Display(Name = "Type")]
        public string TaskTypeId { get; set; }
        [Display(Name = "Cnt")]
        public int CallbackCnt { get; set; }

        [Display(Name = "Job")]
        public string JobFormatted
        {
            get
            {
                return StringFormatSvc.JobFormatted(jobNum) + ":"+ taskID.ToString().PadLeft(2,'0');
            }
        }
        public String tollFreeNumber { get; set; }


        [Display(Name = "Toll Free #")]
        public string TollFreeFormatted
        {
            get
            {
                return StringFormatSvc.PhoneNumberFormatted(tollFreeNumber);
            }
        }

    }
}




