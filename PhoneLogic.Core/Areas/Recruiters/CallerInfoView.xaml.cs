using System;
using System.Windows;
using System.Windows.Controls;
using PhoneLogic.Core.ViewModels;

namespace PhoneLogic.Core.Areas.Recruiters
{
    public partial class CallerInfoView : UserControl
    {
        public CallerInfoView()
        {

            InitializeComponent();
            if(ConversationContext.Instance.PhoneLogicContext.callerId != null)
            { 
                txtCallerId.Text = ConversationContext.Instance.PhoneLogicContext.callerId;
            }
        }


        public String CallerId
        {
            get { return (String)GetValue(CallerIdProperty); }
            set
            {
                SetValue(CallerIdProperty, value);
                txtCallerId.Text =  String.Format("{0:(###) ###-####}", double.Parse(value));
                dhv.GetCalls(value);
            }
        }

        // Using a DependencyProperty as the backing store for CallerId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CallerIdProperty =
            DependencyProperty.Register("CallerId", typeof(String), typeof(CallerInfoView), new PropertyMetadata("Phone number goes here"));

        
        
    }
}
