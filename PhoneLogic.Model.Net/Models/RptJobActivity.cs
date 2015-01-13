using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneLogic.Model
{
  public  class RptJobActivity
    {
        public string jobNum { get; set; }
        public int taskID { get; set; }

        [Display(Name = "Job")]
        public string jobFormatted
        {
            get
            {
                return jobNum.Substring(0, 4) + "-" + jobNum.Substring(4);
            }
        }
     [Display(Name = "Topic")]
     public string taskName { get; set; }
     [Display(Name = "Topic Description")]
     public string taskDscr { get; set; }
     [Display(Name = "Task Type")]
     public string taskTypeID { get; set; }
     [Display(Name = "Project Manager")]
      public string ProjMgr { get; set; }
      [Display(Name = "Inbound")]
      public Nullable<int> InboundCall { get; set; }
      [Display(Name = "Direct")]
      public Nullable<int> DirectCall { get; set; }
      [Display(Name = "Queued")]
      public Nullable<int> QueuedCall { get; set; }
      [Display(Name = "PlacedQueued")]
      public Nullable<int> PlaceQueuedCall { get; set; }
      [Display(Name = "Callback")]
      public Nullable<int> Callback { get; set; }
      [Display(Name = "Ok Callback")]
      public Nullable<int> SuccessfullCallback { get; set; }
      [Display(Name = "Outbound")]
      public Nullable<int> OutboundCall { get; set; }
      [Display(Name = "Abandoned")]
      public Nullable<int> AbandonedCall { get; set; }
      [Display(Name = "No Recruiters Avail")]
      public Nullable<int> NoInterviewers { get; set; }
      [Display(Name = "Closed Job")]
      public Nullable<int> ClosedJob { get; set; }
      [Display(Name = "Phone Room Closed")]
      public Nullable<int> PhoneRoomClosed { get; set; }

  }
}



