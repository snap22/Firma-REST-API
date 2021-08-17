USE RestDB;

GO
CREATE TABLE help_title
(
	Id INT PRIMARY KEY IDENTITY,
	Title VARCHAR(5)
);

GO
INSERT INTO help_title(Title) VALUES('Bc.');
INSERT INTO help_title(Title) VALUES('Mgr.');
INSERT INTO help_title(Title) VALUES('Ing.');
INSERT INTO help_title(Title) VALUES('MUDr.');
INSERT INTO help_title(Title) VALUES('MDDr.');
INSERT INTO help_title(Title) VALUES('PhD.');
INSERT INTO help_title(Title) VALUES('ArtD.');
INSERT INTO help_title(Title) VALUES('ThDr.');

GO
CREATE OR ALTER PROCEDURE help_create_employee  
	(
	@par_first_name varchar(20),
	@par_last_name varchar(20),
	@par_company_id INT = NULL
	)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @new_email varchar(50);
	DECLARE @new_title varchar(5);
	DECLARE @new_contact varchar(15);

	SET @new_email = TRIM(@par_first_name) + '.' + TRIM(@par_last_name) + '@Email.sk';
	SELECT TOP 1 @new_title = Title FROM help_title ORDER BY NEWID();
	SELECT TOP 1 @new_contact =  '+4219' + CAST(CAST(RAND() * 100000000 AS INT) AS VARCHAR ) FROM help_title;

	INSERT INTO Employee (FirstName, LastName, Email, Title, Contact, CompanyId)
		VALUES (TRIM(@par_first_name), TRIM(@par_last_name), @new_email,  @new_title, @new_contact, @par_company_id);
END;

--	***	RESET INDEXOV ***
GO
--DBCC CHECKIDENT ('Company', RESEED, 0);
--DBCC CHECKIDENT ('Employee', RESEED, 0);
--DBCC CHECKIDENT ('Division', RESEED, 0);
--DBCC CHECKIDENT ('Project', RESEED, 0);
--DBCC CHECKIDENT ('Department', RESEED, 0);
GO

-- FIRMA 1 - EvilWorks

EXEC RestDB.dbo.help_create_employee 'Carmel', 'Parks';	-- 1
GO

INSERT INTO Company (Title, Code, Director)
	VALUES ( 'EvilWorks', 'AAA1', 1);	-- 1

-- FIRMA 1 - ZAMESTNANCI (Id 2 až 9)

EXEC RestDB.dbo.help_create_employee 'Lily', 'Conroy', 1;
EXEC RestDB.dbo.help_create_employee 'Becky ', 'Dunn', 1;
EXEC RestDB.dbo.help_create_employee 'Dedalus', 'Clearwater', 1;
EXEC RestDB.dbo.help_create_employee 'Jimmy', 'Jigger', 1;
EXEC RestDB.dbo.help_create_employee 'Albus', 'Diggory', 1;
EXEC RestDB.dbo.help_create_employee 'Jean', 'Rosier', 1;
EXEC RestDB.dbo.help_create_employee 'Jimmy', 'Pomfrey', 1;
EXEC RestDB.dbo.help_create_employee 'Daisy', 'Bagnold', 1;	-- 9

-- FIRMA 1 - DIVIZIE
INSERT INTO Division (Title, Code, CompanyId, Leader)
	VALUES('The Evil Division', 'EWA1', 1, 3);	-- 1

-- FIRMA 1 - PROJEKTY
INSERT INTO Project (Title, Code, DivisionId, Leader)
	VALUES('First evil Project', 'EWP1', 1, 4);	-- 1
INSERT INTO Project (Title, Code, DivisionId, Leader)
	VALUES('Last evil Project', 'EWP2', 1, 5);	-- 2

	
-- FIRMA 1 - ODDELENIA
INSERT INTO Department(Title, Code, ProjectId, Leader)
	VALUES('Evil depo', 'EWD1', 1, 7);
INSERT INTO Department(Title, Code, ProjectId, Leader)
	VALUES('Bad depo', 'EWD2', 1, 8);
INSERT INTO Department(Title, Code, ProjectId, Leader)
	VALUES('Worst depo', 'EWD3', 2, 9);


-- FIRMA 2 - Goodify
EXEC RestDB.dbo.help_create_employee 'Ema', 'Dolan';	-- 10

INSERT INTO Company (Title, Code, Director)
	VALUES ( 'Goodify', 'AAA2', 10);	-- 2

-- FIRMA 2 - ZAMESTNANCI (Id 11 až 15)
EXEC RestDB.dbo.help_create_employee 'Brandan', 'Bender', 2;
EXEC RestDB.dbo.help_create_employee 'Vicky', 'Dunn', 2;
EXEC RestDB.dbo.help_create_employee 'Jacky', 'Dunn', 2;
EXEC RestDB.dbo.help_create_employee 'Harrison', 'Jones', 2;
EXEC RestDB.dbo.help_create_employee 'Merlin', 'Matin', 2;

-- FIRMA 2 - DIVIZIE
INSERT INTO Division (Title, Code, CompanyId, Leader)
	VALUES('The Good Div', 'GFD1', 2, 11);	-- 2
INSERT INTO Division (Title, Code, CompanyId, Leader)
	VALUES('The Better Div', 'GFD2', 2, 12);	-- 3

-- FIRMA 2 - PROJEKTY
INSERT INTO Project (Title, Code, DivisionId, Leader)
	VALUES('First good Project', 'GFP1', 3, 13);	-- 3

	
-- FIRMA 2 - ODDELENIA
INSERT INTO Department(Title, Code, ProjectId, Leader)
	VALUES('Good depo', 'GFD1', 3, 14);
INSERT INTO Department(Title, Code, ProjectId, Leader)
	VALUES('Better depo', 'GFD2', 3, 15);


-- NEZAMESTNANI (Id 16 až 20)
EXEC RestDB.dbo.help_create_employee 'Henry', 'Porter';
EXEC RestDB.dbo.help_create_employee 'Donald', 'Beasley';
EXEC RestDB.dbo.help_create_employee 'Veronica', 'Ginger';
EXEC RestDB.dbo.help_create_employee 'Rufus', 'Bagrid';
EXEC RestDB.dbo.help_create_employee 'Darius', 'Bigheaded';
GO

-- vymazanie pomocnych objektov
DROP PROCEDURE help_create_employee;
DROP TABLE RestDB.dbo.help_title;

