
USE master;
GO

IF DB_ID('DbMapperTestDatabase') IS NOT NULL
BEGIN
	PRINT '-> Dropping database to re-create it';
	DROP DATABASE DbMapperTestDatabase;
END;
GO

PRINT '-> Creating test database';
CREATE DATABASE DbMapperTestDatabase;
GO

PRINT '-> Creating Tables';

CREATE TABLE DbMapperTestDatabase.dbo.Companies
(
	CompanyId					INT					NOT NULL		IDENTITY(1, 1),
	CompanyCode					VARCHAR(30)			NOT NULL,
	CompanyUniqueIdentifier		UNIQUEIDENTIFIER	NOT NULL,
	CompanyCreateDate			DATETIME			NOT NULL,
	CONSTRAINT PK_Companies PRIMARY KEY (CompanyId)
);
GO
	
ALTER TABLE DbMapperTestDatabase.dbo.Companies
	ADD CONSTRAINT DF_Companies_CompanyUniqueIdentifier DEFAULT NEWID() FOR CompanyUniqueIdentifier;
GO

ALTER TABLE DbMapperTestDatabase.dbo.Companies
	ADD CONSTRAINT DF_Companies_CompanyCreateDate DEFAULT GETDATE() FOR CompanyCreateDate;
GO