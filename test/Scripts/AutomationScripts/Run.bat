
@echo off

call SetupTestDatabase.bat >nul 2>nul
call nunit3-console ..\..\DbMapper.DAL.SqlServer.Tests\bin\Debug\DbMapper.DAL.SqlServer.Tests.dll
call TearDownTestDatabase.bat >nul 2>nul