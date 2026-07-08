# Copilot instructions — instruções rápidas para agentes de IA

Objetivo
- Fornecer instruções mínimas, acionáveis e específicas para trabalhar neste repositório.

Visão geral do repositório
- Projeto .NET com três APIs (microserviços): `CustomersApi`, `ReservationsApi`, `VehicleApi`.
- Cada serviço segue o mesmo layout: `Controllers/`, `Models/`, `Services/`, `Interfaces/`, `Data/`, `Migrations/`.

Comandos essenciais
- Build da solução: `dotnet build`
- Rodar um serviço (ex.):
  - `dotnet run --project VehicleApi/VehicleApi.csproj`
- Aplicar migrations EF (ex.):
  - `dotnet ef database update --project <ServiceFolder>/<ServiceFolder>.csproj`

Pontos de entrada e arquivos-chave
- Entry points: `VehicleApi/Program.cs`, `CustomersApi/Program.cs`, `ReservationsApi/Program.cs`
- Controllers: `*/Controllers/` (endpoints HTTP)
- Serviços e contratos: `*/Services/` e `*/Interfaces/`
- Migrations EF: `*/Migrations/`
- Solution: `VehicleTestDrive.slnx`

Convenções importantes
- Interfaces iniciam com `I` (ex.: `IVehicle`) e são registradas em DI no `Program.cs` do serviço.
- Prefira alterações isoladas por serviço; evite mudanças globais sem coordenar todos os projetos.
- Ao alterar modelos/DbContext, crie ou atualize migrations no projeto afetado.

Ao criar PRs
- Garanta que `dotnet build` passe localmente.
- Se incluir migrations, documente os comandos para aplicá-las.

Notas para agentes
- Não copie documentação existente — linke para os arquivos relevantes quando precisar de contexto.
- Para mais detalhes e sugestões de skills (ex.: "run-all"), consulte `AGENTS.md` na raiz do repositório.

Se quiser, posso adaptar este arquivo para inglês, ou adicionar exemplos de `dotnet ef` com `--startup-project` se o ambiente exigir.