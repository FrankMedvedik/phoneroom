USE [PhoneLogic]
GO

/****** Object:  View [dbo].[vLyncCallLog]    Script Date: 08/28/2015 12:26:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create view [dbo].[vLyncCallLog] as
select 
strtcall.StartLogId,
strtcall.CallID,
strtcall.JobNumber,
strtcall.CallerId, 
strtcall.TollFreeNumber,
strtcall.CallStartTime,
ctcall.eventTime RecruiterConnectTime,
endcall.eventTime CallEndTime,
strtcall.RecruiterSIP,
strtcall.CallDirection,
endcall.callstatus CallEndStatus,
DATEDIFF(s,strtcall.CallStartTime, endcall.eventTime) CallDuration
from ( 
        select callDirection, 
                        MIN(logId) StartLogId, 
                        MAX(logId) EndLogId, 
                        Min(jobNumber) jobNumber, 
                        Max(callerId) callerId, 
                        MAX(tollFreeNumber) tollFreeNumber, 
                        MIN(eventTime) callStartTime, 
                        Max(eventTime) callEndTime, 
                        max(recruiterSIP) recruiterSIP,  
                        a.callID 
         from 
                (select callStatus callDirection, 
                                callID 
                from lyncCallStatusLog 
                where callStatus in( 'Incoming', 'Outgoing'))  a join  lyncCallStatusLog b on  a.callid = b.callid 
        group by a.callid, 
                         a.calldirection) strtcall LEFT JOIN lyncCallStatusLog ctcall ON strtcall.callID = ctcall.callID and ctcall.callStatus = 'Connected'
JOIN lyncCallStatusLog  endCall ON strtcall.endLogID =  endCall.LogID 




GO

drop table LyncCallLogHist 
select * into LyncCallLogHist from vLyncCallLog where callstarttime < '8/28/2015'

insert into LyncCallLogHist select * from vLyncCallLog 
where callid not in(select distinct callid from vLyncCallLogHistory ) 
and callendtime is not null




 CREATE PROC [dbo].[postLyncCall]  @callid varchar(255)
as
INSERT into LyncCallLogHist 
(StartLogId,
CallID,
JobNumber,
CallerId, 
TollFreeNumber,
CallStartTime,
RecruiterConnectTime,
CallEndTime,
RecruiterSIP,
CallDirection,
CallEndStatus,
CallDuration )
select 
StartLogId,
CallID,
JobNumber,
CallerId, 
TollFreeNumber,
CallStartTime,
RecruiterConnectTime,
CallEndTime,
RecruiterSIP,
CallDirection,
CallEndStatus,
CallDuration
from vLyncCallLogSrc
where callid =  @callid
go



CREATE TRIGGER postLyncCallTrg ON lyncCallStatusLog  FOR AFTER INSERT
AS
BEGIN
 SELECT @CALLID = TransactionID FROM INSERTED
 SELECT @CALLID = TransactionID FROM INSERTED
if(

END   


select callid from vLyncCallLogSrc s
where not exists (select 1 from LyncCallLogHist  b where s.callid = b.callid)
and callendtime < getdate() -.25





get everything since last run 
that fits criteria

 

create table lcArc
(


 CREATE PROC [dbo].[postLyncCalls]   as
INSERT into LyncCallLogHist 
(StartLogId,
CallID,
JobNumber,
CallerId, 
TollFreeNumber,
CallStartTime,
RecruiterConnectTime,
CallEndTime,
RecruiterSIP,
CallDirection,
CallEndStatus,
CallDuration )
select 
StartLogId,
CallID,
JobNumber,
CallerId, 
TollFreeNumber,
CallStartTime,
RecruiterConnectTime,
CallEndTime,
RecruiterSIP,
CallDirection,
CallEndStatus,
CallDuration
from vLyncCallLogSrc
where not exists (select 1 from LyncCallLogHist  b where s.callid = b.callid)
and callendtime < getdate() -.25

go



