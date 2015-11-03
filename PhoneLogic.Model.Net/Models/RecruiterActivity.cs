using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneLogic.Model
{

    public class RecruiterActivitySummary
    {
        [Display(Name = "ReviewDate")]
        public System.DateTime ReviewDate { get; set; }

        [Display(Name = "Phone Room")]
        public String PhoneRoom { get; set; }

        [Display(Name = "Recruiter")]
        public String DisplayName { get; set; }

        [Display(Name = "Email")]
        public String EmailAddress { get; set; }

        [Display(Name = "Title")]
        public String Description { get; set; }

        [Display(Name = "Start")]
        public System.DateTime StartTime { get; set; }

        [Display(Name = "SIP")]
        public string RecruiterSIP { get; set; }

        [Display(Name = "End")]
        public Nullable<System.DateTime> EndTime { get; set; }

        [Display(Name = "ActivityType")]
        public string ActivityType { get; set; }

        [Display(Name = "Duration")]
        public long Duration { get; set; }
    }


    [Serializable]
    public class RecruiterActivity
    {
        [Display(Name = "ReviewDate")]
        public System.DateTime ReviewDate { get; set; }

        [Display(Name = "Phone Room")]
        public String PhoneRoom { get; set; }

        [Display(Name = "Recruiter")]
        public String DisplayName { get; set; }

        [Display(Name = "Email")]
        public String EmailAddress { get; set; }

        [Display(Name = "Title")]
        public String Description { get; set; }

        [Display(Name = "Start")]
        public System.DateTime StartTime { get; set; }

        [Display(Name = "SIP")]
        public string RecruiterSIP { get; set; }

        [Display(Name = "End")]
        public Nullable<System.DateTime> EndTime { get; set; }

        [Display(Name = "ActivityName")]
        public string ActivityName { get; set; }

        [Display(Name = "ActivityType")]
        public string ActivityType { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Duration")]
        public long Duration { get; set; }

    }
}