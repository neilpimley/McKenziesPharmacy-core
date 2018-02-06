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
dotnet build --configuration "%config%"

dotnet pack pharmacy.models/pharmacy.models.csproj --configuration "%config%"
dotnet pack pharmacy.repositories/pharmacy.repositories.csproj --configuration "%config%"
dotnet pack pharmacy.services/pharmacy.services.csproj --configuration "%config%"
