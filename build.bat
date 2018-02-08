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

REM if config == Debug {
	dotnet pack pharmacy.models/pharmacy.models.csproj --configuration "%config%" --include-symbols --include-source 
	dotnet pack pharmacy.repositories/pharmacy.repositories.csproj --configuration "%config%" --include-symbols --include-source 
	dotnet pack pharmacy.services/pharmacy.services.csproj --configuration "%config%" --include-symbols --include-source 
REM } else {
REM 	dotnet pack pharmacy.models/pharmacy.models.csproj --configuration "%config%" 
REM 	dotnet pack pharmacy.repositories/pharmacy.repositories.csproj --configuration "%config%" 
REM 	dotnet pack pharmacy.services/pharmacy.services.csproj --configuration "%config%" 
REM }
