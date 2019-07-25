@echo off
cd %~dp0 
REM THE ABOVE CHANGES CURRENT DIR TO BATCH FILE'S DIR
REM %USERPROFILE%\VSProjects\codegen-lib-LastEditionOfIIC

rem call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\VsDevCmd.bat"
set NUGETEXE=%USERPROFILE%\VSProjects\NuGet.exe

if NOT exist %NUGETEXE% (
	ECHO NUGETEXE DOES NOT EXIST! DOWNLOAD FROM https://www.nuget.org/downloads AND PUT IN IN "%USERPROFILE%\VSProjects\NuGet.exe"
	EXIT /B 1
)


set NUGET_LOCAL_REPOSITORY=%USERPROFILE%\VSProjects\Nuget.Local
if NOT exist %NUGET_LOCAL_REPOSITORY% (
	MKDIR %NUGET_LOCAL_REPOSITORY%
)
if NOT exist %NUGET_LOCAL_REPOSITORY% (
	ECHO %NUGET_LOCAL_REPOSITORY% DOES NOT EXIST! CREATE IN "%NUGET_LOCAL_REPOSITORY%"
	EXIT /B 1
)


set SOLUTIONDIR=%USERPROFILE%\VSProjects\IIC\packages

CALL :dopackahe org.codegen.common.3.1
CALL :dopackahe org.codegen.model.lib.3.2
CALL :dopackahe org.codegen.model.lib.db.3.3
CALL :dopackahe org.codegen.model.lib.db.mssql.1.0
CALL :dopackahe org.codegen.model.lib.db.mysql.1.0
CALL :dopackahe org.codegen.win.controls.3.2

EXIT /B %ERRORLEVEL% 

:dopackahe
echo "------start %~1 --"
rd /s /q %SOLUTIONDIR%\%~1
del %NUGET_LOCAL_REPOSITORY%\%~1.nupkg
%NUGETEXE% pack %~1.nuspec -OutputDirectory %NUGET_LOCAL_REPOSITORY%
echo "------end %~1 --"
rem ECHO The square of %~1 is %~2
rem PAUSE
EXIT /B 0
