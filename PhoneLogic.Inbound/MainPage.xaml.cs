﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using Newtonsoft.Json;
using PhoneLogic.Core;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Core.Views;
using PhoneLogic.Model;
using PhoneLogic.ViewContracts.MVVMMessenger;

namespace PhoneLogic.Inbound
{
    public partial class MainPage : UserControl
    {
        private Conversation _myConversation;


        public MainPage()
        {
            InitializeComponent();
            timer.Visibility = Visibility.Collapsed;

            DataContext = ConversationContext.Instance;
            InitializeCallInformation();
            
            Messenger.Default.Register<AppColors>
                (
                    this, SetColors
                );
        }


        private void SetColors(AppColors ac)
        {
            String xamlString = "<Canvas xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Background=\"Aqua\"/>";
            Canvas c = (Canvas)System.Windows.Markup.XamlReader.Load(xamlString);
            SolidColorBrush AquaBrush = (SolidColorBrush)c.Background;


            //LayoutRoot.Foreground = ac.TheForeground;
            if (ac.TheBackground != new SolidColorBrush(Colors.LightGray))
                LayoutRoot.Background = ac.TheBackground;
            else
                LayoutRoot.Background = AquaBrush;
            Main.Background = ac.TheBackground;
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
                MessageBox.Show("Error initializing CallInfo " + e.Message + (e.InnerException != null ? e.InnerException.Message : "No Inner Exception") 
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
                // get task id before we chop up the JobFormatted number
                ConversationContext.Instance.PhoneLogicContext.taskId =
                    Convert.ToInt32(ConversationContext.Instance.PhoneLogicContext.jobNumber.Substring(9));
                ConversationContext.Instance.PhoneLogicContext.jobNumber =
                    ConversationContext.Instance.PhoneLogicContext.jobNumber.Substring(0, 8);
            }
            JobDetail.ViewModel.GetPhoneLogicTask(ConversationContext.Instance.PhoneLogicContext.jobNumber,
                ConversationContext.Instance.PhoneLogicContext.TaskID);
            JobDetail.Visibility = Visibility.Visible;
            cinfoView.CallerId = ConversationContext.Instance.PhoneLogicContext.callerId;
            if(ConversationContext.Instance.PhoneLogicContext.callbackId != -1) UpdateCallBack(ConversationContext.Instance.PhoneLogicContext.callbackId);
        }

        public async void UpdateCallBack(int callbackId)
        {
            
            await CallbackSvc.StartCall(
                new CallbackDto
                {
                    callbackID = callbackId,
                    SIP = LyncClient.GetClient().Self.Contact.Uri,
                    status = "In Call",
                });
        }


        private void conversation_StateChanged(object sender, ConversationStateChangedEventArgs e)
        {

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
            if (ConversationContext.Instance.PhoneLogicContext.callbackId > 0)
            {

                var cw = new CallCloseWindow()
                {
                    IsCallback = (ConversationContext.Instance.PhoneLogicContext.callbackId > 0),
                    KeepCallback = false,
                    CallDuration = timer._vm.TimeFromStart
                };
                //  MessageBox.Show(String.Format(" Is Callback? {0}| Keep Callback? {1}|CallDuration {2}", cw.IsCallback, cw.KeepCallback, cw.CallDuration));
                cw.Closed += CallCloseWindow_Closed;
                cw.Show();
            }
            else
                EndCall();

        }

        private async void CallCloseWindow_Closed(object sender, EventArgs e)
            {
                var c = (CallCloseWindow)sender;
                c.Closed -= CallCloseWindow_Closed;
                if (c.IsCallback)
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
            EndCall();
            }

        private void EndCall()
        {
            var myCW = LyncClient.GetAutomation().GetConversationWindow(_myConversation);
            _myConversation.End();
            myCW.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShutdownThisConversation();
        }
        }
    }


