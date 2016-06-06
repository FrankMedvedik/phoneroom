using System;
using System.Windows.Data;

namespace MM.Toolkit
{
    public class NullToBooleanTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Boolean retVal = true;
            String param = String.Empty;

            if (parameter != null)
                param = parameter.ToString();

            if (!String.IsNullOrEmpty(param))
                retVal = (value == null);
            else
                retVal = (value != null);

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
