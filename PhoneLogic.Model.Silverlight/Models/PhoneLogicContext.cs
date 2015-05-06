namespace PhoneLogic.Model
{
    public class PhoneLogicContext 
    {
        public string ToString()
        {
            return
                string.Format("PhonelogicContext: ConversationId {0} | DialedNumber {1} | CallerId {2} | callbackId {3} | jobNumber {4} |TaskID {5} | taskId{6} | TimeReceived  {7} "
                , conversationId, dialedNumber, callerId, callbackId, jobNumber, TaskID, taskId, timeReceived);
                
        }
        private string _conversationId;
        public string conversationId
        {
            get { return _conversationId; }
            set
            {
                _conversationId = value;
                  }
        }

        private string _dialedNumber;
        public string dialedNumber 
        {
            get { return _dialedNumber; }
            set
            {
                _dialedNumber  = value;
                  }
        }

        private string _callerId;
        public string callerId 
        {
            get { return _callerId; }
            set
            {
                _callerId  = value;
                  }
        }

        private string _jobNumber;
        public string jobNumber 
        {
            get { return _jobNumber; }
            set
            {
                _jobNumber  = value;
                  }
        }

        private int _taskId;
        public int taskId
        {
            get { return _taskId; }
            set
            {
                _taskId = value;
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

            }
        }

        private int _callbackId;

        public int callbackId
        {
            get { return _callbackId; }
            set
            {
                _callbackId = value;
            }
        }

    }

}
