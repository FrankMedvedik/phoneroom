using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using PhoneLogic.Core.Services;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Core.Areas.DialHistory
{
    public partial class DialPadView : UserControl
    {
        // private string _phoneNumber;

        public DialPadView()
        {
            InitializeComponent();
            DataContext = this;
            ActionName = "Find";
        }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
            ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }

        public string ActionName
        {
            get { return (string) btnGo.Content; }
            set { btnGo.Content = value; }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionNameProperty =
            DependencyProperty.Register("ActionName", typeof(string), typeof(DialPadView),
                new PropertyMetadata("SetActionName!"));

        private void key_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button) e.OriginalSource;
            string s = btn.Content.ToString();
            if (s == ActionName)
            {
                if (ValidateCallerId())
                    Messenger.Default.Send(new NotificationMessage<string>(this, TbPhoneNumber.Text,
                        Notifications.PhoneNumberChanged));
            }
            else
            {
                switch (s)
                {
                    case "Clr":
                        TbPhoneNumber.Text = "";
                        tbErrors.Text = "";
                        break;
                    case "Backspace":
                        if (!String.IsNullOrEmpty(TbPhoneNumber.Text) && TbPhoneNumber.Text.Length > 0)
                            TbPhoneNumber.Text = TbPhoneNumber.Text.Remove(TbPhoneNumber.Text.Length - 1);
                        break;
                    default:
                        TbPhoneNumber.Text += s;
                        break;
                }
            }
        }

        private bool ValidateCallerId()
        {
            tbErrors.Text = "";

            if (String.IsNullOrWhiteSpace(TbPhoneNumber.Text))
                tbErrors.Text = "Phone number is required";
            if ((PhoneNumberSvc.GetNumbers(TbPhoneNumber.Text).Length != 10))
                tbErrors.Text = "Phone number invalid";

            return (tbErrors.Text == "");

        }



    }
}