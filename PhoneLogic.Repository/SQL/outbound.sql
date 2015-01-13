


--DROP PROC [dbo].[pl_GetMyCallbacks_sip]
--go
--CREATE PROC [dbo].[pl_GetMyCallbacks_sip]
--	@SIP nvarCHAR(256)
--AS
--SELECT top 100 c.callbackID,
--		c.jobNum,
--		c.taskID,
--		c.phoneNum,
--		c.msgLength,
--		c.closeDate,
--		CAST(ISNULL(ac.[Timezone Offset (from UTC)], 0) AS INT) as UTC_code,
--		ISNULL(ac.Region, '') as Region,
--		c.timeEntered,
--		tft.tollFreeNumber,
--		CASE WHEN SUBSTRING(c.msgScr,1,1) = 'C' THEN REPLACE(c.msgScr, 'c:\PhoneLogicMedia\Recordings','') 
--			ELSE REPLACE(c.msgScr,'F:\Recordings\','') END as msgScr
--FROM callbacks c
--	INNER JOIN taskAgent ta ON ta.jobNum = c.jobNum AND ta.taskID = c.taskID
--	INNER JOIN agent a ON ta.agentId = a.agentId 
--	INNER JOIN task t ON t.jobNum = ta.jobNum AND t.taskID = ta.taskID
--	LEFT JOIN tollFreeTask tft ON tft.jobNum = ta.jobNum AND tft.taskID = ta.taskID
--	INNER JOIN reference.dbo.na_area_codes ac
--	ON SUBSTRING(c.phonenum,1,3) = ac.code
---- added by dr 8/18/2014	

--where not exists (select 1 from returnedCalls r where c.callbackID = r.CallbackID and r.endCallDate is null)
---- AND a.SIP = @SIP
--	order by c.timeEntered 
--GO

--exec pl_GetMyCallbacks_sip 'A!'


--use PhoneLogic

----- CREATE COLUMN: status
-----
--ALTER TABLE dbo.callbacks ADD status nchar(20)
--GO
-----
----- CREATE COLUMN: SIP
-----
--ALTER TABLE dbo.callbacks ADD SIP nchar(256)
--GO


--exec pl_GetMyCallbacks_sip 'A!'


--create index agent_sip_ak on agent(sip)

--//create unique index agent_sip_ak on agent(sip)




--use PhoneLogic
-----
----- CREATE TABLE: returnedCalls
-----
--CREATE TABLE returnedCalls
--(
--	callbackID int NOT NULL IDENTITY,
--	SIP nvarchar(256) NOT NULL,
--	startCallDate datetime NOT NULL,
--	endCallDate datetime,
--	phoneNum char(10),
--	PRIMARY KEY CLUSTERED (callbackID, SIP, startCallDate)
--)
--GO
-----
----- CREATE COLUMN: closeDate
-----
--ALTER TABLE dbo.callbacks ADD closeDate datetime
--GO
-----
----- CREATE COLUMN: msgLength
-----
--ALTER TABLE dbo.callbacks ADD msgLength int
--GO




--CREATE PROC [dbo].[pl_getJobCallStats]
--AS
--select jobnum, min(timeEntered) oldest_call, max(timeEntered) newest_call, count(*) call_cnt
--from callbacks
--where closedate is null
--group by jobnum