/* Tworzenie bazy danych do aplikacji do zarządzania flotą */
IF NOT EXISTS (SELECT d.[name]
				FROM sys.databases d
				WHERE	(d.database_id > 4)
				AND		(d.[name] = N'fleet_db'))
BEGIN
	CREATE DATABASE fleet_db
END
GO
/* Tabela przechowująca dane uwierzytelniające 
username                       passwd						  acc
------------------------------ ------------------------------ ------
*/
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
	,	acc			int				NOT NULL
	)
END
GO
/* Tabela przechowywująca informacje o profilach firm
name       description      address     phone     mail
---------- ---------------- ----------- --------- --------
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'COMP_PROFILES')
				AND		(OBJECTPROPERTY(o.[ID], N'IsUserTable') = 1))
BEGIN
	CREATE TABLE dbo.COMP_PROFILES
	(	[name]			nvarchar(60)	NOT NULL CONSTRAINT PK_COMP_PROFILES PRIMARY KEY
	,	[description]	nvarchar(300)	NOT NULL
	,	[address]		nvarchar(100)	NOT NULL
	,	phone			nvarchar(9)		NOT NULL
	,	mail			nvarchar(100)	NOT NULL
)
END
/* Tabela przechowująca informacje o pojazdach
car_id	brand	model	prod_year	hp		cc		  photo_url
-------	------- ------- -----------	------- --------- -----------
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'CARS')
				AND		(OBJECTPROPERTY(o.[ID], N'IsUserTable') = 1))
BEGIN
	CREATE TABLE dbo.CARS
	(	car_id			int				IDENTITY CONSTRAINT PK_CARS PRIMARY KEY
	,	brand			nvarchar(50)	NOT NULL
	,	model			nvarchar(50)	NOT NULL
	,	prod_year		nvarchar(4)		NOT NULL
	,	hp				int				NOT NULL
	,	cc				int				NOT NULL
	,	photo_url		nvarchar(100)	NOT NULL
)
END
/* Tabela przechowujące informacje o profilach użytkowników 
username      first_name     last_name     company      position   photo_url	 
------------- -------------- ------------- ------------ ---------- ------------- 
phone   mail	 car_id		   plate_number
------- -------- ------------  --------------
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'USERS_PROFILES')
				AND		(OBJECTPROPERTY(o.[ID], N'IsUserTable') = 1))
BEGIN
	CREATE TABLE dbo.USERS_PROFILES
	(	username	nvarchar(30)	NOT NULL CONSTRAINT FK_USERS_PROFILES__USERS_CRED 
									FOREIGN KEY REFERENCES dbo.USERS_CRED(username)
	,	first_name	nvarchar(30)	NOT NULL
	,	last_name	nvarchar(50)	NOT NULL
	,	company		nvarchar(60)	NOT NULL CONSTRAINT FK_USERS_PROFILES__COMP_PROFILES
									FOREIGN KEY REFERENCES dbo.COMP_PROFILES([name])
	,	position	nvarchar(50)	NOT NULL
	,	photo_url	nvarchar(100)	NOT NULL
	,	phone		nvarchar(9)		NOT NULL
	,	mail		nvarchar(100)	NOT NULL
	,	car_id		int				NOT NULL CONSTRAINT FK_USERS_PROFILES__CARS
									FOREIGN KEY REFERENCES dbo.CARS(car_id)
	,	car_plate	nvarchar(7)		NOT NULL CONSTRAINT PK_USERS_PROFILES PRIMARY KEY
)
END



/* Tabela przechowująca tokeny aktywnych sesji
username                       sessionID
------------------------------ --------------------- */
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
/* Tabela przechowująca
service_id  cost      time  description
----------- --------- ----- ----------- */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'SERVICES')
				AND		(OBJECTPROPERTY(o.[ID], N'IsUserTable') = 1))
BEGIN
	CREATE TABLE dbo.[SERVICES]
	(	service_id		int		IDENTITY CONSTRAINT PK_SERVICES PRIMARY KEY
	,	cost			int		NOT NULL
	,	[time]			nvarchar(10)	NOT NULL
	,	[description]	nvarchar(300)	NOT NULL
)
END
/* Tabela przechowująca
car_service_id  address   phone		mail
--------------- --------- --------- ------------ */
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'CAR_SERVICES')
				AND		(OBJECTPROPERTY(o.[ID], N'IsUserTable') = 1))
BEGIN
	CREATE TABLE dbo.CAR_SERVICES
	(	car_service_id		int				IDENTITY CONSTRAINT PK_CAR_SERVICES PRIMARY KEY
	,	name				nvarchar(50)	NOT NULL
	,	[address]			nvarchar(100)	NOT NULL
	,	phone				nvarchar(9)		NOT NULL
	,	mail				nvarchar(100)	NOT NULL
)
END



/* Tabela przechowująca
id			car_id    service_id   car_service_id    date  
----------- --------- ------------ ----------------- -------
*/
USE fleet_db
GO
IF NOT EXISTS (SELECT 1
				FROM sysobjects o (NOLOCK)
				WHERE	(o.[name] = N'REPAIR_HISTORY')
				AND		(OBJECTPROPERTY(o.[ID], N'IsUserTable') = 1))
BEGIN
	CREATE TABLE dbo.REPAIR_HISTORY
	(	id				int				IDENTITY
	,	plate_number	nvarchar(7)		NOT NULL CONSTRAINT FK_REPAIR_HISTORY__USER_PROFILES
										FOREIGN KEY REFERENCES dbo.USERS_PROFILES(car_plate)
	,	service_id		int				NOT NULL CONSTRAINT FK_REPAIR_HISTORY__SERVICES
										FOREIGN KEY REFERENCES dbo.[SERVICES](service_id)
	,	car_service_id	int				NOT NULL CONSTRAINT FK_REPAIR_HISTORY__CAR_SERVICES
										FOREIGN KEY REFERENCES dbo.CAR_SERVICES(car_service_id)
	,	[date]			datetime		
)
END

/* 
INSERT INTO dbo.USERS_CRED (username, passwd, acc) VALUES ('dulnikip', '123', 2)
INSERT INTO dbo.COMP_PROFILES (name, [description], [address], phone, mail) VALUES ('Politechnika Warszawska', 'Jesteśmy jedną z największych i najlepszych uczelni w Polsce. Od lat kształcimy kolejne pokolenia inżynierów i nie tylko inżynierów oraz prowadzimy ważne badania, głównie z obszaru nauk technicznych.', 'Plac Politechniki 1
00-661 Warszawa', '222347211', 'info@pw.edu.pl')
INSERT INTO dbo.USERS_PROFILES (username, first_name, last_name, company, position, photo_url, phone, mail, car_id, car_plate) VALUES ('dulnikip', 'Patryk', 'Dulnikiewicz', 'Politechnika Warszawska', 'Student','..\..\..\Assets\Profile_pictures\profile_pic_0.png', '695865558', '01153036@pw.edu.pl', 23, 'WA4S701')
*/



