﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PhoneLogic.Model 
{
    public class CallInQueue
    {
        public string CallerId { get; set; }

        public string Id { get; set; }

        public string JobNumber { get; set; }
        
        public string TimeIn { get; set; }

        [Display(Name = "Job")]
        public string JobFormatted
        {
            get
            {
                return StringFormatSvc.JobFormatted(JobNumber);
            }
        }

        [Display(Name = "Phone #")]
        public string CallerIdFormatted
        {
            get
            {
                return StringFormatSvc.PhoneNumberFormatted(CallerId);
            }
        }
    }
}
