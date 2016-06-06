using System;
using System.Windows;
using System.Windows.Data;

namespace MM.Toolkit
{
    public class VisibilityToBooleanTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility vis = Visibility.Collapsed;
            String param = null;

            if ((value != null) && (value is Visibility))
                vis = (Visibility)value;

            if ((parameter != null) && (parameter is String))
                param = (String)value;

            if (String.IsNullOrEmpty(param))
                return (vis != Visibility.Visible);
            else
                return (vis == Visibility.Visible);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Boolean val = true;
            String param = null;

            if ((value != null) && (value is Boolean))
                val = (Boolean)value;

            if ((parameter != null) && (parameter is String))
                param = (String)parameter;

            if (!String.IsNullOrEmpty(param))
                val = !val;

            if (val)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }
    }
}
