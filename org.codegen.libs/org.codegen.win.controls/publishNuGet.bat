@echo off
cd %~dp0 
set NUGETEXE=%USERPROFILE%\VSProjects\NuGet.exe

if NOT exist %NUGETEXE% (
    ECHO NUGETEXE DOES NOT EXIST! DOWNLOAD FROM https://www.nuget.org/downloads AND PUT IN IN "%USERPROFILE%\VSProjects\NuGet.exe"
    EXIT /B 1
)

%NUGETEXE% setApiKey oy2hb5p42hauyccg7zvaeiw6hg6576oedkxjesswwxdv7i
%NUGETEXE% pack CodeGen.WinControls.Library.nuspec
%NUGETEXE% push CodeGen.WinControls.Library.3.0.2.39.nupkg -Source https://www.nuget.org/api/v2/
rem  https://api.nuget.org/v3/index.json

