using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PhoneLogic.Core.Services
{
    public class ColorToBrushSvc
    {
        public static Brush GetForeground(int? cnt)
        {
            SolidColorBrush noActivity  = new SolidColorBrush( Colors.Black);
            SolidColorBrush low = new SolidColorBrush( Colors.Black);
            SolidColorBrush medium = new SolidColorBrush(Colors.Black);
            SolidColorBrush high = new SolidColorBrush(Colors.White);

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

        public static Brush GetBackground(int? cnt)
        {
            SolidColorBrush noActivity = new SolidColorBrush(Colors.White);
            SolidColorBrush low = new SolidColorBrush(Colors.Yellow);
            SolidColorBrush medium = new SolidColorBrush(Colors.Orange);
            SolidColorBrush high = new SolidColorBrush(Colors.Red);

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
