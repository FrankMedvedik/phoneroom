using System;
using System.Windows.Data;

namespace MM.Toolkit
{
    public class PopupShowingImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            String retVal = "/MM.Toolkit;component/Resources/Arrow.Bullet.Down.32x32.png";

            if (value != null)
            {
                Boolean vis = (Boolean)value;

                if (vis)
                    retVal = "/MM.Toolkit;component/Resources/Arrow.Bullet.Up.32x32.png";
            }

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
