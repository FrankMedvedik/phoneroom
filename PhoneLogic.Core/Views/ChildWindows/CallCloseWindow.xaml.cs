using System;
using System.ComponentModel;
using System.Windows;

namespace PhoneLogic.Core.Views
{
    public partial class CallCloseWindow
    {
        public CallCloseWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public TimeSpan CallDuration
        {
            get { return (TimeSpan) GetValue(CallDurationProperty); }
            set
            {
                SetValue(CallDurationProperty, value);
                RaisePropertyChanged("CallDuration");
            }
        }

        public DependencyProperty CallDurationProperty =
            DependencyProperty.Register("CallDuration", typeof(TimeSpan), typeof(CallCloseWindow),
                new PropertyMetadata(new TimeSpan()));


        public Boolean KeepCallback
        {
            get { return (Boolean) GetValue(KeepCallbackProperty); }
            set
            {
                SetValue(KeepCallbackProperty, value);
                RaisePropertyChanged("KeepCallback");
            }
        }

        public static readonly DependencyProperty KeepCallbackProperty =
            DependencyProperty.Register("KeepCallback", typeof(Boolean), typeof(CallCloseWindow),
                new PropertyMetadata(false));


        public Boolean IsCallback
        {
            get { return (Boolean) GetValue(IsCallbackProperty); }
            set
            {
                SetValue(IsCallbackProperty, value);
                CallbackStackPanel.Visibility = IsCallback ? Visibility.Visible : Visibility.Collapsed;
                RaisePropertyChanged("IsCallback");
            }
        }

        public static readonly DependencyProperty IsCallbackProperty =
            DependencyProperty.Register("IsCallback", typeof(Boolean), typeof(CallCloseWindow),
                new PropertyMetadata(false));


        //public string NoticeText
        //{
        //    set
        //    {
        //        TextBlock_Notice.Text = value;
        //    }
        //}

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}