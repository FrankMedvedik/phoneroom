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
    
    public partial class rptGetJobCallActivityResult
    {
        public string jobNum { get; set; }
        public int taskID { get; set; }
        public string taskName { get; set; }
        public string taskDscr { get; set; }
        public string taskTypeID { get; set; }
        public string taskStatusID { get; set; }
        public string ProjMgr { get; set; }
        public Nullable<int> InboundCall { get; set; }
        public Nullable<int> DirectCall { get; set; }
        public Nullable<int> QueuedCall { get; set; }
        public Nullable<int> PlaceQueuedCall { get; set; }
        public Nullable<int> Callback { get; set; }
        public Nullable<int> SuccessfullCallback { get; set; }
        public Nullable<int> OutboundCall { get; set; }
        public Nullable<int> AbandonedCall { get; set; }
        public Nullable<int> NoInterviewers { get; set; }
        public Nullable<int> ClosedJob { get; set; }
        public Nullable<int> PhoneRoomClosed { get; set; }
    }
}
