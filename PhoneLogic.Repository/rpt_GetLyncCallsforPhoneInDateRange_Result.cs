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
    
    public partial class rpt_GetLyncCallsforPhoneInDateRange_Result
    {
        public Nullable<int> StartLogId { get; set; }
        public string CallID { get; set; }
        public string JobNumber { get; set; }
        public string taskdscr { get; set; }
        public string taskName { get; set; }
        public string TaskTypeId { get; set; }
        public string CallerId { get; set; }
        public Nullable<int> CallerId_UTC_code { get; set; }
        public string CallerId_Region { get; set; }
        public string TollFreeNumber { get; set; }
        public Nullable<System.DateTime> CallStartTime { get; set; }
        public Nullable<System.DateTime> RecruiterConnectTime { get; set; }
        public Nullable<System.DateTime> CallEndTime { get; set; }
        public string RecruiterSIP { get; set; }
        public string CallDirection { get; set; }
        public string CallEndStatus { get; set; }
        public Nullable<int> CallDuration { get; set; }
    }
}
