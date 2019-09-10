@echo off
cd %~dp0 
set NUGETEXE=%USERPROFILE%\VSProjects\NuGet.exe

if NOT exist %NUGETEXE% (
    ECHO NUGETEXE DOES NOT EXIST! DOWNLOAD FROM https://www.nuget.org/downloads AND PUT IN IN "%USERPROFILE%\VSProjects\NuGet.exe"
    EXIT /B 1
)
set NUGET_LOCAL_REPOSITORY=%USERPROFILE%\VSProjects\Nuget.Local

%NUGETEXE% setApiKey oy2hb5p42hauyccg7zvaeiw6hg6576oedkxjesswwxdv7i

CALL :dopackahe org.codegen.common.3.1
rem CALL :dopackahe org.codegen.model.lib.3.2
rem CALL :dopackahe org.codegen.model.lib.db.3.3
rem CALL :dopackahe org.codegen.model.lib.db.mssql.1.0
rem CALL :dopackahe org.codegen.model.lib.db.mysql.1.0
rem CALL :dopackahe org.codegen.win.controls.3.2


:dopackahe
rem %NUGETEXE% pack CodeGen.WinControls.Library.nuspec
%NUGETEXE% push %NUGET_LOCAL_REPOSITORY%\%~1.nupkg -Source https://api.nuget.org/api/v2/package -Verbosity detail
rem  https://api.nuget.org/api/v2/package

EXIT /B 0

