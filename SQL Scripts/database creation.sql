

CREATE DATABASE RestDB;

GO
USE RestDB;

GO
CREATE TABLE RestDB.dbo.Company
(
	Id INT PRIMARY KEY IDENTITY,
	Title VARCHAR(20) NOT NULL,
	Code CHAR(4) UNIQUE NOT NULL,
	Director INT NOT NULL,
);

GO
CREATE TABLE RestDB.dbo.Employee
(
	Id INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(20) NOT NULL,
	Title VARCHAR(10) NULL,
	Email VARCHAR(50) UNIQUE NOT NULL,
	Contact VARCHAR(15) NOT NULL,	
	CompanyId INT NULL,
	CONSTRAINT FK_Employee_CompanyId FOREIGN KEY (CompanyId) REFERENCES RestDB.dbo.Company(Id)
);

GO
ALTER TABLE RestDB.dbo.Company
	ADD CONSTRAINT FK_Company_Director FOREIGN KEY (Director) REFERENCES RestDB.dbo.Employee(Id);

GO
CREATE TABLE RestDB.dbo.Division
(
	Id INT PRIMARY KEY IDENTITY,
	Title VARCHAR(20) NOT NULL,
	Code CHAR(4) UNIQUE NOT NULL,
	Leader INT NOT NULL,
	CompanyId INT NOT NULL,
	CONSTRAINT FK_Division_Leader FOREIGN KEY (Leader) REFERENCES RestDB.dbo.Employee(Id),
	CONSTRAINT FK_Division_CompanyId FOREIGN KEY (CompanyId) REFERENCES RestDB.dbo.Company(Id)
);

GO
CREATE TABLE RestDB.dbo.Project
(
	Id INT PRIMARY KEY IDENTITY,
	Title VARCHAR(20) NOT NULL,
	Code CHAR(4) UNIQUE NOT NULL,
	Leader INT NOT NULL,
	DivisionId INT NOT NULL,
	CONSTRAINT FK_Project_Leader FOREIGN KEY (Leader) REFERENCES RestDB.dbo.Employee(Id),
	CONSTRAINT FK_Project_DivisionId FOREIGN KEY (DivisionId) REFERENCES RestDB.dbo.Division(Id)
);

GO
CREATE TABLE RestDB.dbo.Department
(
	Id INT PRIMARY KEY IDENTITY,
	Title VARCHAR(20) NOT NULL,
	Code CHAR(4) UNIQUE NOT NULL,
	Leader INT NOT NULL,
	ProjectId INT NOT NULL,
	CONSTRAINT FK_Department_Leader FOREIGN KEY (Leader) REFERENCES RestDB.dbo.Employee(Id),
	CONSTRAINT FK_Department_ProjectId FOREIGN KEY (ProjectId) REFERENCES RestDB.dbo.Project(Id)
);




--	DIVIZIA

GO
CREATE OR ALTER TRIGGER TR_div_check_leader 
ON RestDB.dbo.Division
INSTEAD OF INSERT, UPDATE
AS
BEGIN
	IF EXISTS (
		SELECT 'x' 
			FROM RestDB.dbo.Employee
			JOIN inserted ON (RestDB.dbo.Employee.Id = inserted.Leader)
			WHERE (RestDB.dbo.Employee.CompanyId != inserted.CompanyId) OR (RestDB.dbo.Employee.CompanyId IS NULL)
	)
		BEGIN
			RAISERROR('Employee is from a different Company.', 11, 1);
			ROLLBACK TRANSACTION;
		END;
	ELSE
		BEGIN
			IF EXISTS(SELECT 'x' FROM deleted)	-- update
				BEGIN
					UPDATE RestDB.dbo.Division 
						SET 
							Title = inserted.Title,
							Code = inserted.Code,
							Leader = inserted.Leader,
							CompanyId = inserted.CompanyId
							FROM inserted
					WHERE (RestDB.dbo.Division.Id IN (SELECT Id FROM deleted));
				END;
			ELSE	-- insert
				BEGIN
					INSERT INTO RestDB.dbo.Division(Title, Code, Leader, CompanyId)
						SELECT Title, Code, Leader, CompanyId FROM inserted;

					SELECT Id FROM RestDB.dbo.Division WHERE @@ROWCOUNT > 0 AND Id = scope_identity();
				END;
		END;
END;

GO
CREATE OR ALTER TRIGGER TR_div_delete
ON RestDB.dbo.Division
INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM RestDB.dbo.Project
		WHERE DivisionId IN (SELECT Id FROM deleted);

	DELETE FROM RestDB.dbo.Division
		WHERE Id IN (SELECT Id FROM deleted);
END;

--	PROJEKT
GO
CREATE OR ALTER TRIGGER TR_pro_check_leader 
ON RestDB.dbo.Project
INSTEAD OF INSERT, UPDATE
AS
BEGIN
	IF EXISTS (
		SELECT 'x' 
			FROM RestDB.dbo.Employee
			JOIN inserted ON (RestDB.dbo.Employee.Id = inserted.Leader)
			WHERE (RestDB.dbo.Employee.CompanyId != (SELECT CompanyId FROM RestDB.dbo.Division WHERE Id = inserted.DivisionId)) 
				OR (RestDB.dbo.Employee.CompanyId IS NULL)
	)
		BEGIN
			RAISERROR('Employee is from a different Company.', 11, 1);
			ROLLBACK TRANSACTION;
		END;
	ELSE
		BEGIN
			IF EXISTS(SELECT 'x' FROM deleted)	-- update
				BEGIN
					UPDATE RestDB.dbo.Project 
						SET 
							Title = inserted.Title,
							Code = inserted.Code,
							Leader = inserted.Leader,
							DivisionId = inserted.DivisionId
							FROM inserted
					WHERE (RestDB.dbo.Project.Id IN (SELECT Id FROM deleted));
				END;
			ELSE	-- insert
				BEGIN
					INSERT INTO RestDB.dbo.Project(Title, Code, Leader, DivisionId)
						SELECT Title, Code, Leader, DivisionId FROM inserted;

					SELECT Id FROM RestDB.dbo.Project WHERE @@ROWCOUNT > 0 AND Id = scope_identity();
				END;
		END;
END;

GO
CREATE OR ALTER TRIGGER TR_pro_delete
ON RestDB.dbo.Project
INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM RestDB.dbo.Department
		WHERE ProjectId IN (SELECT Id FROM deleted);
	DELETE FROM RestDB.dbo.Project
		WHERE Id IN (SELECT Id FROM deleted);
END;

--	ODDELENIE
GO
CREATE OR ALTER TRIGGER TR_dep_check_leader 
ON RestDB.dbo.Department
INSTEAD OF INSERT, UPDATE
AS
BEGIN
	IF EXISTS (
		SELECT 'x' 
			FROM RestDB.dbo.Employee
			JOIN inserted ON (RestDB.dbo.Employee.Id = inserted.Leader)
			WHERE (RestDB.dbo.Employee.CompanyId != (SELECT CompanyId FROM RestDB.dbo.Division WHERE Id = 
								(SELECT DivisionId FROM RestDB.dbo.Project WHERE Id = inserted.ProjectId) )) 
				OR (RestDB.dbo.Employee.CompanyId IS NULL)
	)
		BEGIN
			RAISERROR('Employee is from a different Company.', 11, 1);
			ROLLBACK TRANSACTION;
		END;
	ELSE
		BEGIN
			IF EXISTS(SELECT 'x' FROM deleted)	-- update
				BEGIN
					UPDATE RestDB.dbo.Department 
						SET 
							Title = inserted.Title,
							Code = inserted.Code,
							Leader = inserted.Leader,
							ProjectId = inserted.ProjectId
							FROM inserted
					WHERE (RestDB.dbo.Department.Id IN (SELECT Id FROM deleted));
				END;
			ELSE	-- insert
				BEGIN
					INSERT INTO RestDB.dbo.Department(Title, Code, Leader, ProjectId)
						SELECT Title, Code, Leader, ProjectId FROM inserted;

					SELECT Id FROM RestDB.dbo.Department WHERE @@ROWCOUNT > 0 AND Id = scope_identity();
				END;
		END;
END;


--	FIRMA
GO
CREATE OR ALTER TRIGGER TR_com_check_leader 
ON RestDB.dbo.Company
INSTEAD OF INSERT, UPDATE
AS
BEGIN
	IF EXISTS (
		SELECT 'x' 
			FROM RestDB.dbo.Employee
			JOIN inserted ON (RestDB.dbo.Employee.Id = inserted.Director)
			WHERE (RestDB.dbo.Employee.CompanyId != inserted.Id) 
				AND (RestDB.dbo.Employee.CompanyId IS NOT NULL)
	)
		BEGIN
			RAISERROR('Employee is from a different Company.', 11, 1);
			ROLLBACK TRANSACTION;
		END;
	ELSE
		BEGIN
			IF EXISTS(SELECT 'x' FROM deleted)	-- update
				BEGIN
					UPDATE RestDB.dbo.Company 
						SET 
							Title = inserted.Title,
							Code = inserted.Code,
							Director = inserted.Director
							FROM inserted
					WHERE (RestDB.dbo.Company.Id IN (SELECT Id FROM deleted));
				END;
			ELSE	-- insert
				BEGIN
					INSERT INTO RestDB.dbo.Company(Title, Code, Director)
						SELECT Title, Code, Director FROM inserted;

					SELECT Id FROM RestDB.dbo.Company WHERE @@ROWCOUNT > 0 AND Id = scope_identity();
				END;
		END;
END;

GO
CREATE OR ALTER TRIGGER TR_com_set_director
ON RestDB.dbo.Company
AFTER INSERT
AS
BEGIN
	IF EXISTS (
		SELECT 'x' FROM RestDB.dbo.Employee JOIN inserted ON (RestDB.dbo.Employee.Id = inserted.Director)
			WHERE RestDB.dbo.Employee.CompanyId IS NULL
	)
	BEGIN
		UPDATE RestDB.dbo.Employee
			SET CompanyId = inserted.Id
			FROM inserted
		WHERE RestDB.dbo.Employee.Id = inserted.Director
	END;
END;

GO
CREATE OR ALTER TRIGGER TR_com_delete
ON RestDB.dbo.Company
INSTEAD OF DELETE
AS
BEGIN
	UPDATE RestDB.dbo.Employee
		SET CompanyId = null
		FROM deleted
	WHERE CompanyId = deleted.Id;

	DELETE FROM RestDB.dbo.Division
		WHERE CompanyId IN (SELECT Id FROM deleted);
	DELETE FROM RestDB.dbo.Company
		WHERE Id IN (SELECT Id FROM deleted);
END;

