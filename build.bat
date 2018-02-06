@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)
 
set version=1.0.0
if not "%PackageVersion%" == "" (
   set version=%PackageVersion%
)

set nuget=
if "%nuget%" == "" (
	set nuget=nuget
)

REM Build
dotnet publish --configuration "%config%"


REM Package
mkdir Build
mkdir Build\lib
mkdir Build\lib\netcoreapp2.0

copy pharmacy.models\bin\%config%\netcoreapp2.0\pharmacymodels.dll Build\lib\netcoreapp2.0
copy pharmacy.repositories\bin\%config%\netcoreapp2.0\pharmacyrepositories.dll Build\lib\netcoreapp2.0
copy pharmacy.services\bin\%config%\netcoreapp2.0\pharmacyservices.dll Build\lib\netcoreapp2.0

call %nuget% pack "mckenzies.pharmacy.core.nuspec" -NoPackageAnalysis -verbosity detailed -o Build -Version %version% -p Configuration="%config%"
