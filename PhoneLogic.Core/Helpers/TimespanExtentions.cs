﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneLogic.Core.Helpers
{
    public static class TimespanExtentions
    {
        public static TimeSpan TotalTime(this IEnumerable<TimeSpan> theCollection)
        {
            int i = 0;
            int totalSeconds = 0;

            var arrayDuration = theCollection.ToArray();

            for (i = 0; i < arrayDuration.Length; i++)
            {
                totalSeconds = (int)(arrayDuration[i].TotalSeconds) + totalSeconds;
            }

            return TimeSpan.FromSeconds(totalSeconds);
        }
        
        public static string ToReadableAgeString(this TimeSpan span)
        {
            return string.Format("{0:0}", span.Days / 365.25);
        }

        public static string ToReadableString(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? String.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }
    }
}
