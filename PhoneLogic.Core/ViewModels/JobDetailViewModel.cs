using PhoneLogic.Core.Services;
using PhoneLogic.Model;


namespace PhoneLogic.Core.ViewModels
{
    public class JobDetailViewModel : ViewModelBase
    {
        public JobDetailViewModel()
        {   

            GetPhoneLogicTask(ConversationContext.Instance.PhoneLogicContext.jobNumber,
                               ConversationContext.Instance.PhoneLogicContext.TaskID);
        }
       
        public async void GetPhoneLogicTask(string jobNum, string taskid)
        {
            if (ConversationContext.Instance.PhoneLogicContext == null) return;
            if (string.IsNullOrEmpty(ConversationContext.Instance.PhoneLogicContext.jobNumber)) return;
            PhoneLogicTask = await PhoneLogicTaskSvc.GetTask(jobNum,taskid);
        }

        private PhoneLogicTask _phoneLogicTask = new PhoneLogicTask();
        public PhoneLogicTask  PhoneLogicTask
        {
            get { return _phoneLogicTask; }
            set
            {
                _phoneLogicTask = value;
                NotifyPropertyChanged();
            }
        }

    }
}
