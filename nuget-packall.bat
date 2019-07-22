@echo off
cd %USERPROFILE%\VSProjects\codegen-lib-LastEditionOfIIC

rem call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\VsDevCmd.bat"
set NUGETEXE="C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\Extensions\hflum5rs.rt1\~PC\ProjectTemplates\CSharp\NuGet\NuGet.exe"
set OUTPUTDIR=%USERPROFILE%\VSProjects\Nuget.Local
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
del %OUTPUTDIR%\%~1.nupkg
%NUGETEXE% pack %~1.nuspec -OutputDirectory %OUTPUTDIR%
echo "------end %~1 --"
rem ECHO The square of %~1 is %~2
rem PAUSE
EXIT /B 0
