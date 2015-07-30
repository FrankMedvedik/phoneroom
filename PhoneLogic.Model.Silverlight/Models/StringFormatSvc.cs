using System;

namespace PhoneLogic.Model 
{
    public static class StringFormatSvc
    {

        public static string DurationFormatted(string duration)
        {
            int n;
            if (int.TryParse("duration", out n))
                return string.Format("{0:00}:{1:00}:{2:00}", n/3600, (n/60)%60, n%60);
            return duration;
        }

        public static  string DurationFormatted(int duration)
        {
                return string.Format("{0:00}:{1:00}:{2:00}", duration / 3600, (duration / 60) % 60, duration % 60);
        }
        public static string JobFormatted(string jobNumber)
        {
            string retval;
            retval = (String.IsNullOrWhiteSpace(jobNumber)) ? jobNumber : jobNumber.Substring(0, 4) + "-" + jobNumber.Substring(4, 4);
            return retval;
        }

        public static string PhoneNumberFormatted(string phoneNumber)
        {
                string retval;
                int n;
                if(int.TryParse("phoneNumber", out n))
                    retval = (String.IsNullOrWhiteSpace(phoneNumber)) ? phoneNumber : String.Format("{0:(###) ###-####}", double.Parse(phoneNumber));
                else
                    retval = phoneNumber;
              return retval;
        }
        public static int TaskFromJobNumber(string JobNumber)
        {
            throw new NotImplementedException();
            string retval;
            int n;

            retval = JobNumber;
            return n;
        }
    }

}
