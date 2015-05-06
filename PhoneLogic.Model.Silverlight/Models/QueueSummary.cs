namespace PhoneLogic.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class QueueSummary
    {
        [Display(Name = "Job")]
        public string jobFormatted
        {
            get
            {
                return JobNumber.Substring(0, 4) + "-" + JobNumber.Substring(4,4);
            }
        }

        public string JobNumber { get; set; }
        [Display(Name = "Calls")]
        public int InQueue { get; set; }
        
    }
}
