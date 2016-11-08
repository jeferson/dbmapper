
@echo off

call SetupTestDatabase.bat >nul 2>nul
call nunit3-console ..\..\DbMapper.Tests\bin\Debug\DbMapper.Tests.dll --where "cat == IntegrationTests" --noresult
call TearDownTestDatabase.bat >nul 2>nul