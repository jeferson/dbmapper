USE master;
GO

IF DB_ID('DbMapperTestDatabase') IS NOT NULL
BEGIN
	PRINT 'Dropping test database';
	DROP DATABASE DbMapperTestDatabase;
END;
GO