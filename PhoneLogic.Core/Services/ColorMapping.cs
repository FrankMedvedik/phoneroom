﻿namespace PhoneLogic.Core.Services
{
    public class ColorMapping
    {
        public static string GetForeground(int cnt)
        {
            const string noActivity = "Black";
            const string low = "Black";
            const string medium = "Grey";
            const string high = "White";

            if (cnt == 0)
                return noActivity;
            if (cnt == 1)
                return low;
            if (cnt > 1 && cnt < 5)
                return medium;
            if (cnt > 5)
                return high;
            return low;
        }

        public static string GetBackground(int cnt)
        {
            const string noActivity = "White";
            const string low = "Yellow";
            const string medium = "Orange";
            const string high = "Red";

            if (cnt == 0)
                return noActivity;
            if (cnt == 1)
                return low;
            if (cnt > 1 && cnt < 5)
                return medium;
            if (cnt > 5)
                return high;
            return low;
        }
    }
}
