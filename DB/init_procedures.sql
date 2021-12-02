/* Tworzenie bazy danych do aplikacji do zarz�dzania flot� */
IF NOT EXISTS (SELECT d.[name]
				FROM sys.databases d
				WHERE	(d.database_id > 4)
				AND		(d.[name] = N'fleet_db'))
BEGIN
	CREATE DATABASE fleet_db
END
GO
/* Usuwanie procedur */
/*
USE fleet_db
GO
DROP PROCEDURE PROC_GET_USER_PROFILE
DROP PROCEDURE PROC_GET_SHORT_USER_PROFILE
DROP PROCEDURE PROC_GET_COMP_PROFILE
DROP PROCEDURE PROC_AUTHORIZE
DROP PROCEDURE PROC_CLOSE_SESSION
*/



/* Procedura zwracaj�ca informacje o profilu u�ytkownika. 
** @token s�u�y do weryfikacji sesji, je�li podany @token
** nie znajduje si� w tabeli CONF_SESSIONS to procedura
** nie zwr�ci danych. Je�li @username jest pomini�ty procedura
** zwr�ci dane o profilu pasuj�cym do @token
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_USER_PROFILE')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_USER_PROFILE AS '
	EXEC sp_sqlexec @stmt
END
GO

ALTER PROCEDURE dbo.PROC_GET_USER_PROFILE (@token varbinary(64), @input_username nvarchar(30) = NULL)
AS
	IF @input_username IS NULL
	BEGIN
		SELECT @input_username = username FROM CONF_SESSIONS WHERE sessionID=@token
	END

	DECLARE @sql nvarchar(200)
	
	SET @input_username = LTRIM(RTRIM(@input_username))

	SET @sql = N'USE fleet_db '
				+ N'SELECT * FROM USERS_PROFILES WHERE username = ''' + @input_username + ''''
	EXEC sp_sqlexec @sql
RETURN
GO



/* Procedura zwracaj�ca imi�, nazwisko, firm� i zdj�cie u�ytkownika */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_SHORT_USER_PROFILE')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_SHORT_USER_PROFILE AS '
	EXEC sp_sqlexec @stmt
END
GO

ALTER PROCEDURE dbo.PROC_GET_SHORT_USER_PROFILE @token varbinary(64)
AS
	DECLARE @user nvarchar(30)
	SELECT @user = username FROM CONF_SESSIONS
		WHERE sessionID = @token

	DECLARE @sql nvarchar(200)

	SET @sql = N'USE fleet_db '
				+ N'SELECT first_name, last_name, company, photo_url '
				+ N'FROM USERS_PROFILES '
				+ N'WHERE username=''' + @user + ''''
	EXEC sp_sqlexec @sql
RETURN
GO




/* Procedura zwracaj�ca informacje o profilu firmy. 
** @token s�u�y do weryfikacji sesji, je�li podany @token
** nie znajduje si� w tabeli CONF_SESSIONS to procedura
** nie zwr�ci danych.
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_COMP_PROFILE')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_COMP_PROFILE AS '
	EXEC sp_sqlexec @stmt
END
GO

ALTER PROCEDURE dbo.PROC_GET_COMP_PROFILE (@token varbinary(64), @input_name nvarchar(60) = NULL)
AS
	IF @input_name IS NULL
	BEGIN
		SELECT @input_name = company FROM USERS_PROFILES u
			JOIN CONF_SESSIONS s ON u.username=s.username
			WHERE s.sessionID=@token
	END

	DECLARE @sql nvarchar(200)
	
	SET @input_name = LTRIM(RTRIM(@input_name))

	SET @sql = N'USE fleet_db '
				+ N'SELECT * FROM COMP_PROFILES WHERE [name] = ''' + @input_name + ''''
	EXEC sp_sqlexec @sql
RETURN
GO



/* Procedura weryfikuj�ca podane dane uwierzytelniaj�ce.
** Je�li dane s� poprawne funkcja generuje losowy token, 
** przypisuje token do u�ytkownika w tablicy CONF_SESSIONS
** i zwraca wy�ej wspomniany token. */
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



/* Procedura usuwaj�ca rekord z tabeli CONF_SESSIONS.
** Wywo�ywana przy wylogowywaniu z aplikacji. */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_CLOSE_SESSION')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_CLOSE_SESSION AS '
	EXEC sp_sqlexec @stmt
END
GO

ALTER PROCEDURE dbo.PROC_CLOSE_SESSION (@token varbinary(64))
AS
	DELETE FROM CONF_SESSIONS WHERE sessionID = @token
GO