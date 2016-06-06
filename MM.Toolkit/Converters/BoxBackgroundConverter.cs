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
    public class BoxBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
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
                return "#FFFFFFFF";
            else
                return "#FFEBEBEB";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
