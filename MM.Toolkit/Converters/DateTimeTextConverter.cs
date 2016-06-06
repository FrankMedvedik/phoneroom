using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;

namespace MM.Toolkit
{
    public class DateTimeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            String param = "yyyy.MM.dd HH:mm";
            DateTime? datetime = null;

            if ((parameter != null) && (parameter is String))
                param = parameter.ToString();

            if (value is DateTime)
                datetime = (DateTime)value;
            else if (value is DateTime?)
                datetime = (DateTime?)value;

            if (datetime.HasValue)
                return datetime.Value.ToString(param);
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime retVal = DateTime.MinValue;
            String datetimestr = String.Empty;

            if (value != null)
            {
                datetimestr = value.ToString();

                try
                {
                    retVal = DateTime.Parse(datetimestr);
                }
                catch { }
            }

            return retVal;
        }
    }
}
