using System;
using System.Globalization;
using System.Windows.Data;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Converters
{

    public class IntToForegroundColorConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ColorMapping.GetForeground((int) value));
        }

        public object ConvertBack(object value, Type targetType,
               object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
