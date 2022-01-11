/* Tworzenie bazy danych do aplikacji do zarz¹dzania flot¹ */
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
DROP PROCEDURE PROC_GET_CAR_PROFILE
DROP PROCEDURE PROC_GET_USERS_LIST
DROP PROCEDURE PROC_GET_CARS_LIST
DROP PROCEDURE PROC_AUTHORIZE
DROP PROCEDURE PROC_CLOSE_SESSION
*/



/* Procedura weryfikuj¹ca podane dane uwierzytelniaj¹ce.
** Jeœli dane s¹ poprawne funkcja generuje losowy token, 
** przypisuje token do u¿ytkownika w tablicy CONF_SESSIONS
** i zwraca wy¿ej wspomniany token. */
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



/* Procedura usuwaj¹ca rekord z tabeli CONF_SESSIONS.
** Wywo³ywana przy wylogowywaniu z aplikacji. */
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



/* Procedura zwracaj¹ca informacje o profilu pojazdu.
** @token s³u¿y do weryfikacji sesji, jeœli podany @token
** nie znajduje siê w tabeli CONF_SESSIONS to procedura
** nie zwróci danych. Jeœli @username jest pominiêty procedura
** zwróci dane o profilu pasuj¹cym do @token
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_CAR_PROFILE')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_CAR_PROFILE AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_GET_CAR_PROFILE (@token varbinary(64), @input_username nvarchar(30) = NULL)
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END
	
	IF @input_username IS NULL
	BEGIN
		SELECT @input_username = username FROM CONF_SESSIONS WHERE sessionID=@token
	END

	DECLARE @car_plate nvarchar(7)
	SELECT @car_plate = car_plate FROM USERS_PROFILES WHERE username=@input_username

	DECLARE @sql nvarchar(200)

	SET @sql = N'USE fleet_db'
			+ N' SELECT u.car_plate, c.* FROM USERS_PROFILES u'
			+ N' JOIN CARS c ON u.car_id = c.car_id'
			+ N' WHERE u.car_plate = ''' + @car_plate + ''''
	EXEC sp_sqlexec @sql
RETURN
GO



/* Procedura zwracaj¹ca informacje o profilu warsztatu. 
** @token s³u¿y do weryfikacji sesji, jeœli podany @token
** nie znajduje siê w tabeli CONF_SESSIONS to procedura
** nie zwróci danych.
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_CAR_SERVICE')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_CAR_SERVICE AS '
	EXEC sp_sqlexec @stmt
END
GO

ALTER PROCEDURE dbo.PROC_GET_CAR_SERVICE (@token varbinary(64), @input_id int)
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @sql nvarchar(200)

	SET @sql = N'USE fleet_db '
				+ N'SELECT * FROM CAR_SERVICES WHERE car_service_id = ''' + STR(@input_id) + ''''
	EXEC sp_sqlexec @sql
RETURN
GO



/* Procedura zwracaj¹ca listê warsztatów.
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_CAR_SERVICES_LIST')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_CAR_SERVICES_LIST AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_GET_CAR_SERVICES_LIST (@token varbinary(64))
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END
	
	DECLARE @sql nvarchar(200)

	SET @sql = N'USE fleet_db '
				+ N'SELECT * '
				+ N'FROM CAR_SERVICES ORDER BY [name]'
	EXEC sp_sqlexec @sql
GO



/* Procedura zwracaj¹ca listê pojazdów z bazy danych.
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_CARS_LIST')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_CARS_LIST AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_GET_CARS_LIST (@token varbinary(64))
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @sql nvarchar(200)

	SET @sql = N'USE fleet_db '
				+ N'SELECT * '
				+ N'FROM CARS ORDER BY brand, model, prod_year '
	EXEC sp_sqlexec @sql
GO



/* Procedura zwracaj¹ca informacje o profilu firmy. 
** @token s³u¿y do weryfikacji sesji, jeœli podany @token
** nie znajduje siê w tabeli CONF_SESSIONS to procedura
** nie zwróci danych.
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
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

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



/* Procedura zwracaj¹ca listê us³ug warsztatowych.
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_SERVICES')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_SERVICES AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_GET_SERVICES (@token varbinary(64))
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @sql nvarchar(200)

	SET @sql = N'USE fleet_db '
				+ N'SELECT * '
				+ N'FROM [SERVICES] ORDER BY service_id '
	EXEC sp_sqlexec @sql
GO



/* Procedura zwracaj¹ca imiê, nazwisko, firmê i zdjêcie u¿ytkownika */
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
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END
	
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



/* Procedura zwracaj¹ca informacje o profilu u¿ytkownika. 
** @token s³u¿y do weryfikacji sesji, jeœli podany @token
** nie znajduje siê w tabeli CONF_SESSIONS to procedura
** nie zwróci danych. Jeœli @username jest pominiêty procedura
** zwróci dane o profilu pasuj¹cym do @token
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
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	IF @input_username IS NULL
	BEGIN
		SELECT @input_username = username FROM CONF_SESSIONS WHERE sessionID=@token
	END

	DECLARE @sql nvarchar(500)
	
	SET @input_username = LTRIM(RTRIM(@input_username))

	SET @sql = N'USE fleet_db '
				+ N'SELECT u.username, u.first_name, u.last_name, u.company, u.position, u.photo_url, u.phone, u.mail, c.brand, c.model '
				+ N'FROM USERS_PROFILES u '
				+ N'JOIN CARS c ON u.car_id=c.car_id '
				+ N'WHERE username = ''' + @input_username + ''''

	EXEC sp_sqlexec @sql
RETURN
GO



/* Procedura zwracaj¹ca listê u¿ytkowników z firmy,
** do której nale¿y u¿ytkownik, którego @token zosta³
** przekazany jako parametr.
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_USERS_LIST')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_USERS_LIST AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_GET_USERS_LIST (@token varbinary(64))
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @comp_name nvarchar(60)
	SELECT @comp_name = u.company FROM USERS_PROFILES u
	JOIN CONF_SESSIONS s ON u.username = s.username
	WHERE s.sessionID = @token

	DECLARE @sql nvarchar(200)

	SET @sql = N'USE fleet_db '
				+ N'SELECT first_name, last_name, position, username '
				+ N'FROM USERS_PROFILES WHERE company = ''' + @comp_name + ''''
	EXEC sp_sqlexec @sql
GO



/* Procedura wstawiaj¹ca rekord do tabeli REPAIR_HISTORY.
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_INSERT_REPAIR_HISTORY')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_INSERT_REPAIR_HISTORY AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_INSERT_REPAIR_HISTORY (@token varbinary(64), @service_id int, @car_service_id int, @date nvarchar(19))
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @sql nvarchar(200)
	DECLARE @plate nvarchar(7)

	SELECT @plate = u.car_plate FROM USERS_PROFILES u
	JOIN CONF_SESSIONS c ON u.username = c.username
	WHERE c.sessionID = @token

	SET @sql = N'USE fleet_db'
			+ N' INSERT INTO REPAIR_HISTORY (plate_number, service_id, car_service_id, [date])'
			+ N' VALUES (''' + @plate + ''''
			+ N', ''' + STR(@service_id) + ''''
			+ N', ''' + STR(@car_service_id) + ''''
			+ N', ''' + @date + ''')'
			
	EXEC sp_sqlexec @sql
	SELECT 0
RETURN
GO







