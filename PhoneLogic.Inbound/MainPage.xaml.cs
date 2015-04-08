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
        public MainPage()
        {
            DataContext = ConversationContext.Instance;
            try
            {
                LyncClient lyncClient = LyncClient.GetClient();
                if (lyncClient == null)
                {
                    MessageBox.Show("Sign into Lync please");
                    return;
                }


                Conversation conversation = LyncClient.GetHostingConversation();
                if (conversation == null)
                {
                    // we are doing just a lookup page
                    ConversationContext.Instance.ShowJobDetailView = false;
                }

                else
                {
                    string appData = conversation.GetApplicationData(ConditionalConfiguration.RecknerCallAppGuid);
                    // no app data means we did not come from either an inbound call or a callback message 
                    if (appData == null)
                    {
                        ConversationContext.Instance.ShowJobDetailView = false;
                    }
                    else
                    {
                        //   MessageBox.Show(appData);
                        // we have a call and it has app data meaning we should know the job info
                        ConversationContext.Instance.PhoneLogicContext =
                            JsonConvert.DeserializeObject<PhoneLogicContext>(appData);
                        if (ConversationContext.Instance.PhoneLogicContext.jobNumber.Length > 8)
                        {
                            //MessageBox.Show(ConversationContext.Instance.PhoneLogicContext.jobNumber.Substring(0, 8) + " " 
                            //    + ConversationContext.Instance.PhoneLogicContext.jobNumber.Substring(9));
                            ConversationContext.Instance.PhoneLogicContext.taskId =
                                Convert.ToInt32(ConversationContext.Instance.PhoneLogicContext.jobNumber.Substring(9));
                            ConversationContext.Instance.PhoneLogicContext.jobNumber =
                                ConversationContext.Instance.PhoneLogicContext.jobNumber.Substring(0, 8);
                        }

                        ConversationContext.Instance.ShowJobDetailView = true;
                        conversation.StateChanged += conversation_StateChanged;
                        
                        #region debug
                        //MessageBox.Show(ConversationContext.Instance.PhoneLogicContext.jobNumber + " " + 
                        //    ConversationContext.Instance.PhoneLogicContext.TaskID);

                        //MessageBox.Show(String.Format("Job {0}  Task {1}",
                        //    ConversationContext.Instance.PhoneLogicContext.jobNumber,
                        //    ConversationContext.Instance.PhoneLogicContext.TaskID));

                        // a callback comes from a message if it is not a call back you can't close the callback.

                        //if (ConversationContext.Instance.PhoneLogicContext.callbackId > 0)
                        //{
                        //    ConversationContext.Instance.ShowCallbackButtons = true;
                        //    ConversationContext.Instance.KeepCallback = fa;
                        //}
                        #endregion
                    }
                }
            }

            catch
                (Exception e)
            {
                MessageBox.Show(e.Message +
                                (e.InnerException != null ? e.InnerException.Message : " - no inner exception"));
            }
            finally
            {
                InitializeComponent();
                //ConversationContext.Instance.PhoneLogicContext.callbackId = 312400;
                //MessageBox.Show("test Callback id =" + ConversationContext.Instance.PhoneLogicContext.callbackId);
            }
        }

        
    private void conversation_StateChanged(object sender, ConversationStateChangedEventArgs e)
    {
        //MessageBox.Show("Conversation State:" + e.NewState.ToString());

        if (e.NewState == ConversationState.Inactive)
        {

            var waitingWindow = new WaitingWindow();
            waitingWindow.Closed += new EventHandler(waitingWindow_Closed);
            waitingWindow.Show();
        }
    }
        private async void waitingWindow_Closed(object sender, EventArgs e)
            {
                WaitingWindow ww = (WaitingWindow)sender;
                    var cb = new CallbackDto()
                    {
                        callbackID = ConversationContext.Instance.PhoneLogicContext.callbackId,
                        SIP = LyncClient.GetClient().Self.Contact.Uri
                    };
                    if (ConversationContext.Instance.KeepCallback)
                        await CallbackSvc.EndCall(cb);
                    else
                        await CallbackSvc.Close(cb);
                    var conversation = (Conversation) LyncClient.GetHostingConversation();
                    var myAutomation = Microsoft.Lync.Model.LyncClient.GetAutomation();
                    conversation.End();
                    ConversationWindow cw = myAutomation.GetConversationWindow(conversation);
                    cw.Close();
            }
        

        private void test()
        {
            ConversationContext.Instance.ShowCallbackButtons = false;
            ConversationContext.Instance.ShowJobDetailView = true;

            ConversationContext.Instance.PhoneLogicContext =
                new PhoneLogicContext
                {
                    callerId = "",
                    callbackId = -1,
                    conversationId = "",
                    dialedNumber = "",
                    jobNumber = "20080716",
                    timeReceived = null,
                    taskId = 1
                };
        }
        }
    }


