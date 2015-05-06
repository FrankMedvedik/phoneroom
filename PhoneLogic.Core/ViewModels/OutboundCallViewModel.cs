using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Lync.Model;
using Newtonsoft.Json;
using PhoneLogic.Core.Models;
using PhoneLogic.Core.Services;
using PhoneLogic.Model;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.ViewModels
{
    public class OutboundCallViewModel : ViewModelBase
    {
        private OutboundCall _outboundCall = new OutboundCall();
        public OutboundCall OutboundCall
        {
            get { return _outboundCall; }
            set
            {
                _outboundCall = value;
                NotifyPropertyChanged();
            }
        }
        private Boolean _canMakeCall = false;
        public Boolean CanMakeCall
        {
            get { return _canMakeCall; }
            set
            {
                _canMakeCall = value;
                NotifyPropertyChanged();
            }
        }

        private PhoneLogicTask _task;
        public PhoneLogicTask Task
        {
            get { return _task; }
            set
            {
                ViewVisible =  (value != null ) ? true: false;
                if (_task != value)
                {
                    _task = value;
                    OutboundCall.jobNum = _task.JobNum;
                    OutboundCall.taskID = _task.taskID ?? default(int); 
                    NotifyPropertyChanged();
                }
            }
        }
        private Boolean _viewVisible = true;
        public Boolean ViewVisible
        {
            get { return _viewVisible; }
            set
            {
                _viewVisible = value;
                NotifyPropertyChanged();
            }
        }

    }
}

