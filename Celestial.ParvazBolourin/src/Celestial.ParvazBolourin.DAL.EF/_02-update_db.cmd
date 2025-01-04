dotnet tool update --global dotnet-ef --version 8.0.7
dotnet tool restore
dotnet build
dotnet ef --startup-project ../Celestial.ParvazBolourin/ database update --context MainDbContext
pause
