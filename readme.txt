## Database Configuration

This project uses SQL Server and Entity Framework Core.

For local development, configure the connection string using
ASP.NET Core User Secrets:

dotnet user-secrets init

dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_CONNECTION_STRING"