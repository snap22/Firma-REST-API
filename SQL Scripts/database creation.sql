

CREATE DATABASE TestDB2;

GO
USE TestDB2;

GO
CREATE TABLE TestDB2.dbo.Company
(
	Id INT PRIMARY KEY IDENTITY,
	Title VARCHAR(20) NOT NULL,
	Code CHAR(4) UNIQUE NOT NULL,
	Director INT NOT NULL,
);

GO
CREATE TABLE TestDB2.dbo.Employee
(
	Id INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(20) NOT NULL,
	Title VARCHAR(10) NULL,
	Email VARCHAR(50) UNIQUE NOT NULL,
	Contact VARCHAR(15) NOT NULL,	
	CompanyId INT NULL,
	CONSTRAINT FK_Employee_CompanyId FOREIGN KEY (CompanyId) REFERENCES TestDB2.dbo.Company(Id)
);

GO
ALTER TABLE TestDB2.dbo.Company
	ADD CONSTRAINT FK_Company_Director FOREIGN KEY (Director) REFERENCES TestDB2.dbo.Employee(Id);

GO
CREATE TABLE TestDB2.dbo.Division
(
	Id INT PRIMARY KEY IDENTITY,
	Title VARCHAR(20) NOT NULL,
	Code CHAR(4) UNIQUE NOT NULL,
	Leader INT NOT NULL,
	CompanyId INT NOT NULL,
	CONSTRAINT FK_Division_Leader FOREIGN KEY (Leader) REFERENCES TestDB2.dbo.Employee(Id),
	CONSTRAINT FK_Division_CompanyId FOREIGN KEY (CompanyId) REFERENCES TestDB2.dbo.Company(Id)
);

GO
CREATE TABLE TestDB2.dbo.Project
(
	Id INT PRIMARY KEY IDENTITY,
	Title VARCHAR(20) NOT NULL,
	Code CHAR(4) UNIQUE NOT NULL,
	Leader INT NOT NULL,
	DivisionId INT NOT NULL,
	CONSTRAINT FK_Project_Leader FOREIGN KEY (Leader) REFERENCES TestDB2.dbo.Employee(Id),
	CONSTRAINT FK_Project_DivisionId FOREIGN KEY (DivisionId) REFERENCES TestDB2.dbo.Division(Id)
);

GO
CREATE TABLE TestDB2.dbo.Department
(
	Id INT PRIMARY KEY IDENTITY,
	Title VARCHAR(20) NOT NULL,
	Code CHAR(4) UNIQUE NOT NULL,
	Leader INT NOT NULL,
	ProjectId INT NOT NULL,
	CONSTRAINT FK_Department_Leader FOREIGN KEY (Leader) REFERENCES TestDB2.dbo.Employee(Id),
	CONSTRAINT FK_Department_ProjectId FOREIGN KEY (ProjectId) REFERENCES TestDB2.dbo.Project(Id)
);




--	DIVIZIA

GO
CREATE OR ALTER TRIGGER TR_div_check_leader 
ON TestDB2.dbo.Division
INSTEAD OF INSERT, UPDATE
AS
BEGIN
	IF EXISTS (
		SELECT 'x' 
			FROM TestDB2.dbo.Employee
			JOIN inserted ON (TestDB2.dbo.Employee.Id = inserted.Leader)
			WHERE (TestDB2.dbo.Employee.CompanyId != inserted.CompanyId) OR (TestDB2.dbo.Employee.CompanyId IS NULL)
	)
		BEGIN
			RAISERROR('Employee is from a different Company.', 11, 1);
			ROLLBACK TRANSACTION;
		END;
	ELSE
		BEGIN
			IF EXISTS(SELECT 'x' FROM deleted)	-- update
				BEGIN
					UPDATE TestDB2.dbo.Division 
						SET 
							Title = inserted.Title,
							Code = inserted.Code,
							Leader = inserted.Leader,
							CompanyId = inserted.CompanyId
							FROM inserted
					WHERE (TestDB2.dbo.Division.Id IN (SELECT Id FROM deleted));
				END;
			ELSE	-- insert
				BEGIN
					INSERT INTO TestDB2.dbo.Division(Title, Code, Leader, CompanyId)
						SELECT Title, Code, Leader, CompanyId FROM inserted;

					SELECT Id FROM TestDB2.dbo.Division WHERE @@ROWCOUNT > 0 AND Id = scope_identity();
				END;
		END;
END;

GO
CREATE OR ALTER TRIGGER TR_div_delete
ON TestDB2.dbo.Division
INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM TestDB2.dbo.Project
		WHERE DivisionId IN (SELECT Id FROM deleted);

	DELETE FROM TestDB2.dbo.Division
		WHERE Id IN (SELECT Id FROM deleted);
END;

--	PROJEKT
GO
CREATE OR ALTER TRIGGER TR_pro_check_leader 
ON TestDB2.dbo.Project
INSTEAD OF INSERT, UPDATE
AS
BEGIN
	IF EXISTS (
		SELECT 'x' 
			FROM TestDB2.dbo.Employee
			JOIN inserted ON (TestDB2.dbo.Employee.Id = inserted.Leader)
			WHERE (TestDB2.dbo.Employee.CompanyId != (SELECT CompanyId FROM TestDB2.dbo.Division WHERE Id = inserted.DivisionId)) 
				OR (TestDB2.dbo.Employee.CompanyId IS NULL)
	)
		BEGIN
			RAISERROR('Employee is from a different Company.', 11, 1);
			ROLLBACK TRANSACTION;
		END;
	ELSE
		BEGIN
			IF EXISTS(SELECT 'x' FROM deleted)	-- update
				BEGIN
					UPDATE TestDB2.dbo.Project 
						SET 
							Title = inserted.Title,
							Code = inserted.Code,
							Leader = inserted.Leader,
							DivisionId = inserted.DivisionId
							FROM inserted
					WHERE (TestDB2.dbo.Project.Id IN (SELECT Id FROM deleted));
				END;
			ELSE	-- insert
				BEGIN
					INSERT INTO TestDB2.dbo.Project(Title, Code, Leader, DivisionId)
						SELECT Title, Code, Leader, DivisionId FROM inserted;

					SELECT Id FROM TestDB2.dbo.Project WHERE @@ROWCOUNT > 0 AND Id = scope_identity();
				END;
		END;
END;

GO
CREATE OR ALTER TRIGGER TR_pro_delete
ON TestDB2.dbo.Project
INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM TestDB2.dbo.Department
		WHERE ProjectId IN (SELECT Id FROM deleted);
	DELETE FROM TestDB2.dbo.Project
		WHERE Id IN (SELECT Id FROM deleted);
END;

--	ODDELENIE
GO
CREATE OR ALTER TRIGGER TR_dep_check_leader 
ON TestDB2.dbo.Department
INSTEAD OF INSERT, UPDATE
AS
BEGIN
	IF EXISTS (
		SELECT 'x' 
			FROM TestDB2.dbo.Employee
			JOIN inserted ON (TestDB2.dbo.Employee.Id = inserted.Leader)
			WHERE (TestDB2.dbo.Employee.CompanyId != (SELECT CompanyId FROM TestDB2.dbo.Division WHERE Id = 
								(SELECT DivisionId FROM TestDB2.dbo.Project WHERE Id = inserted.ProjectId) )) 
				OR (TestDB2.dbo.Employee.CompanyId IS NULL)
	)
		BEGIN
			RAISERROR('Employee is from a different Company.', 11, 1);
			ROLLBACK TRANSACTION;
		END;
	ELSE
		BEGIN
			IF EXISTS(SELECT 'x' FROM deleted)	-- update
				BEGIN
					UPDATE TestDB2.dbo.Department 
						SET 
							Title = inserted.Title,
							Code = inserted.Code,
							Leader = inserted.Leader,
							ProjectId = inserted.ProjectId
							FROM inserted
					WHERE (TestDB2.dbo.Department.Id IN (SELECT Id FROM deleted));
				END;
			ELSE	-- insert
				BEGIN
					INSERT INTO TestDB2.dbo.Department(Title, Code, Leader, ProjectId)
						SELECT Title, Code, Leader, ProjectId FROM inserted;

					SELECT Id FROM TestDB2.dbo.Department WHERE @@ROWCOUNT > 0 AND Id = scope_identity();
				END;
		END;
END;


--	FIRMA
GO
CREATE OR ALTER TRIGGER TR_com_check_leader 
ON TestDB2.dbo.Company
INSTEAD OF INSERT, UPDATE
AS
BEGIN
	IF EXISTS (
		SELECT 'x' 
			FROM TestDB2.dbo.Employee
			JOIN inserted ON (TestDB2.dbo.Employee.Id = inserted.Director)
			WHERE (TestDB2.dbo.Employee.CompanyId != inserted.Id) 
				AND (TestDB2.dbo.Employee.CompanyId IS NOT NULL)
	)
		BEGIN
			RAISERROR('Employee is from a different Company.', 11, 1);
			ROLLBACK TRANSACTION;
		END;
	ELSE
		BEGIN
			IF EXISTS(SELECT 'x' FROM deleted)	-- update
				BEGIN
					UPDATE TestDB2.dbo.Company 
						SET 
							Title = inserted.Title,
							Code = inserted.Code,
							Director = inserted.Director
							FROM inserted
					WHERE (TestDB2.dbo.Company.Id IN (SELECT Id FROM deleted));
				END;
			ELSE	-- insert
				BEGIN
					INSERT INTO TestDB2.dbo.Company(Title, Code, Director)
						SELECT Title, Code, Director FROM inserted;

					SELECT Id FROM TestDB2.dbo.Company WHERE @@ROWCOUNT > 0 AND Id = scope_identity();
				END;
		END;
END;

GO
CREATE OR ALTER TRIGGER TR_com_set_director
ON TestDB2.dbo.Company
AFTER INSERT
AS
BEGIN
	IF EXISTS (
		SELECT 'x' FROM TestDB2.dbo.Employee JOIN inserted ON (TestDB2.dbo.Employee.Id = inserted.Director)
			WHERE TestDB2.dbo.Employee.CompanyId IS NULL
	)
	BEGIN
		UPDATE TestDB2.dbo.Employee
			SET CompanyId = inserted.Id
			FROM inserted
		WHERE TestDB2.dbo.Employee.Id = inserted.Director
	END;
END;

GO
CREATE OR ALTER TRIGGER TR_com_delete
ON TestDB2.dbo.Company
INSTEAD OF DELETE
AS
BEGIN
	UPDATE TestDB2.dbo.Employee
		SET CompanyId = null
		FROM deleted
	WHERE CompanyId = deleted.Id;

	DELETE FROM TestDB2.dbo.Division
		WHERE CompanyId IN (SELECT Id FROM deleted);
	DELETE FROM TestDB2.dbo.Company
		WHERE Id IN (SELECT Id FROM deleted);
END;

