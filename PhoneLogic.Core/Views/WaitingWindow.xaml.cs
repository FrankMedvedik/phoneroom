using System;
using System.ComponentModel;
using System.Windows;

namespace PhoneLogic.Core.Views
{
	public partial class WaitingWindow
	{
		public WaitingWindow()
		{
			InitializeComponent();
		}
        public event PropertyChangedEventHandler PropertyChanged;
	    private Boolean _keepCallback;

	    public Boolean KeepCallback
	    {
	        get { return _keepCallback; }
	        set
	        {
	            _keepCallback = value;
	            RaisePropertyChanged("KeepCallback");
	        }
	    }

	    private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        //public int CallDuration
        //{
        //    get { return (int)GetValue(CallDurationProperty); }
        //    set { SetValue(CallDurationProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for CallDuration.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CallDurationProperty =
        //    DependencyProperty.Register("CallDuration", typeof(int), typeof(ownerclass), new PropertyMetadata(0));

        
		public string NoticeText
		{
			set
			{
				TextBlock_Notice.Text = value;
			}
		}

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
	}
}

