@echo off
cd %USERPROFILE%\VSProjects\codegen-lib-LastEditionOfIIC

rem call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\VsDevCmd.bat"
set NUGETEXE="C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\Extensions\hflum5rs.rt1\~PC\ProjectTemplates\CSharp\NuGet\NuGet.exe"
set OUTPUTDIR=%USERPROFILE%\VSProjects\Nuget.Local
set SOLUTIONDIR=%USERPROFILE%\VSProjects\IIC\packages

echo "delete and build org.codegen.common.3.1"
rd /s /q %SOLUTIONDIR%\org.codegen.common.3.1
del %OUTPUTDIR%\org.codegen.common.3.1.nupkg
%NUGETEXE% pack org.codegen.common.3.1.nuspec -OutputDirectory %OUTPUTDIR%

echo "--------"

rd /s /q %SOLUTIONDIR%\org.codegen.model.lib.3.2
rd /s /q %SOLUTIONDIR%\org.codegen.model.lib.db.3.3
rd /s /q %SOLUTIONDIR%\org.codegen.lib.db.mysql.1.0
rd /s /q %SOLUTIONDIR%\org.codegen.win.controls.3.2


del %OUTPUTDIR%\org.codegen.model.lib.3.2.nupkg
del %OUTPUTDIR%\org.codegen.model.lib.db.3.3.nupkg
del %OUTPUTDIR%\org.codegen.lib.db.mysql.1.0.nupkg
del %OUTPUTDIR%\org.codegen.win.controls.3.2.nupkg


%NUGETEXE% pack org.codegen.model.lib.3.2.nuspec -OutputDirectory %OUTPUTDIR%
%NUGETEXE% pack org.codegen.model.lib.db.3.3.nuspec -OutputDirectory %OUTPUTDIR%
%NUGETEXE% pack org.codegen.win.controls.3.2.nuspec -OutputDirectory %OUTPUTDIR%
%NUGETEXE% pack org.codegen.model.lib.db.mysql.1.0.nuspec -OutputDirectory %OUTPUTDIR%

