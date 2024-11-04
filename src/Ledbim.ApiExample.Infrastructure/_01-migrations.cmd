set /p name=MigrationName:
dotnet ef migrations --startup-project ../Ledbim.ApiExample/ add V_%name% --context ApplicationContext
pause
