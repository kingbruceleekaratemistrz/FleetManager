/* Tworzenie bazy danych do aplikacji do zarz¹dzania flot¹ */
IF NOT EXISTS (SELECT d.[name]
				FROM sys.databases d
				WHERE	(d.database_id > 4)
				AND		(d.[name] = N'fleet_db'))
BEGIN
	CREATE DATABASE fleet_db
END
GO
/* Usuwanie tabel */
/*	
USE fleet_db
GO
DROP TABLE REPAIR_HISTORY
DROP TABLE CAR_SERVICES
DROP TABLE [SERVICES]
DROP TABLE CONF_SESSIONS
DROP TABLE USERS_PROFILES
DROP TABLE CARS
DROP TABLE COMP_PROFILES
DROP TABLE USERS_CRED
*/




/* Tabela przechowuj¹ca dane uwierzytelniaj¹ce 
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



/* Tabela przechowywuj¹ca informacje o profilach firm
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



/* Tabela przechowuj¹ca informacje o pojazdach
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



/* Tabela przechowuj¹ce informacje o profilach u¿ytkowników 
username      first_name     last_name     company      position   photo_url	  phone   mail	   car_id		plate_number
------------- -------------- ------------- ------------ ---------- ------------- ------- -------- ------------  --------------
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



/* Tabela przechowuj¹ca tokeny aktywnych sesji
username                       sessionID
------------------------------ ---------------------
*/
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



/* Tabela przechowuj¹ca
service_id  cost      time  description
----------- --------- ----- ------------
*/
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



/* Tabela przechowuj¹ca
car_service_id  address   phone		mail
--------------- --------- --------- ------------
*/
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



/* Tabela przechowuj¹ca
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
	,	[date]			datetime		NOT NULL
)
END



/*
/* Wstawienie danych do USERS_CRED */
INSERT INTO dbo.USERS_CRED (username, passwd) VALUES ('admin', 'admin')
INSERT INTO dbo.USERS_CRED (username, passwd) VALUES ('user1', 'passwd123')
INSERT INTO dbo.USERS_CRED (username, passwd) VALUES ('user2', 'Fall2021')

/* Wstawianie danych do COMP_PROFILES */
INSERT INTO dbo.COMP_PROFILES ([name], [description], [address], phone, mail) 
	VALUES ('Macrosoft', 'Software development company', 'Redmond, WA 98052-6399 USA', '425882808', 'macrosoft@outlook.com')
INSERT INTO dbo.COMP_PROFILES ([name], [description], [address], phone, mail) 
	VALUES ('Netflix', 'Movies and TV series streaming at demand.', 'Los Gatos, CA 100 Winchester Cir USA', '800112439', 'netflix@mail.com')

/* Wstawianie danych do CARS */
INSERT INTO dbo.CARS (plate_number, brand, model, prod_year, hp, cc)
	VALUES ('WA77354', 'Skoda', 'Fabia', '2017', '105', '1422')
INSERT INTO dbo.CARS (plate_number, brand, model, prod_year, hp, cc)
	VALUES ('WU12345', 'BMW', 'M3', '2015', '431', '2979')
INSERT INTO dbo.CARS (plate_number, brand, model, prod_year, hp, cc)
	VALUES ('EOP7441', 'Volkswagen', 'Passat', '2020', '190', '1984')
INSERT INTO dbo.CARS (plate_number, brand, model, prod_year, hp, cc)
	VALUES ('XA82824', 'Mercedes-Benz', 'klasa G AMG', '2017', '571', '5461')
INSERT INTO dbo.CARS (plate_number, brand, model, prod_year, hp, cc)
	VALUES ('WU11354', 'BMW-ALPINA', 'B7', '2021', '608', '4395')
INSERT INTO dbo.CARS (plate_number, brand, model, prod_year, hp, cc)
	VALUES ('XZ27441', 'Volkswagen', 'Arteon', '2020', '190', '1984')
INSERT INTO dbo.CARS (plate_number, brand, model, prod_year, hp, cc)
	VALUES ('XZZ7441', 'Volkswagen', 'Arteon', '2018', '240', '1968')


/* Wstawienie danych do USERS_PROFILES */
INSERT INTO dbo.USERS_PROFILES (username, first_name, last_name, company, position, photo_url, phone, mail, car_plate)
	VALUES ('admin', 'Admini', 'Strator', 'Macrosoft', 'CEO', 'C:\Users\pduln\Documents\GitHub\FleetManager\Assets\Profile_pictures\profile_pic_1.png', '123321123', 'admin@gmail.com', 'XA82824' )
INSERT INTO dbo.USERS_PROFILES (username, first_name, last_name, company, position, photo_url, phone, mail, car_plate)
	VALUES ('user1', 'Pan', 'Jan', 'Macrosoft', 'Janitor', 'C:\Users\pduln\Documents\GitHub\FleetManager\Assets\Profile_pictures\profile_pic_2.png', '431352643', 'PanJan@gmail.com', 'EOP7441')
INSERT INTO dbo.USERS_PROFILES (username, first_name, last_name, company, position, photo_url, phone, mail, car_plate)
	VALUES ('user2', 'Frodo', 'Baggins', 'Netflix', 'CEO', 'C:\Users\pduln\Documents\GitHub\FleetManager\Assets\Profile_pictures\profile_pic_3.png', '142511511', 'Shire@gmail.com', 'WU11354')
*/




