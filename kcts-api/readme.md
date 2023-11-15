generate migrations:
dotnet ef migrations add AddIdentityTables --project ../Persistence/Persistence.csproj

apply migrations to db:
dotnet ef database update
