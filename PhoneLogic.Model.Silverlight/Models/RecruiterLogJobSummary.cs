using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneLogic.Model
{
    public class RecruiterLogJobSummary
    {
        public string JobNumber { get; set; }
        public TimeSpan recruiterCallDuration { get; set; }
        public TimeSpan totalCallDuration { get; set; }

        [Display(Name = "Job")]
        public string jobFormatted
        {
            get
            {
                return (String.IsNullOrWhiteSpace(JobNumber))
                    ? JobNumber
                    : JobNumber.Substring(0, 4) + "-" + JobNumber.Substring(4, 4);
            }
        }

        [Display(Name = "Call Time")]
        public String recruiterCallDurationFormatted
        {
            get { return recruiterCallDuration.ToReadableString(); }
        }

        [Display(Name = "Total Time")]
        public String totalCallDurationFormatted
        {
            get { return totalCallDuration.ToReadableString(); }
        }
    }

    public static class TimeSpanExtension
        {
            public static string ToReadableAgeString(this TimeSpan span)
            {
                return string.Format("{0:0}", span.Days / 365.25);
            }

            public static string ToReadableString(this TimeSpan span)
            {
                string formatted = string.Format("{0}{1}{2}{3}",
                    span.Duration().Days > 0
                        ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? String.Empty : "s")
                        : string.Empty,
                    span.Duration().Hours > 0
                        ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? String.Empty : "s")
                        : string.Empty,
                    span.Duration().Minutes > 0
                        ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? String.Empty : "s")
                        : string.Empty,
                    span.Duration().Seconds > 0
                        ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? String.Empty : "s")
                        : string.Empty);

                if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

                if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

                return formatted;
            }
        }
    
}