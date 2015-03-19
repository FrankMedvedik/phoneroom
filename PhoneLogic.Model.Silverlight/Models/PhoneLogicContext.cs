namespace PhoneLogic.Model
{
   #if SILVERLIGHT
    using System.ComponentModel;
    using System;
    using System.Collections.Generic;
    public class PhoneLogicContext : INotifyPropertyChanged
#else
    using System;
    using System.Collections.Generic;
    public class PhoneLogicContext 
#endif
    {
        private string _conversationId;
        public string conversationId
        {
            get { return _conversationId; }
            set
            {
                _conversationId = value;
#if SILVERLIGHT
            NotifyPropertyChanged();
#endif
                  }
        }

        private string _dialedNumber;
        public string dialedNumber 
        {
            get { return _dialedNumber; }
            set
            {
                _dialedNumber  = value;
#if SILVERLIGHT
            NotifyPropertyChanged();
#endif
                  }
        }

        private string _callerId;
        public string callerId 
        {
            get { return _callerId; }
            set
            {
                _callerId  = value;
#if SILVERLIGHT
            NotifyPropertyChanged();
#endif
                  }
        }

        private string _jobNumber;
        public string jobNumber 
        {
            get { return _jobNumber; }
            set
            {
                _jobNumber  = value;
#if SILVERLIGHT
            NotifyPropertyChanged();
#endif
                  }
        }

        private int _taskId;
        public int taskId
        {
            get { return _taskId; }
            set
            {
                _taskId = value;
#if SILVERLIGHT
            NotifyPropertyChanged();
#endif
            }
        }


        public string TaskID
        {
            get { return taskId.ToString(); }
        }


        private string _timeReceived;
        public string timeReceived
        {
            get { return _timeReceived; }
            set
            {
                _timeReceived  = value;
#if SILVERLIGHT
            NotifyPropertyChanged("timeReceived");
#endif

            }
        }

        private int _callbackId;

        public int callbackId
        {
            get { return _callbackId; }
            set
            {
                _callbackId = value;
        #if SILVERLIGHT
            NotifyPropertyChanged("callbackId");
        #endif
            }
        }

    }

}
