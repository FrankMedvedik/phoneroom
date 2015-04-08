using System;
using System.Windows;
using System.Windows.Browser;
using Microsoft.Lync.Model;
using PhoneLogic.Core.Services;
using PhoneLogic.Core.ViewModels;
using PhoneLogic.Model;

namespace PhoneLogic.Inbound
{
    public interface ICloseHandler
    {
        string Message { get; set; }
        void Initialize();
    }
    public class CloseHandler : ICloseHandler
    {
        private readonly ICloseHandler _closeHandler;

        public CloseHandler()
        {
            if (!Application.Current.IsRunningOutOfBrowser)
                _closeHandler = new InBrowserCloseHandler();
            else
                _closeHandler = new OutOfBrowserCloseHandler();
        }

        #region ICloseHandler Members

        public string Message
        {
            get { return _closeHandler.Message; }
            set { _closeHandler.Message = value; }
        }

        public void Initialize()
        {
            _closeHandler.Initialize();
        }

        #endregion
    }



    public class InBrowserCloseHandler : ICloseHandler
    {
        private const string ScriptableObjectName = "InBrowserCloseHandler";

        #region ICloseHandler Members

        public void Initialize()
        {
            HtmlPage.RegisterScriptableObject(ScriptableObjectName, this);
            string pluginName = HtmlPage.Plugin.Parent.Id;

            HtmlPage.Window.Eval(string.Format(
                @"window.onbeforeunload = function () {{
            var slApp = document.getElementById('{0}').getElementsByTagName('object')[0];
            var result = slApp.Content.{1}.OnBeforeUnload();
            if(result.length > 0)
                return result;
            }}",
                pluginName, ScriptableObjectName)
                );
        }

        public string Message { get; set; }

        #endregion

        [ScriptableMember]
        public string OnBeforeUnload()
        {
            if (ConversationContext.Instance.PhoneLogicContext.callbackId > 0)
            {
                var cb = new CallbackDto()
                {   callbackID = ConversationContext.Instance.PhoneLogicContext.callbackId,
                    SIP = LyncClient.GetClient().Self.Contact.Uri
                };

                if (ConversationContext.Instance.KeepCallback)
                {
                    // MessageBox.Show("Keep Callback = " + ConversationContext.Instance.KeepCallback);
                     CallbackSvc.EndCall(cb);
                }
                else
                {
                    MessageBox.Show("Delete Callback = " + ConversationContext.Instance.KeepCallback);
                    CallbackSvc.Close(cb);
                }

            }
            return Message;
        }
    }
    public class OutOfBrowserCloseHandler : ICloseHandler
    {
        #region ICloseHandler Members

        public async void Initialize()
        {  
            Application.Current.MainWindow.Closing +=
                (s, e) =>
                {
                    if (ConversationContext.Instance.PhoneLogicContext.callbackId != 0)
                    {
                        var cb = new CallbackDto()
                        {
                            callbackID = ConversationContext.Instance.PhoneLogicContext.callbackId,
                            SIP = LyncClient.GetClient().Self.Contact.Uri
                        };
                        MessageBoxResult boxResult = MessageBox.Show(
                            string.Format(
                                @"Are you sure you want to close the application?{1}{1}{0}",
                                Message, Environment.NewLine),
                            string.Empty,
                            MessageBoxButton.OKCancel);
                        if (boxResult == MessageBoxResult.Cancel)
                        {
                            e.Cancel = true;
                            MessageBox.Show("end call");
                            //await CallbackSvc.EndCall(cb);
                        }
                        else
                            MessageBox.Show("delete and end call");
                            //await CallbackSvc.Close(cb);
                    }
                };
        }

        public string Message { get; set; }

        #endregion
    }
}
