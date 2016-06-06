using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MM.Toolkit
{
    public class DateTimeFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            String retVal = null;
            DateTimeFormatHolder holder = null;

            holder = value as DateTimeFormatHolder;
            if (holder != null)
                retVal = holder.DateTime.ToString(holder.Format);

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
