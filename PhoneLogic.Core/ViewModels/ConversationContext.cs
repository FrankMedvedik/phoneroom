using System;
using PhoneLogic.Model;

namespace PhoneLogic.Core.ViewModels
{
    public sealed class ConversationContext : ViewModelBase
    {
        private static volatile ConversationContext instance;
        private static object syncRoot = new Object();
        private ConversationContext() { }

        public static ConversationContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ConversationContext();
                    }
                }

                return instance;
            }
        }

        private Boolean _IsCWE;
        public Boolean IsCWE
        {
            get { return (instance._IsCWE); }
            set
            {
                instance._IsCWE = value;
                NotifyPropertyChanged();
            }
        }

        private Boolean _ShowJobDetailView;
        public Boolean ShowJobDetailView
        {
            get { return (instance._ShowJobDetailView); }
            set
            {
                instance._ShowJobDetailView = value;
                NotifyPropertyChanged();
            }
        }
        private Boolean _keepCallback;
        public Boolean KeepCallback
        {
            get { return (instance._keepCallback); }
            set
            {
                instance._keepCallback = value;
                NotifyPropertyChanged();
            }
        }

        private Boolean _ShowCallbackButtons;
        public Boolean ShowCallbackButtons
        {
            get { return (instance._ShowCallbackButtons); }
            set
            {
                instance._ShowCallbackButtons = value;
                NotifyPropertyChanged();
            }
        }
        private PhoneLogicContext _PhoneLogicContext = new PhoneLogicContext()
        {
            callerId = "",
            callbackId = -1,
            conversationId = "",
            dialedNumber = "",
            jobNumber = "",
            taskId = -1,
            timeReceived = null
        };

        public PhoneLogicContext PhoneLogicContext
        {
            get { return instance._PhoneLogicContext; }
            set
            {
                instance._PhoneLogicContext = value;
                NotifyPropertyChanged();
            }
        }

    }
}