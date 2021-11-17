USE fleet_db
GO

DECLARE @token varbinary(64)
EXECUTE PROC_AUTHORIZE @input_username = 'user2', @input_passwd = 'Fall2021'
SELECT @token

SELECT * FROM CONF_SESSIONS

DELETE FROM CONF_SESSIONS WHERE 1=1

