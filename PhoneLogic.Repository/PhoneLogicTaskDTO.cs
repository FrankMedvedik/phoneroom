//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PhoneLogic.Repository
{
    using System;
    
    public partial class PhoneLogicTaskDTO
    {
        public string jobNum { get; set; }
        public int taskID { get; set; }
        public string taskName { get; set; }
        public string taskDscr { get; set; }
        public Nullable<System.DateTime> TaskStartDate { get; set; }
        public Nullable<System.DateTime> TaskEndDate { get; set; }
        public string taskTypeID { get; set; }
        public string taskStatusID { get; set; }
        public bool isActive { get; set; }
        public int menuID { get; set; }
        public bool canEnhanceQueue { get; set; }
        public bool canZeroOut { get; set; }
        public System.DateTime stopCallbackTime { get; set; }
        public System.DateTime stopQueueTime { get; set; }
        public int closedMsgID { get; set; }
        public int closedHits { get; set; }
        public string internetURL { get; set; }
        public string catiURL { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
        public Nullable<System.DateTime> oldest_call { get; set; }
        public Nullable<System.DateTime> newest_call { get; set; }
        public Nullable<int> call_cnt { get; set; }
        public string tollFreeNumber { get; set; }
    }
}
