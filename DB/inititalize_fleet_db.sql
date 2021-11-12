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
	DECLARE @sql nvarchar(100)
	
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

