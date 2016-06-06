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
using System.Windows.Data;

namespace MM.Toolkit
{
    public class GridLengthTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Double tmpDouble = default(Double);
            GridLength tmpGridLength = default(GridLength);

            if (value is Double)
            {
                if ((value != null) && Double.TryParse(value.ToString(), out tmpDouble))
                    return new GridLength(tmpDouble, GridUnitType.Pixel);
                else
                    return new GridLength();
            }
            else if (value is GridLength)
            {
                if (value != null)
                    tmpGridLength = (GridLength)value;

                return tmpGridLength.Value;
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
