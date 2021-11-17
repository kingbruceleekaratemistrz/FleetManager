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
DROP TABLE CONF_SESSIONS
DROP TABLE USERS_PROFILES
DROP TABLE USERS_CRED
*/




/* Tabela przechowuj¹ca dane uwierzytelniaj¹ce 
username                       passwd
------------------------------ ------------------------------
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
	)
END
GO



/* Tabela przechowuj¹ce informacje o profilach u¿ytkowników 
username      first_name     last_name     company      photo_url
------------- -------------- ------------- ------------ -------------
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
	,	company		nvarchar(50)	NOT NULL
	,	photo_url	nvarchar(100)	NOT NULL
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



/* Wstawienie danych do USERS_CRED */
INSERT INTO dbo.USERS_CRED (username, passwd) VALUES ('admin', 'admin')
INSERT INTO dbo.USERS_CRED (username, passwd) VALUES ('user1', 'passwd123')
INSERT INTO dbo.USERS_CRED (username, passwd) VALUES ('user2', 'Fall2021')

/* Wstawienie danych do USERS_PROFILES */
INSERT INTO dbo.USERS_PROFILES (username, first_name, last_name, company, photo_url)
	VALUES ('admin', 'Admini', 'Strator', 'tak', 'C:\Users\pduln\Documents\GitHub\FleetManager\Assets\Profile_pictures\profile_pic_1.png')
INSERT INTO dbo.USERS_PROFILES (username, first_name, last_name, company, photo_url)
	VALUES ('user1', 'Pan', 'Jan', 'Macrosoft', 'C:\Users\pduln\Documents\GitHub\FleetManager\Assets\Profile_pictures\profile_pic_2.png')
INSERT INTO dbo.USERS_PROFILES (username, first_name, last_name, company, photo_url)
	VALUES ('user2', 'Frodo', 'Baggins', 'Netflix', 'C:\Users\pduln\Documents\GitHub\FleetManager\Assets\Profile_pictures\profile_pic_3.png')


