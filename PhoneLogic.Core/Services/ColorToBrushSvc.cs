using System.Windows.Media;

namespace PhoneLogic.Core.Services
{
    public class ColorToBrushSvc
    {
        public static Brush GetForeground(int? cnt)
        {
            var noActivity = new SolidColorBrush(Colors.Black);
            var low = new SolidColorBrush(Colors.Black);
            var medium = new SolidColorBrush(Colors.Black);
            var high = new SolidColorBrush(Colors.Black);

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
            var noActivity = new SolidColorBrush(Colors.White);
            var low = new SolidColorBrush(Colors.Yellow);
            var medium = new SolidColorBrush(Colors.Orange);
            var high = new SolidColorBrush(Colors.Red);

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