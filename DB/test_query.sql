USE fleet_db
GO

DECLARE @token varbinary(64)
EXECUTE PROC_AUTHORIZE @input_username = 'user2', @input_passwd = 'Fall2021'
SELECT @token

SELECT * FROM CONF_SESSIONS

DELETE FROM CONF_SESSIONS WHERE 1=1

EXEC PROC_CLOSE_SESSION @token=0x5051211491916AB65C1BCB9A85B749086B2DFC2563278D672C2359ADDF9E89752F0CA44E0E282BB68CDFC92B31E72BD451B12F476463133F418E322AC176C8F9
GO

DECLARE @token varbinary(64)
SET @token=0x825E358B05A74D35F81D600DB03F485EB5D5DD8D3BB1769105A9E0608C601B48C3083AE6DB23D269F96515CA55D03C56D607C88A26A8891CA450EAB16CADCE15
DECLARE @input_username nvarchar(30)
SET @input_username=NULL

IF @input_username IS NULL
BEGIN
	SELECT @input_username= username FROM CONF_SESSIONS WHERE sessionID=@token
	PRINT 'XDD'
END
PRINT @input_username