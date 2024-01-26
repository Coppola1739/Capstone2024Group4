@echo off

REM Set the path to the test project (adjust if needed)
set TEST_PROJECT=CapstoneTests\CapstoneTests.csproj

REM Get the path to the project root
for /f "delims=" %%I in ("%~dp0") do set "PROJECT_ROOT=%%~fI"

REM Set paths relative to the project root
set NUNIT_CONSOLE_PATH=%PROJECT_ROOT%\packages\NUnit.ConsoleRunner.3.12.0\tools\net5.0\nunit3-console.exe
set REPORTGENERATOR_PATH=%PROJECT_ROOT%\packages\ReportGenerator.4.8.12\tools\net5.0\ReportGenerator.exe

REM Step 1: Run NUnit tests and collect coverage with Coverlet
dotnet test "%PROJECT_ROOT%\%TEST_PROJECT%" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

REM Step 2: Generate human-readable coverage report with ReportGenerator
%REPORTGENERATOR_PATH% "-reports:%PROJECT_ROOT%\coverage.opencover.xml" "-targetdir:%PROJECT_ROOT%\coverage-report" "-reporttypes:HTML"

REM Step 3: Open the coverage report in the default web browser (Windows)
start "" "%PROJECT_ROOT%\coverage-report\index.html"

pause
