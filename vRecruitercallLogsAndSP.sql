-- all connected then completed calls  

 drop view vLyncRecruiterCallLog 
 go
 create view vLyncRecruiterCallLog as
 select strtcall.logId StartLogId,
 strtcall.callID,
 strtcall.JobNumber,
strtcall.callerId, 
strtcall.tollFreeNumber,
strtcall.eventTime callStartTime,
ctcall.eventTime recruiterConnectTime,
endcall.eventTime CallEndTime,
endcall.recruiterSIP,
CONVERT(varchar,(ctcall.eventTime - strtcall.eventTime ), 108) preConnectDuration,
CONVERT(varchar,(endcall.eventTime - strtcall.eventTime ), 108) TotalCallDuration,
CONVERT(varchar,(endcall.eventTime - ctcall.eventTime ), 108) recruiterCallDuration
 from lyncCallStatusLog strtcall LEFT OUTER JOIN lyncCallStatusLog ctcall ON strtcall.callID =  ctcall.callID and ctcall.callStatus = 'Connected'
 LEFT OUTER JOIN lyncCallStatusLog  endCall ON strtcall.callID =  endCall.callID and endCall.callStatus = 'Completed'
 where strtcall.callStatus = 'Incoming'
and endcall.recruiterSip is not null
 
 
 drop proc [rpt_GetRecruiterCallLog]
go
  CREATE PROC [dbo].[rpt_GetRecruiterCallLog]
	@sip nvarchar(50),
	@startDate DATETIME,
	@endDate DATETIME
AS
  SELECT 
	callerId
	,callId
	,callStartTime
	,callEndTime
	,recruiterConnectTime
	,preconnectDuration
	,recruiterCallDuration
	,recruiterSip
	,tollfreeNumber
	,totalCallDuration
	,StartLogId
	,JobNumber
FROM vlyncRecruiterCallLog l
WHERE l.recruiterSip = @sip and l.CallstartTime between @startDate and @endDate
ORDER BY CallstartTime 
GO


exec rpt_GetRecruiterCallLog 'sip:fmedvedik@reckner.com' , '4/1/2015', '5/15/2015'

call dispo view
all calls with relevant dates transposed expanded vLyncRecruiterCallLog to vLyncCallLog