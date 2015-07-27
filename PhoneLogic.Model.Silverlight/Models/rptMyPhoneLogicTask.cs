using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneLogic.Model
{
    public class rptMyPhoneLogicTask
    {
        public string JobNum { get; set; }
        public int taskID { get; set; }
        public string TaskName { get; set; }
        public string TaskDscr { get; set; }
        public string RecruiterSIP { get; set; }
        public Nullable<System.DateTime> OldestMsg { get; set; }
        public Nullable<System.DateTime> NewestMsg { get; set; }
        public Nullable<int> MsgCnt { get; set; }
        public Nullable<int> CallCnt { get; set; }
        public Nullable<int> InboundCallCnt { get; set; }
        public Nullable<int> OutboundCallCnt { get; set; }
        public Nullable<int> TotalCallDuration { get; set; }
        public Nullable<int> AvgCallDuration { get; set; }
        public Nullable<int> MaxCallDuration { get; set; }
        public Nullable<int> UniqueCallerCnt { get; set; }
        public Nullable<System.DateTime> FirstCallTime { get; set; }
        public Nullable<System.DateTime> LastCallTime { get; set; }
    }
}
