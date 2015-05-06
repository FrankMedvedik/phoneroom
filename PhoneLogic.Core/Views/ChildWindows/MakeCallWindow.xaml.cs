using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Extensibility;
using PhoneLogic.Model;

namespace PhoneLogic.Core.Views
{
    public partial class MakeCallWindow 
    {
        public MakeCallWindow()
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

        public String AppData
        {
            get { return (String)GetValue(AppDataProperty); }
            set
            {
                SetValue(AppDataProperty, value);
                RaisePropertyChanged("AppData");
            }
        }

        public DependencyProperty AppDataProperty =
            DependencyProperty.Register("AppData", typeof(String), typeof(MakeCallWindow), new PropertyMetadata(""));


        private async void Call_Click(object sender, RoutedEventArgs e)
        {
            //   string OutboundNumber = ((Button)sender).CommandParameter.ToString();
            //DataGridRowEventArgs a = (DataGridRowEventArgs) e;

            if (String.IsNullOrWhiteSpace(tbOutboundPhone.Text))
            {
                MessageBox.Show("Please enter a phone number first");
                return;
            }
            if (LyncClient.GetClient().State != ClientState.SignedIn)
            {
                MessageBox.Show("Please Sign in to Lync first ");
                return;
            }


            /* setup making the call  */
            var participantUri = new List<string> { tbOutboundPhone.Text };
            var modalitySettings = new Dictionary<AutomationModalitySettings, object>
            {
                {AutomationModalitySettings.ApplicationId, ConditionalConfiguration.RecknerCallAppGuid},
                {AutomationModalitySettings.Subject,tbOutboundPhone.Text },
                {AutomationModalitySettings.ApplicationData, AppData}
            };

            ///* start of call */
            //await CallbackSvc.StartCall(new CallbackDto
            //{
            //    SIP = LyncClient.GetClient().Self.Contact.Uri,
            //    callbackID = _vm.SelectedMyCallback.callbackID
            //});


            //await PhoneCallSvc.LogPhoneCall(new PhoneCall()
            //{
            //    //callbackID = _vm.SelectedMyCallback.callbackID,
            //    jobNum = _vm.SelectedPhoneLogicTask.JobNum,
            //    //taskID = _vm.SelectedPhoneLogicTask.JobNum,
            //    phoneNum = _vm.PhoneNumber,
            //    SIP = LyncClient.GetClient().Self.Contact.Uri
            //});

            LyncClient.GetAutomation().BeginStartConversation(
                AutomationModalities.Audio,
                participantUri,
                modalitySettings, ar =>
                {
                    try
                    {
                        ConversationWindow newWindow = LyncClient.GetAutomation().EndStartConversation(ar);
                        newWindow.BeginOpenExtensibilityWindow(
                            ConditionalConfiguration.RecknerCallAppGuid,
                            newWindow.EndOpenExtensibilityWindow,
                            null);
                    }
                    catch (Exception oe)
                    {
                        MessageBox.Show("Can't Open Output Windows: Operation exception on start conversation " +
                                        oe.Message);
                    }
                },
                null);
        }

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

