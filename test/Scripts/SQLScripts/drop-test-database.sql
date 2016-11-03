USE master;
GO

IF DB_ID('DbMapperTestDatabase') IS NOT NULL
	PRINT 'Dropping test database';
	DROP DATABASE DbMapperTestDatabase;
GO