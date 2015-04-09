using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using Microsoft.Lync.Model.Extensibility;
using Newtonsoft.Json;
using PhoneLogic.Core;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Core.Views;
using PhoneLogic.Model;

namespace PhoneLogic.Inbound
{
    public partial class MainPage : UserControl
    {
        private Conversation _myConversation = null;

        public MainPage()
        {
            InitializeComponent();
            timer.Visibility = Visibility.Collapsed;

            DataContext = ConversationContext.Instance;
            InitializeCallInformation();
        }

        public void InitializeCallInformation()
        {
            try
            {
                LyncClient lyncClient = LyncClient.GetClient();
                if (lyncClient == null)
                {
                    MessageBox.Show("Sign into Lync please");
                    return;
                }


                _myConversation = LyncClient.GetHostingConversation();
                if (_myConversation == null)
                {
                    // we are doing just a lookup page
                    JobDetail.Visibility = Visibility.Collapsed;
                }
                else
                {   // need to check this to handle outbound calls that are already ACTIVE
                    if (_myConversation.State == ConversationState.Active)
                    {
                        timer._vm.Start();
                        timer.Visibility = Visibility.Visible;
                    }
                    _myConversation.StateChanged += conversation_StateChanged;
                    SetupJob();
                }
            }

            catch(Exception e)
            {
                MessageBox.Show(e.Message + (e.InnerException != null ? e.InnerException.Message : "") 
                    + ConversationContext.Instance.PhoneLogicContext.ToString());
            }
        }

        private void SetupJob()
        {
            string appData = _myConversation.GetApplicationData(ConditionalConfiguration.RecknerCallAppGuid);
            // MessageBox.Show(appData);

            ConversationContext.Instance.PhoneLogicContext =
                JsonConvert.DeserializeObject<PhoneLogicContext>(appData);
            if (ConversationContext.Instance.PhoneLogicContext.jobNumber.Length > 8)
            {
                // get task id before we chop up the job number
                ConversationContext.Instance.PhoneLogicContext.taskId =
                    Convert.ToInt32(ConversationContext.Instance.PhoneLogicContext.jobNumber.Substring(9));
                ConversationContext.Instance.PhoneLogicContext.jobNumber =
                    ConversationContext.Instance.PhoneLogicContext.jobNumber.Substring(0, 8);
            }
            JobDetail.ViewModel.GetPhoneLogicTask(ConversationContext.Instance.PhoneLogicContext.jobNumber,
                ConversationContext.Instance.PhoneLogicContext.TaskID);
            JobDetail.Visibility = Visibility.Visible;
        }


        private void conversation_StateChanged(object sender, ConversationStateChangedEventArgs e)
    {
        //MessageBox.Show("Conversation State:" + e.NewState.ToString());

        if (e.NewState == ConversationState.Active)
        {
            timer._vm.Start();
            timer.Visibility = Visibility.Visible;
        }
        else if(e.NewState == ConversationState.Inactive)
        {
            timer._vm.Stop();
            ShutdownThisConversation();
        }
    }

        private void ShutdownThisConversation()
        {

            var cw = new CallCloseWindow()
            {
                IsCallback = (ConversationContext.Instance.PhoneLogicContext.callbackId > 0),
                KeepCallback = false,
                CallDuration = timer._vm.TimeFromStart
            };
//           MessageBox.Show(String.Format(" Is Callback? {0}| Keep Callback? {1}|CallDuration {2}", cw.IsCallback, cw.KeepCallback, cw.CallDuration));
            cw.Closed += CallCloseWindow_Closed;
            cw.Show();
        }

        private async void CallCloseWindow_Closed(object sender, EventArgs e)
            {
                var c = (CallCloseWindow)sender;
                c.Closed -= CallCloseWindow_Closed;
                //MessageBox.Show(String.Format(" Is Callback? {0}| Keep Callback? {1}|CallDuration {2}",
                //    c.IsCallback, c.KeepCallback, c.CallDuration));
                //if (c.IsCallback)
                {
                    var cb = new CallbackDto()
                    {
                        callbackID = ConversationContext.Instance.PhoneLogicContext.callbackId,
                        SIP = LyncClient.GetClient().Self.Contact.Uri
                    };
                    if (c.KeepCallback)
                        await CallbackSvc.EndCall(cb);
                    else
                        await CallbackSvc.Close(cb);
                }
                var myCW = LyncClient.GetAutomation().GetConversationWindow(_myConversation);
                _myConversation.End();
                myCW.Close();
            }

        }
    }


