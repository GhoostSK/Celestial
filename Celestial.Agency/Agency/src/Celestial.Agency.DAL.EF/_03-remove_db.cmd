dotnet build
dotnet ef migrations --startup-project ../Celestial.Agency/ remove --context MainDbContext --force
pause
