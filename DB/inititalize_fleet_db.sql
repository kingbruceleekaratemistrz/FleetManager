/* PLIK NIEAKTUALNY */

/* Tworzenie bazy danych do aplikacji do zarz¹dzania flot¹ */
IF NOT EXISTS (SELECT d.[name]
				FROM sys.databases d
				WHERE	(d.database_id > 4)
				AND		(d.[name] = N'fleet_db'))
BEGIN
	CREATE DATABASE fleet_db
END
GO


/* Tworzenie tabeli przechowuj¹cej loginy i has³a u¿ytkowników */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'USERS_CRED')
				AND		(OBJECTPROPERTY(o.[ID], N'IsUserTable') = 1))
BEGIN
	CREATE TABLE dbo.USERS_CRED
	(	username	nvarchar(30)	NOT NULL CONSTRAINT PK_USERS_CRED PRIMARY KEY
	,	passwd		nvarchar(30)	NOT NULL
	)
END
GO

INSERT INTO dbo.USERS_CRED (username, passwd) values ('admin', 'admin')
INSERT INTO dbo.USERS_CRED (username, passwd) values ('user1', 'passwd123')
INSERT INTO dbo.USERS_CRED (username, passwd) values ('user2', 'Fall2021')
/* SELECT * FROM dbo.USERS_CRED */
/* Zapisywanie na bazie procedury logowania */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'USERS_LOGIN')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.USERS_LOGIN AS '
	EXEC sp_sqlexec @stmt
END
GO

ALTER PROCEDURE dbo.USERS_LOGIN (@input_username nvarchar(30), @input_passwd nvarchar(30))
AS
	DECLARE @sql nvarchar(200)
	
	SET @input_username = LTRIM(RTRIM(@input_username))
	SET @input_passwd = LTRIM(RTRIM(@input_passwd))

	SET @sql = N'USE fleet_db '
				+ N'SELECT * FROM dbo.USERS_CRED '
				+ N'WHERE username = ''' + @input_username + ''''
				+ N' AND passwd = '''+ @input_passwd + ''''
	EXEC sp_sqlexec @sql
RETURN
GO

EXEC fleet_db.dbo.USERS_LOGIN @input_username = 'user2', @input_passwd = 'Fall2021'

USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'USERS_PROFILE')
				AND		(OBJECTPROPERTY(o.[ID], N'IsUserTable') = 1))
BEGIN
	CREATE TABLE dbo.USERS_PROFILE
	(	username	nvarchar(30)	NOT NULL CONSTRAINT FK_USERS_PROFILE__USERS_CRED 
									FOREIGN KEY REFERENCES dbo.USERS_CRED(username)
	,	first_name	nvarchar(30)	NOT NULL
	,	last_name	nvarchar(50)	NOT NULL
	,	company		nvarchar(50)	NOT NULL
	,	photo_url	nvarchar(100)	NOT NULL
)
END

INSERT INTO dbo.USERS_PROFILE (username, first_name, last_name, company, photo_url)
	VALUES ('admin', 'Admini', 'Strator', 'tak', 'C:\Users\pduln\Documents\GitHub\FleetManager\Assets\Profile_pictures\profile_pic_1.png')
INSERT INTO dbo.USERS_PROFILE (username, first_name, last_name, company, photo_url)
	VALUES ('user1', 'Pan', 'Jan', 'Macrosoft', 'C:\Users\pduln\Documents\GitHub\FleetManager\Assets\Profile_pictures\profile_pic_2.png')
INSERT INTO dbo.USERS_PROFILE (username, first_name, last_name, company, photo_url)
	VALUES ('user2', 'Frodo', 'Baggins', 'Netflix', 'C:\Users\pduln\Documents\GitHub\FleetManager\Assets\Profile_pictures\profile_pic_3.png')

SELECT * FROM fleet_db.dbo.USERS_PROFILE WHERE username = 'admin'


/* Procedura zwracaj¹ca informacje o profilu u¿ytkownika. */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'USERS_GET_PROFILE')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.USERS_GET_PROFILE AS '
	EXEC sp_sqlexec @stmt
END
GO

ALTER PROCEDURE dbo.USERS_GET_PROFILE (@input_username nvarchar(30))
AS
	DECLARE @sql nvarchar(200)
	
	SET @input_username = LTRIM(RTRIM(@input_username))

	SET @sql = N'USE fleet_db '
				+ N'SELECT * FROM USERS_PROFILE WHERE username = ''' + @input_username + ''''
	EXEC sp_sqlexec @sql
RETURN
GO

EXEC USERS_GET_PROFILE @input_username = 'admin'



USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_AUTHORIZE')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_AUTHORIZE AS '
	EXEC sp_sqlexec @stmt
END
GO

ALTER PROCEDURE dbo.PROC_AUTHORIZE (@input_username nvarchar(30), @input_passwd nvarchar(30))
AS
	DECLARE @sql nvarchar(500)
	DECLARE @ParmDefinition nvarchar(500)
	DECLARE @result int
	
	SET @input_username = LTRIM(RTRIM(@input_username))
	SET @input_passwd = LTRIM(RTRIM(@input_passwd))
	SET @ParmDefinition = N'@username nvarchar(30), @password nvarchar(30), @resultOUT int OUTPUT'

	SET @sql = N'USE fleet_db '
				+ N'SELECT @resultOUT = COUNT (*) FROM dbo.USERS_CRED '
				+ N'WHERE username = @username AND passwd = @password'
	EXEC sp_executesql @sql, @ParmDefinition, @username = @input_username, @password = @input_passwd, @resultOUT = @result OUTPUT
	IF @result = 1
	BEGIN
		DECLARE @token varbinary(64)
		SET @token = CRYPT_GEN_RANDOM(64)
		INSERT INTO fleet_db.dbo.CONF_SESSIONS (username, sessionID) VALUES (@input_username, @token)
		SELECT @token
	END
	ELSE
		SELECT 0
GO

USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'CONF_SESSIONS')
				AND		(OBJECTPROPERTY(o.[ID], N'IsUserTable') = 1))
BEGIN
	CREATE TABLE dbo.CONF_SESSIONS
	(	username	nvarchar(30)	NOT NULL CONSTRAINT FK_CONF_SESSIONS__USERS_CRED 
									FOREIGN KEY REFERENCES dbo.USERS_CRED(username)
	,	sessionID	varbinary(64)	NOT NULL
)
END

EXECUTE fleet_db.dbo.PROC_AUTHORIZE @input_username='admin', @input_passwd='admin'

SELECT * FROM CONF_SESSIONS

SELECT CRYPT_GEN_RANDOM(64)