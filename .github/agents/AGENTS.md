# AGENTS — repo guidance for AI coding agents

Purpose: give an AI agent the minimal, actionable facts to be productive in this repo.

Quick overview
- Repo type: .NET microservices sample with three API projects: `CustomersApi`, `ReservationsApi`, `VehicleApi`.
- Project layout (per service): Controllers, Data, Interfaces, Migrations, Models, Services.

Essential commands
- Build solution: `dotnet build`
- Run a single service: `dotnet run --project <ServiceFolder>/<ServiceFolder>.csproj` (e.g. `dotnet run --project VehicleApi/VehicleApi.csproj`)
- Apply EF migrations (when needed): `dotnet ef database update --project <ServiceFolder>/<ServiceFolder>.csproj`

Key files (entry points & examples)
- Vehicle API entry: [VehicleApi/Program.cs](VehicleApi/Program.cs#L1-L40)
- Vehicle controller example: [VehicleApi/Controllers/VehiclesController.cs](VehicleApi/Controllers/VehiclesController.cs#L1-L200)
- Vehicle model: [VehicleApi/Models/Vehicle.cs](VehicleApi/Models/Vehicle.cs#L1-L200)
- Customers API entry: [CustomersApi/Program.cs](CustomersApi/Program.cs#L1-L40)
- Reservations API entry: [ReservationsApi/Program.cs](ReservationsApi/Program.cs#L1-L40)
- Solution file: [VehicleTestDrive.slnx](VehicleTestDrive.slnx)

Notes for agents
- Don't duplicate documentation found in the repo; link to files above for details.
- Look for service conventions (Controllers, Services, Interfaces) when making changes.
- Each API contains EF Core migrations under `Migrations/` — use `dotnet ef` if you need to update or apply schema changes.

Suggested next customizations
- Create a small skill to "Run all services locally" that opens three terminals and runs each `dotnet run --project ...`.
- Add a launch configuration or Docker Compose recipe and a skill to run it.
- Add `.github/copilot-instructions.md` only if a different, more opinionated agent behaviour is required.

If you want, I can now add a runnable helper script or a dedicated skill to start all services locally.