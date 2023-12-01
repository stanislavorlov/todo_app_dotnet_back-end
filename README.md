# todo_app_dotnet_back-end

Example application created by AI using GitHub copilot. https://github.com/features/copilot

Simple ToDo WebUI application using MS SQL server database connected via EF.Core.

Prerequisites
------------
- .Net 7
- C# 11 and previous
- Docker 24.06 or higher

Local Development
------------
- clone repository
- `cd` into `TodoAppWeb` project directory
- Replace MS SQL Database connection string in `appsettings.json`:
  * Replace `Server` with you local MS SQL Server (default `localhost`)
  * Replace `User Id` \ `Password` with your sql server credentials
- Run `dotnet build` and `dotnet run` commands
- Application will be available at http://localhost:5150

Build application
------------
- naviate into root directory (where TodoAppWeb.sln is located)
- Run `dotnet build` command

Running tests
------------
- `cd` into `TodoAppWeb.Tests` project directory
- Run `dotnet test` command

Running application via docker-compose
------------
- naviate into root directory (where docker-compose.yml is located)
- Run `docker-compose up` command
- Navigate to application http://localhost:5000/



