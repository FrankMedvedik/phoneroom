using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneLogic.Model
{
    public class rptInboundCallByHour
    {
        [Display(Name = "Date")]
        public string WorkDate { get; set; }
        [Display(Name = "< 9am")]
        public int? Before_9am { get; set; }
        [Display(Name = "9am")]
        public int? C9_am { get; set; }
        [Display(Name = "10am")]
        public int? C10_am { get; set; }
        [Display(Name = "11am")]
        public int? C11_am { get; set; }
        [Display(Name = "12pm")]
        public int? C12_pm { get; set; }
        [Display(Name = "1pm")]
        public int? C1_pm { get; set; }
        [Display(Name = "2pm")]
        public int? C2_pm { get; set; }
        [Display(Name = "3pm")]
        public int? C3_pm { get; set; }
        [Display(Name = "4pm")]
        public int? C4_pm { get; set; }
        [Display(Name = "5pm")]
        public int? C5_pm { get; set; }
        [Display(Name = "6pm")]
        public int? C6_pm { get; set; }
        [Display(Name = "7pm")]
        public int? C7_pm { get; set; }
        [Display(Name = "8pm")]
        public int? C8_pm { get; set; }
        [Display(Name = "9pm")]
        public int? C9_pm { get; set; }
        [Display(Name = "10pm")]
        public int? C10_pm { get; set; }
        [Display(Name = "10pm < ")]
        public int? after_10pm { get; set; }
    }
}
