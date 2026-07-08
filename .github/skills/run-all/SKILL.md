# run-all skill

Purpose: provide a small automation to start all three API services locally in separate terminals for development.

When to use
- Startup: quickly run all services for end-to-end testing.

Behavior
- Opens three terminals and runs:

```
dotnet run --project VehicleApi/VehicleApi.csproj
dotnet run --project CustomersApi/CustomersApi.csproj
dotnet run --project ReservationsApi/ReservationsApi.csproj
```

Notes
- The skill does not modify source files. It only automates terminal commands.
- On Windows, prefer PowerShell terminals. Ensure `dotnet` is on PATH.