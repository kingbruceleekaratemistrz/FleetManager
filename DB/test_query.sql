USE fleet_db
GO
SELECT * FROM COMP_PROFILES
SELECT * FROM USERS_CRED
SELECT * FROM USERS_PROFILES
SELECT * FROM CARS
SELECT * FROM CAR_SERVICES
SELECT * FROM [SERVICES] ORDER BY service_id
SELECT * FROM REPAIR_HISTORY

DELETE CONF_SESSIONS WHERE 1=1

--0x3DA3371643D3BA4B2B20F593557BF1A805FE31966BFC83F4636D54870AEDC28FD76F0F31C21EE9FB1366C792A0700E281F1F7B5476AF87EF86C1C468232F3227
SELECT u.acc FROM USERS_CRED u
JOIN CONF_SESSIONS c ON u.username=c.username
WHERE c.sessionID = 0x3DA3371643D3BA4B2B20F593557BF1A805FE31966BFC83F4636D54870AEDC28FD76F0F31C21EE9FB1366C792A0700E281F1F7B5476AF87EF86C1C468232F3227

EXEC PROC_GET_ACC @token=0x3DA3371643D3BA4B2B20F593557BF1A805FE31966BFC83F4636D54870AEDC28FD76F0F31C21EE9FB1366C792A0700E281F1F7B5476AF87EF86C1C468232F3227

SELECT * FROM USERS_CRED

/* Usuwanie tabel */
/*	
USE fleet_db 
GO
DROP TABLE CONF_SESSIONS
DROP TABLE USERS_PROFILES
DROP TABLE CARS
DROP TABLE COMP_PROFILES
DROP TABLE USERS_CRED
*/
/* Wyswietlanie tabel */
/*
USE fleet_db
GO
SELECT * FROM COMP_PROFILES
SELECT * FROM CONF_SESSIONS
SELECT * FROM USERS_CRED
SELECT * FROM USERS_PROFILES
SELECT * FROM CARS
*/

DELETE FROM CONF_SESSIONS WHERE 1=1

SELECT * FROM CONF_SESSIONS
--0x53AB63A23E6750ED289AD8CEFEBD85BED6B1AD5BBE179141660F2738400D4D84F2D24F4E6512ACA4CD2B70BEE70D7B8B7ED5A65ADF3DEDFA3AB6D8BF8D40BB0E
SELECT h.* FROM REPAIR_HISTORY h
JOIN USERS_PROFILES p ON h.plate_number = p.car_plate
JOIN CONF_SESSIONS c ON p.username = c.username
JOIN [SERVICES] s ON h.
WHERE c.sessionID=0x53AB63A23E6750ED289AD8CEFEBD85BED6B1AD5BBE179141660F2738400D4D84F2D24F4E6512ACA4CD2B70BEE70D7B8B7ED5A65ADF3DEDFA3AB6D8BF8D40BB0E

SELECT s.[description], c.[name], r.[date] FROM REPAIR_HISTORY r
JOIN [SERVICES] s ON r.service_id = s.service_id
JOIN CAR_SERVICES c ON r.car_service_id = c.car_service_id
JOIN USERS_PROFILES u ON r.plate_number = u.car_plate
JOIN CONF_SESSIONS cs ON u.username = cs.username
WHERE cs.sessionID = 0x53AB63A23E6750ED289AD8CEFEBD85BED6B1AD5BBE179141660F2738400D4D84F2D24F4E6512ACA4CD2B70BEE70D7B8B7ED5A65ADF3DEDFA3AB6D8BF8D40BB0E
ORDER BY r.[date]

DECLARE @tmp varbinary(64)
SET @tmp = 0x53AB63A23E6750ED289AD8CEFEBD85BED6B1AD5BBE179141660F2738400D4D84F2D24F4E6512ACA4CD2B70BEE70D7B8B7ED5A65ADF3DEDFA3AB6D8BF8D40BB0E
SELECT @tmp
SELECT CONVERT(nvarchar(128), @tmp, 2)
SELECT CONVERT(varbinary(64), CONVERT(nvarchar(128), @tmp, 2), 2)

DECLARE @tmp2 varbinary(64)
SET @tmp2 = '0x53AB63A23E6750ED289AD8CEFEBD85BED6B1AD5BBE179141660F2738400D4D84F2D24F4E6512ACA4CD2B70BEE70D7B8B7ED5A65ADF3DEDFA3AB6D8BF8D40BB0E'
SELECT @tmp2

EXEC PROC_GET_REPAIR_HISTORY_LIST @token=0x53AB63A23E6750ED289AD8CEFEBD85BED6B1AD5BBE179141660F2738400D4D84F2D24F4E6512ACA4CD2B70BEE70D7B8B7ED5A65ADF3DEDFA3AB6D8BF8D40BB0E
USE fleet_db
SELECT * FROM USERS_CRED c
JOIN USERS_PROFILES p ON c.username = p.username
