/* Tworzenie bazy danych do aplikacji do zarz?dzania flot? */
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



/* Procedura weryfikuj?ca podane dane uwierzytelniaj?ce.
** Je?li dane s? poprawne funkcja generuje losowy token, 
** przypisuje token do u?ytkownika w tablicy CONF_SESSIONS
** i zwraca wy?ej wspomniany token. */
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



/* Procedura usuwaj?ca rekord z tabeli CONF_SESSIONS.
** Wywo?ywana przy wylogowywaniu z aplikacji. */
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



/* Procedura usuwaj?cja rekord z podanej tabeli */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_DELETE_USERS_CRED')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_DELETE_USERS_CRED AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_DELETE_USERS_CRED (@token varbinary(64), @username nvarchar(30))
AS
	DELETE FROM USERS_CRED WHERE username = @username
GO



/* Procedura zwracaj?ca poziom dost?pu u?ytkownika,
** do kt?rego przypisany jest @token, przes?any jako
** parametr. Zwraca:
** -1 - podany @token nie znajduje sie w CONF_SESSIONS
** 0 - podstawowy poziom dost?pu
** 1 - poziom dost?pu administratora firmy
** 2- poziom dost?pu administratora aplikacji */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_ACC')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_ACC AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_GET_ACC @token varbinary(64)
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	SELECT u.acc FROM USERS_CRED u
	JOIN CONF_SESSIONS c ON u.username=c.username
	WHERE c.sessionID=@token
RETURN
GO



/* Procedura zwracaj?ca informacje o profilu pojazdu.
** @token s?u?y do weryfikacji sesji, je?li podany @token
** nie znajduje si? w tabeli CONF_SESSIONS to procedura
** nie zwr?ci danych. Je?li @username jest pomini?ty procedura
** zwr?ci dane o profilu pasuj?cym do @token */
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



/* Procedura zwracaj?ca informacje o profilu warsztatu. 
** @token s?u?y do weryfikacji sesji, je?li podany @token
** nie znajduje si? w tabeli CONF_SESSIONS to procedura
** nie zwr?ci danych. */
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



/* Procedura zwracaj?ca list? warsztat?w. */
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



/* Procedura zwracaj?ca list? pojazd?w z bazy danych. */
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


/* Procedura zwracaj?ca list? firm. */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_COMPANIES_LIST')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_COMPANIES_LIST AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_GET_COMPANIES_LIST (@token varbinary(64))
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @sql nvarchar(200)

	SET @sql = N'USE fleet_db '
				+ N'SELECT name '
				+ N'FROM COMP_PROFILES '
	EXEC sp_sqlexec @sql
GO



/* Procedura zwracaj?ca informacje o profilu firmy. 
** @token s?u?y do weryfikacji sesji, je?li podany @token
** nie znajduje si? w tabeli CONF_SESSIONS to procedura
** nie zwr?ci danych. */
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



/* Procedura zwracaj?ca list? u?ytkownik?w z firmy,
** do kt?rej nale?y u?ytkownik, kt?rego @token zosta?
** przekazany jako parametr. */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_COWORKERS_LIST')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_COWORKERS_LIST AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_GET_COWORKERS_LIST (@token varbinary(64))
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



/* Procedura zwracaj?ca list? napraw u?ytkownika zwi?zanego z podanum tokenem */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_GET_REPAIR_HISTORY_LIST')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_GET_REPAIR_HISTORY_LIST AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_GET_REPAIR_HISTORY_LIST (@token varbinary(64))
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @sql nvarchar(1000)

	SET @sql = N'USE fleet_db '
			+ N' SELECT s.[description], c.[name], r.[date], s.cost, s.[time] FROM REPAIR_HISTORY r'
			+ N' JOIN [SERVICES] s ON r.service_id = s.service_id'
			+ N' JOIN CAR_SERVICES c ON r.car_service_id = c.car_service_id'
			+ N' JOIN USERS_PROFILES u ON r.plate_number = u.car_plate'
			+ N' JOIN CONF_SESSIONS cs ON u.username = cs.username'
			+ N' WHERE cs.sessionID = CONVERT(varbinary(64),''' + CONVERT(nvarchar(128), @token, 2) + ''', 2)'
			+ N' ORDER BY r.[date]'			
	EXEC sp_sqlexec @sql
GO



/* Procedura zwracaj?ca list? us?ug warsztatowych. */
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



/* Procedura zwracaj?ca imi?, nazwisko, firm? i zdj?cie u?ytkownika */
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



/* Procedura zwracaj?ca informacje o profilu u?ytkownika. 
** @token s?u?y do weryfikacji sesji, je?li podany @token
** nie znajduje si? w tabeli CONF_SESSIONS to procedura
** nie zwr?ci danych. Je?li @username jest pomini?ty procedura
** zwr?ci dane o profilu pasuj?cym do @token */
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



/* Procedura zwracaj?ca list? u?ytkownik?w. */
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

	DECLARE @sql nvarchar(200)

	SET @sql = N'USE fleet_db '
				+ N'SELECT first_name, last_name, company, username '
				+ N'FROM USERS_PROFILES '
	EXEC sp_sqlexec @sql
GO


/* Procedura dodaj?ca rekord do CARS */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_INSERT_CARS')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_INSERT_CARS AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_INSERT_CARS (@token varbinary(64), @brand nvarchar(50), @model nvarchar(50), @prod_year nvarchar(4), @hp int, @cc int, @photo_url nvarchar(100))
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @sql nvarchar(400)

	SET @sql = N'USE fleet_db'
			+ N' INSERT INTO CARS (brand, model, prod_year, hp, cc, photo_url)'
			+ N' VALUES (''' + @brand + ''''
			+ N', ''' + @model + ''''
			+ N', ''' + @prod_year + ''''
			+ N', ''' + STR(@hp) + ''''
			+ N', ''' + STR(@cc) + ''''
			+ N', ''' + @photo_url + ''')'
			
	EXEC sp_sqlexec @sql
	SELECT 0
RETURN
GO



/* Procedura dodaj?ca rekord do CAR_SERVICES */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_INSERT_CAR_SERVICES')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_INSERT_CAR_SERVICES AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_INSERT_CAR_SERVICES (@token varbinary(64), @name nvarchar(50), @address nvarchar(100), @phone nvarchar(9), @mail nvarchar(100))
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @sql nvarchar(500)

	SET @sql = N'USE fleet_db'
			+ N' INSERT INTO CAR_SERVICES ([name], [address], phone, mail)'
			+ N' VALUES (''' + @name + ''''
			+ N', ''' + @address + ''''
			+ N', ''' + @phone + ''''
			+ N', ''' + @mail + ''')'
			
	EXEC sp_sqlexec @sql
	SELECT 0
RETURN
GO



/* Procedura dodaj?ca rekord do COMP_PROFILES */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_INSERT_COMP_PROFILES')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_INSERT_COMP_PROFILES AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_INSERT_COMP_PROFILES (@token varbinary(64), @name nvarchar(60), @description nvarchar(300), @address nvarchar(100), @phone nvarchar(9), @mail nvarchar(100))
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @sql nvarchar(800)

	SET @sql = N'USE fleet_db'
			+ N' INSERT INTO COMP_PROFILES ([name], description, [address], phone, mail)'
			+ N' VALUES (''' + @name + ''''
			+ N', ''' + @description + ''''
			+ N', ''' + @address + ''''
			+ N', ''' + @phone + ''''
			+ N', ''' + @mail + ''')'
			
	EXEC sp_sqlexec @sql
	SELECT 0
RETURN
GO



/* Procedura dodaj?ca rekord do SERVICES */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_INSERT_SERVICES')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_INSERT_SERVICES AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_INSERT_SERVICES (@token varbinary(64), @cost int, @time nvarchar(10), @description nvarchar(300))
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @sql nvarchar(700)

	SET @sql = N'USE fleet_db'
			+ N' INSERT INTO SERVICES (cost, [time], description)'
			+ N' VALUES (''' + STR(@cost) + ''''
			+ N', ''' + @time + ''''
			+ N', ''' + @description + ''')'
			
	EXEC sp_sqlexec @sql
	SELECT 0
RETURN
GO



/* Procedura wstawiaj?ca rekord do tabeli REPAIR_HISTORY. */
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



/* Proceura wstawiaj?ca rekord do tabeli USERS_CRED */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_INSERT_USERS_CRED')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_INSERT_USERS_CRED AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_INSERT_USERS_CRED (@token varbinary(64), @username nvarchar(30), @password nvarchar(30), @acc int)
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @sql nvarchar(200)
	SET @sql = N'USE fleet_db'
			+ N' INSERT INTO USERS_CRED (username, passwd, acc)'
			+ N' VALUES (''' + @username + ''''
			+ N', ''' + @password + ''''
			+ N', ''' + STR(@acc) + ''')'
			
	EXEC sp_sqlexec @sql
	SELECT 0
RETURN
GO


/* Proceura wstawiaj?ca rekord do tabeli USERS_PROFILES */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_INSERT_USERS_PROFILES')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_INSERT_USERS_PROFILES AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_INSERT_USERS_PROFILES (@token varbinary(64), @username nvarchar(30), @first_name nvarchar(30), @last_name nvarchar(50), @company nvarchar(60), @position nvarchar(50), @photo_url nvarchar(100), @phone nvarchar(9), @mail nvarchar(100), @car_id int, @car_plate nvarchar(7))
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	DECLARE @sql nvarchar(500)
	SET @sql = N'USE fleet_db'
			+ N' INSERT INTO USERS_PROFILES (username, first_name, last_name, company, position, photo_url, phone, mail, car_id, car_plate)'
			+ N' VALUES (''' + @username + ''''
			+ N', ''' + @first_name + ''''
			+ N', ''' + @last_name + ''''
			+ N', ''' + @company + ''''
			+ N', ''' + @position + ''''
			+ N', ''' + @photo_url + ''''
			+ N', ''' + @phone + ''''
			+ N', ''' + @mail + ''''
			+ N', ''' + STR(@car_id) + ''''
			+ N', ''' + @car_plate + ''')'
			
	EXEC sp_sqlexec @sql
	SELECT 0
RETURN
GO



/* Procedura aktualizuj?ca rekord w tabeli CAR_SERVICES
** Je?li @input_comp jest pomini?ty, zostanie przypisany companu powi?zany z @token.
** address = @new_address
** phone = @new_phone
** mail = @new_mail
** Ulegn? zmianie tylko te warto?ci, dla kt?rych b?dzie przekazany odpowiedni parametr przy wywo?aniu procedury. */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_UPDATE_CAR_SERVICES')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_UPDATE_CAR_SERVICES AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_UPDATE_CAR_SERVICES (@token varbinary(64), @new_address nvarchar(100) = NULL, @new_phone nvarchar(9) = NULL, @new_mail nvarchar(100) = NULL, @input_car_service nvarchar(50) = NULL)
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	IF @new_address IS NULL
	BEGIN
		SELECT @new_address = [address] FROM CAR_SERVICES
		WHERE name = @input_car_service
	END

	IF @new_phone IS NULL
	BEGIN
		SELECT @new_phone = phone FROM CAR_SERVICES
		WHERE name = @input_car_service
	END

	IF @new_mail IS NULL
	BEGIN
		SELECT @new_mail = mail FROM CAR_SERVICES
		WHERE name = @input_car_service
	END


	DECLARE @sql nvarchar(700)

	SET @sql = N'USE fleet_db'
			+ N' UPDATE CAR_SERVICES'
			+ N' SET [address] = ''' + @new_address + ''','
			+ N' phone = ''' + @new_phone + ''','
			+ N' mail = ''' + @new_mail + ''''
			+ N' WHERE name = ''' + @input_car_service + ''''
			
	EXEC sp_sqlexec @sql
	SELECT 0
RETURN
GO



/* Procedura aktualizuj?ca rekord w tabeli COMP_PROFILES
** Je?li @input_comp jest pomini?ty, zostanie przypisany companu powi?zany z @token.
** description = @new_description
** address = @new_address
** phone = @new_phone
** mail = @new_mail
** Ulegn? zmianie tylko te warto?ci, dla kt?rych b?dzie przekazany odpowiedni parametr przy wywo?aniu procedury. */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_UPDATE_COMP_PROFILES')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_UPDATE_COMP_PROFILES AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_UPDATE_COMP_PROFILES (@token varbinary(64), @new_description nvarchar(300) = NULL, @new_address nvarchar(100) = NULL, @new_phone nvarchar(9) = NULL, @new_mail nvarchar(100) = NULL, @input_comp nvarchar(30) = NULL)
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	IF @input_comp IS NULL
	BEGIN
		SELECT @input_comp = u.company FROM USERS_PROFILES u
		JOIN CONF_SESSIONS s ON u.username = s.username
		WHERE sessionID=@token
	END

	IF @new_description IS NULL
	BEGIN
		SELECT @new_description = [description] FROM COMP_PROFILES
		WHERE name = @input_comp
	END

	IF @new_address IS NULL
	BEGIN
		SELECT @new_address = [address] FROM COMP_PROFILES
		WHERE name = @input_comp
	END

	IF @new_phone IS NULL
	BEGIN
		SELECT @new_phone = phone FROM COMP_PROFILES
		WHERE name = @input_comp
	END

	IF @new_mail IS NULL
	BEGIN
		SELECT @new_mail = mail FROM COMP_PROFILES
		WHERE name = @input_comp
	END


	DECLARE @sql nvarchar(700)

	SET @sql = N'USE fleet_db'
			+ N' UPDATE COMP_PROFILES'
			+ N' SET [description] = ''' + @new_description + ''','
			+ N' [address] = ''' + @new_address + ''','
			+ N' phone = ''' + @new_phone + ''','
			+ N' mail = ''' + @new_mail + ''''
			+ N' WHERE name = ''' + @input_comp + ''''
			
	EXEC sp_sqlexec @sql
	SELECT 0
RETURN
GO



/* Procedura aktualizuj?ca rekord w tabeli USERS_CRED.
** Je?li @input_username jest pomini?ty, zostanie przypisany username powi?zany z @token.
** passwd zostanie zmienione na @new_passwd dla u?ytkownika @input_username. */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_UPDATE_USERS_CRED')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_UPDATE_USERS_CRED AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_UPDATE_USERS_CRED (@token varbinary(64), @new_passwd nvarchar(30), @input_username nvarchar(30) = NULL)
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	IF @input_username IS NULL
	BEGIN
		SELECT @input_username = username FROM CONF_SESSIONS
		WHERE sessionID=@token
	END

	DECLARE @sql nvarchar(200)

	SET @sql = N'USE fleet_db'
			+ N' UPDATE USERS_CRED'
			+ N' SET passwd = ''' + @new_passwd + ''''
			+ N' WHERE username = ''' + @input_username + ''''
			
	EXEC sp_sqlexec @sql
	SELECT 0
RETURN
GO



/* Procedura aktualizuj?ca rekord w tabeli USERS_PROFILES
** Je?li @input_username jest pomini?ty, zostanie przypisany username powi?zany z @token.
** first_name = @new_first_name
** last_name = @new_last_name
** position = @new_position
** photo_url = @new_photo_url
** phone = @new_phone
** mail = @new_mail
** Ulegn? zmianie tylko te warto?ci, dla kt?rych b?dzie przekazany odpowiedni parametr przy wywo?aniu procedury. */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'PROC_UPDATE_USERS_PROFILES')
				AND		(OBJECTPROPERTY(o.[ID], N'IsProcedure') = 1))
BEGIN
	DECLARE @stmt nvarchar(100)
	SET @stmt = 'CREATE PROCEDURE dbo.PROC_UPDATE_USERS_PROFILES AS '
	EXEC sp_sqlexec @stmt
END
GO
ALTER PROCEDURE dbo.PROC_UPDATE_USERS_PROFILES (@token varbinary(64), @new_first_name nvarchar(30) = NULL, @new_last_name nvarchar(50) = NULL, @new_position nvarchar(50) = NULL, @new_photo_url nvarchar(100) = NULL, @new_phone nvarchar(9) = NULL, @new_mail nvarchar(100) = NULL, @input_username nvarchar(30) = NULL)
AS
	IF NOT EXISTS (SELECT 1 FROM CONF_SESSIONS WHERE sessionID=@token)
	BEGIN
		SELECT -1
		RETURN
	END

	IF @input_username IS NULL
	BEGIN
		SELECT @input_username = username FROM CONF_SESSIONS
		WHERE sessionID=@token
	END

	IF @new_first_name IS NULL
	BEGIN
		SELECT @new_first_name = first_name FROM USERS_PROFILES
		WHERE username = @input_username
	END

	IF @new_last_name IS NULL
	BEGIN
		SELECT @new_last_name = last_name FROM USERS_PROFILES
		WHERE username = @input_username
	END

	IF @new_position IS NULL
	BEGIN
		SELECT @new_position = position FROM USERS_PROFILES
		WHERE username = @input_username
	END

	IF @new_photo_url IS NULL
	BEGIN
		SELECT @new_photo_url = photo_url FROM USERS_PROFILES
		WHERE username = @input_username
	END

	IF @new_phone IS NULL
	BEGIN
		SELECT @new_phone = phone FROM USERS_PROFILES
		WHERE username = @input_username
	END

	IF @new_mail IS NULL
	BEGIN
		SELECT @new_mail = mail FROM USERS_PROFILES
		WHERE username = @input_username
	END

	DECLARE @sql nvarchar(700)

	SET @sql = N'USE fleet_db'
			+ N' UPDATE USERS_PROFILES'
			+ N' SET first_name = ''' + @new_first_name + ''','
			+ N' last_name = ''' + @new_last_name + ''','
			+ N' position = ''' + @new_position + ''','
			+ N' photo_url = ''' + @new_photo_url + ''','
			+ N' phone = ''' + @new_phone + ''','
			+ N' mail = ''' + @new_mail + ''''
			+ N' WHERE username = ''' + @input_username + ''''
			
	EXEC sp_sqlexec @sql
	SELECT 0
RETURN
GO

