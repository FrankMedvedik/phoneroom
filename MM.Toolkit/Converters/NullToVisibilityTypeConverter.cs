using System;
using System.Windows;
using System.Windows.Data;

namespace MM.Toolkit
{
    public class NullToVisibilityTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility retVal = Visibility.Visible;
            String param = String.Empty;

            if (parameter != null)
                param = parameter.ToString();

            if (!String.IsNullOrEmpty(param))
            {
                if (value == null)
                    retVal = Visibility.Visible;
                else
                    retVal = Visibility.Collapsed;
            }
            else
            {
                if (value == null)
                    retVal = Visibility.Collapsed;
                else
                    retVal = Visibility.Visible;
            }

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
