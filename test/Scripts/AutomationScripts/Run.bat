
@echo off

call SetupTestDatabase.bat >nul 2>nul
call nunit3-console ..\..\DbMapper.Tests\bin\Debug\DbMapper.Tests.dll
call TearDownTestDatabase.bat >nul 2>nul