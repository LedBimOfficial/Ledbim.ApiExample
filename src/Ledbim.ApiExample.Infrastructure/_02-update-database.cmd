set /p id=Ortam:
set ASPNETCORE_ENVIRONMENT=%id%
dotnet ef --startup-project ../Ledbim.ApiExample/ database update --context ApplicationContext
pause