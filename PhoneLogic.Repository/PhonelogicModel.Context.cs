﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PhoneLogicEntities : DbContext
    {
        public PhoneLogicEntities()
            : base("name=PhoneLogicEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<returnedCall> returnedCalls { get; set; }
        public virtual DbSet<inboundCall> inboundCalls { get; set; }
        public virtual DbSet<callback> callbacks { get; set; }
        public virtual DbSet<agent> agents { get; set; }
        public virtual DbSet<agentLog> agentLogs { get; set; }
        public virtual DbSet<callLog> callLogs { get; set; }
        public virtual DbSet<vw_PhoneLogicTaskAgent> vw_PhoneLogicTaskAgent { get; set; }
    
        public virtual ObjectResult<callbackDTO> getMyCallbacks(string sIP, string jobNum, string taskId)
        {
            var sIPParameter = sIP != null ?
                new ObjectParameter("SIP", sIP) :
                new ObjectParameter("SIP", typeof(string));
    
            var jobNumParameter = jobNum != null ?
                new ObjectParameter("jobNum", jobNum) :
                new ObjectParameter("jobNum", typeof(string));
    
            var taskIdParameter = taskId != null ?
                new ObjectParameter("taskId", taskId) :
                new ObjectParameter("taskId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<callbackDTO>("getMyCallbacks", sIPParameter, jobNumParameter, taskIdParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> InsertPlacedCall(string agentID, string jobNumber, string callerID, string eventDscr)
        {
            var agentIDParameter = agentID != null ?
                new ObjectParameter("agentID", agentID) :
                new ObjectParameter("agentID", typeof(string));
    
            var jobNumberParameter = jobNumber != null ?
                new ObjectParameter("jobNumber", jobNumber) :
                new ObjectParameter("jobNumber", typeof(string));
    
            var callerIDParameter = callerID != null ?
                new ObjectParameter("callerID", callerID) :
                new ObjectParameter("callerID", typeof(string));
    
            var eventDscrParameter = eventDscr != null ?
                new ObjectParameter("eventDscr", eventDscr) :
                new ObjectParameter("eventDscr", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("InsertPlacedCall", agentIDParameter, jobNumberParameter, callerIDParameter, eventDscrParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> InsertSuccessfullCallback(string agentID, Nullable<int> callbackID, string eventDscr)
        {
            var agentIDParameter = agentID != null ?
                new ObjectParameter("agentID", agentID) :
                new ObjectParameter("agentID", typeof(string));
    
            var callbackIDParameter = callbackID.HasValue ?
                new ObjectParameter("callbackID", callbackID) :
                new ObjectParameter("callbackID", typeof(int));
    
            var eventDscrParameter = eventDscr != null ?
                new ObjectParameter("eventDscr", eventDscr) :
                new ObjectParameter("eventDscr", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("InsertSuccessfullCallback", agentIDParameter, callbackIDParameter, eventDscrParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> CloseCallback(Nullable<int> callbackID, string sIP)
        {
            var callbackIDParameter = callbackID.HasValue ?
                new ObjectParameter("callbackID", callbackID) :
                new ObjectParameter("callbackID", typeof(int));
    
            var sIPParameter = sIP != null ?
                new ObjectParameter("SIP", sIP) :
                new ObjectParameter("SIP", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("CloseCallback", callbackIDParameter, sIPParameter);
        }
    
        public virtual ObjectResult<rptMyPhoneLogicTask> GetMyPhoneLogicTasks(string sIP)
        {
            var sIPParameter = sIP != null ?
                new ObjectParameter("SIP", sIP) :
                new ObjectParameter("SIP", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rptMyPhoneLogicTask>("GetMyPhoneLogicTasks", sIPParameter);
        }
    
        public virtual ObjectResult<PhoneLogicTaskDTO> GetPhoneLogicTask(string jobNum, Nullable<int> taskID)
        {
            var jobNumParameter = jobNum != null ?
                new ObjectParameter("jobNum", jobNum) :
                new ObjectParameter("jobNum", typeof(string));
    
            var taskIDParameter = taskID.HasValue ?
                new ObjectParameter("taskID", taskID) :
                new ObjectParameter("taskID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PhoneLogicTaskDTO>("GetPhoneLogicTask", jobNumParameter, taskIDParameter);
        }
    
        public virtual ObjectResult<rptGetJobCallActivityResult> rpt_GetJobCallActivity(Nullable<System.DateTime> start, Nullable<System.DateTime> end)
        {
            var startParameter = start.HasValue ?
                new ObjectParameter("Start", start) :
                new ObjectParameter("Start", typeof(System.DateTime));
    
            var endParameter = end.HasValue ?
                new ObjectParameter("End", end) :
                new ObjectParameter("End", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rptGetJobCallActivityResult>("rpt_GetJobCallActivity", startParameter, endParameter);
        }
    
        public virtual ObjectResult<LyncCallLog> rpt_GetLyncCallLog(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LyncCallLog>("rpt_GetLyncCallLog", startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<LyncCallLog> rpt_GetRecruiterLyncCallLog(string sip, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var sipParameter = sip != null ?
                new ObjectParameter("sip", sip) :
                new ObjectParameter("sip", typeof(string));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LyncCallLog>("rpt_GetRecruiterLyncCallLog", sipParameter, startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<LyncCallRecruiter> rpt_GetLyncCallRecruiters(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LyncCallRecruiter>("rpt_GetLyncCallRecruiters", startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<LyncCallJobRecruiter> rpt_GetLyncCallJobRecruiters(string jobNum, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var jobNumParameter = jobNum != null ?
                new ObjectParameter("JobNum", jobNum) :
                new ObjectParameter("JobNum", typeof(string));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LyncCallJobRecruiter>("rpt_GetLyncCallJobRecruiters", jobNumParameter, startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<LyncCallLog> rpt_GetLyncCallJobRecruiterLog(string jobNum, string sip, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var jobNumParameter = jobNum != null ?
                new ObjectParameter("jobNum", jobNum) :
                new ObjectParameter("jobNum", typeof(string));
    
            var sipParameter = sip != null ?
                new ObjectParameter("sip", sip) :
                new ObjectParameter("sip", typeof(string));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LyncCallLog>("rpt_GetLyncCallJobRecruiterLog", jobNumParameter, sipParameter, startDateParameter, endDateParameter);
        }
    
        public virtual int rpt_GetCallbackRpt(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("rpt_GetCallbackRpt", startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<callbackDTO> rpt_GetJobCallbacks(string jobNum, string taskId, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var jobNumParameter = jobNum != null ?
                new ObjectParameter("jobNum", jobNum) :
                new ObjectParameter("jobNum", typeof(string));
    
            var taskIdParameter = taskId != null ?
                new ObjectParameter("taskId", taskId) :
                new ObjectParameter("taskId", typeof(string));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<callbackDTO>("rpt_GetJobCallbacks", jobNumParameter, taskIdParameter, startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<callbackDTO> rpt_GetMyCallbacks(string sIP, string jobNum, string taskId)
        {
            var sIPParameter = sIP != null ?
                new ObjectParameter("SIP", sIP) :
                new ObjectParameter("SIP", typeof(string));
    
            var jobNumParameter = jobNum != null ?
                new ObjectParameter("jobNum", jobNum) :
                new ObjectParameter("jobNum", typeof(string));
    
            var taskIdParameter = taskId != null ?
                new ObjectParameter("taskId", taskId) :
                new ObjectParameter("taskId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<callbackDTO>("rpt_GetMyCallbacks", sIPParameter, jobNumParameter, taskIdParameter);
        }
    
        public virtual ObjectResult<LyncCallJob> rpt_GetLyncCallJobs(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LyncCallJob>("rpt_GetLyncCallJobs", startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<rpt_GetLyncCallRecruiterJobs> rpt_GetLyncCallRecruiterJobs(string sip, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var sipParameter = sip != null ?
                new ObjectParameter("sip", sip) :
                new ObjectParameter("sip", typeof(string));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<rpt_GetLyncCallRecruiterJobs>("rpt_GetLyncCallRecruiterJobs", sipParameter, startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<LyncCallLog> rpt_GetCallerLyncCallLog(string callerId)
        {
            var callerIdParameter = callerId != null ?
                new ObjectParameter("callerId", callerId) :
                new ObjectParameter("callerId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LyncCallLog>("rpt_GetCallerLyncCallLog", callerIdParameter);
        }
    
        public virtual ObjectResult<CallbackRpt> rpt_GetOpenCallbackRpt()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CallbackRpt>("rpt_GetOpenCallbackRpt");
        }
    
        public virtual ObjectResult<CallbackRpt> rpt_GetClosedCallbackRpt(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CallbackRpt>("rpt_GetClosedCallbackRpt", startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<callbackDTO> rpt_GetClosedJobCallbacks(string jobNum, string taskId, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var jobNumParameter = jobNum != null ?
                new ObjectParameter("jobNum", jobNum) :
                new ObjectParameter("jobNum", typeof(string));
    
            var taskIdParameter = taskId != null ?
                new ObjectParameter("taskId", taskId) :
                new ObjectParameter("taskId", typeof(string));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<callbackDTO>("rpt_GetClosedJobCallbacks", jobNumParameter, taskIdParameter, startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<callbackDTO> rpt_GetOpenJobCallbacks(string jobNum, string taskId)
        {
            var jobNumParameter = jobNum != null ?
                new ObjectParameter("jobNum", jobNum) :
                new ObjectParameter("jobNum", typeof(string));
    
            var taskIdParameter = taskId != null ?
                new ObjectParameter("taskId", taskId) :
                new ObjectParameter("taskId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<callbackDTO>("rpt_GetOpenJobCallbacks", jobNumParameter, taskIdParameter);
        }
    
        public virtual ObjectResult<LyncCallLog> rpt_GetLyncCallsforPhoneInDateRange(string phoneNumber, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var phoneNumberParameter = phoneNumber != null ?
                new ObjectParameter("PhoneNumber", phoneNumber) :
                new ObjectParameter("PhoneNumber", typeof(string));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LyncCallLog>("rpt_GetLyncCallsforPhoneInDateRange", phoneNumberParameter, startDateParameter, endDateParameter);
        }
    }
}
