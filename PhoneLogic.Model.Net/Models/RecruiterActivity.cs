using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PhoneLogic.Model
{
#if !SILVERLIGHT
    [Serializable]
#endif
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

        [Display(Name = "SIP")]
        public string RecruiterSIP { get; set; }

        [Display(Name = "Start")]
        public string StartTimeFormatted
        {
            get { return StringFormatSvc.TimeFormatted(StartTime); }
        }

        [Display(Name = "End")]
        public string EndTimeFormatted
        {
            get { return StringFormatSvc.TimeFormatted(EndTime); }
        }

        public System.DateTime StartTime { get; set; }

        public System.DateTime EndTime { get; set; }


        [Display(Name = "ActivityName")]
        public string ActivityName { get; set; }

        [Display(Name = "ActivityType")]
        public string ActivityType { get; set; }

        public TimeSpan Duration { get; set; }

        [Display(Name = "Duration")]
        public string DurationFormatted
        {
            get { return StringFormatSvc.DurationFormatted(Duration); }
        }

        //public class TestRecruiterActivitiesData
            //{
            //    public List<RecruiterActivity> Activities;
            //    public TestRecruiterActivitiesData()
            //    {
            //        var a = new RecruiterActivity()
            //        {
            //             ActivityName = 
            //        }
            //    }

            //}

            //public override string ToString()
            //{
            //    return RecruiterSIP + "." + ActivityName + "." + ActivityType + "." + StartTime + "." + EndTime;
            //}
        }
    }
