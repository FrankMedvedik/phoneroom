using System;
using PhoneLogic.Model;
using Silverlight.Base.MVVMBaseTypes;

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

        //private Boolean _ShowJobDetailView;
        //public Boolean ShowJobDetailView
        //{
        //    get { return (instance._ShowJobDetailView); }
        //    set
        //    {
        //        instance._ShowJobDetailView = value;
        //        NotifyPropertyChanged();
        //    }
        //}
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