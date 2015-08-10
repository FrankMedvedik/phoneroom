﻿using System;
using System.ComponentModel.DataAnnotations;
 
namespace PhoneLogic.Model
{
    public class Call
    {
        [Display(Name = "Phone Room")]
        public String PhoneRoom { get; set; }
        [Display(Name = "Recruiter")]
        public String DisplayName { get; set; }
        [Display(Name = "Email")]
        public String EmailAddress { get; set; }
        [Display(Name = "Title")]
        public String Description { get; set; }
        [Display(Name = "StartLogId")]
        public Nullable<int> StartLogId { get; set; }
        public string JobNumber { get; set; }
        [Display(Name = "Caller ID")]
        public string CallerId { get; set; }
        [Display(Name = "CallId")]
        public string CallId { get; set; }
        [Display(Name = "Start")]
        public Nullable<System.DateTime> CallStartTime { get; set; }
        [Display(Name = "RecruiterConnectTime")]
        public Nullable<System.DateTime> RecruiterConnectTime { get; set; }
        [Display(Name = "SIP")]
        public string RecruiterSIP { get; set; }
        [Display(Name = "End")]
        public Nullable<System.DateTime> CallEndTime { get; set; }
        [Display(Name = "TollFreeNumber")]
        public string TollFreeNumber { get; set; }
        [Display(Name = "Direction")]
        public string CallDirection { get; set; }
        [Display(Name = "Status")]
        public string CallEndStatus { get; set; }
        [Display(Name = "Duration")]
        public Nullable<int> CallDuration { get; set; }

        [Display(Name = "Length")]
        public string CallDurationFormatted
        {
            get
            {
                return StringFormatSvc.DurationFormatted(CallDuration.GetValueOrDefault(0));
            }
        }
        [Display(Name = "Job")]
        public string JobFormatted
        {
            get
            {
                return StringFormatSvc.JobFormatted( JobNumber);
            }
        }

        [Display(Name = "Toll Free #")]
        public string TollFreeNumberFormatted
        {
            get
            {
                return StringFormatSvc.PhoneNumberFormatted(TollFreeNumber);
            }
        }
        [Display(Name = "Phone #")]
        public string PhoneFormatted
        {
            get
            {
                return StringFormatSvc.PhoneNumberFormatted(CallerId);
            }
        }
    }
}

