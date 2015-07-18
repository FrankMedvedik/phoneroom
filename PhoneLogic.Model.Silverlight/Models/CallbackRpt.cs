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
        [Display(Name = "Open Cnt")]
        public int openCnt { get; set; }
        [Display(Name = "Closed Cnt")]
        public int closedCnt { get; set; }
        [Display(Name = "Job")]
        public string JobFormatted
        {
            get
            {
                return jobNum.Substring(0, 4) + "-" + jobNum.Substring(4);
            }
        }

    }
}




