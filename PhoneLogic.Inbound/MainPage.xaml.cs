using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using Newtonsoft.Json;
using PhoneLogic.Core;
using PhoneLogic.Core.ViewModels;
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
                    string appData = conversation.GetApplicationData(CallContextApp.RecknerCallAppGuid);
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
                        //MessageBox.Show(ConversationContext.Instance.PhoneLogicContext.jobNumber + " " + 
                        //    ConversationContext.Instance.PhoneLogicContext.TaskID);

                        //MessageBox.Show(String.Format("Job {0}  Task {1}",
                        //    ConversationContext.Instance.PhoneLogicContext.jobNumber,
                        //    ConversationContext.Instance.PhoneLogicContext.TaskID));
                    
                    }
                        // a callback comes from a message if it is not a call back you can't close the callback.
                        ConversationContext.Instance.ShowCallbackButtons =
                            ConversationContext.Instance.PhoneLogicContext.callbackId != 0;
                    }
            }
            catch (Exception e)
            {
                MessageBox.Show("JobDetailViewConstructor Exception: " + e.Message);
            }
            finally
            {
                InitializeComponent();
            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConversationContext.Instance.ShowJobDetailView = !ConversationContext.Instance.ShowJobDetailView;
            MessageBox.Show(ConversationContext.Instance.ShowJobDetailView.ToString());
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            ConversationContext.Instance.ShowCallbackButtons = !ConversationContext.Instance.ShowCallbackButtons;
            MessageBox.Show(ConversationContext.Instance.ShowCallbackButtons.ToString()
                );
        }
    }
}