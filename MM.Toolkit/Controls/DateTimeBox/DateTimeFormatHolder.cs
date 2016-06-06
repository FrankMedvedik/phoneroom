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
using System.ComponentModel;

namespace MM.Toolkit
{
    public class DateTimeFormatHolder : DependencyObject, INotifyPropertyChanged
    {
        #region -: Properties :-
        
        public DateTime DateTime
        {
            get { return (DateTime)GetValue(DateTimeProperty); }
            set
            {
                SetValue(DateTimeProperty, value);
                NotifyPropertyChanged("DateTime");
            }
        }

        public String Format
        {
            get { return (String)GetValue(FormatProperty); }
            set
            {
                SetValue(FormatProperty, value);
                NotifyPropertyChanged("Format");
            }
        }

        #endregion

        #region -: Dependency Properties :-

        public static readonly DependencyProperty DateTimeProperty = DependencyProperty.Register("DateTime", typeof(DateTime), typeof(DateTimeFormatHolder), new PropertyMetadata(default(DateTime)));
        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(String), typeof(DateTimeFormatHolder), new PropertyMetadata(null));
        
        #endregion

        #region -: INotifyPropertyChanged Members :-

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String aPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));
        }

        #endregion
    }
}
