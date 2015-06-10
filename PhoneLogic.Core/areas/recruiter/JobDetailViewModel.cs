using System;
using Microsoft.Lync.Model;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;
using Silverlight.Base.MVVMBaseTypes;


namespace PhoneLogic.Core.ViewModels
{
    public class JobDetailViewModel : ViewModelBase
    {
       
        public async void GetPhoneLogicTask(string jobNum, string taskid)
        {
               PhoneLogicTask = await PhoneLogicTaskSvc.GetTask(jobNum,taskid);
        }

        private static string msg = "Bad Job Detail Data";
        private PhoneLogicTask _phoneLogicTask = new PhoneLogicTask()
                {
                    JobNum = "00000000",
                    call_cnt = 0,
                    taskDscr = msg,
                    taskName = msg,
                    taskTypeID = msg
                };

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
