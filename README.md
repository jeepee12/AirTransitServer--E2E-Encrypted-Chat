# AirTransitAPI--E2E-Encrypted-Chat
End-to-End Encrypted Chat API, university project.  
When running, the app can be accessible to `http://*:5000`.

Ex. :  
Locally  : `http://localhost:5000`  
Same LAN : `http://192.168.0.2:5000`  
Remotely  : `http://MyDNS:5000`  

## How to run the app locally (with Visual Studio)
* Open the solution (.sln) with Visual Studio
* Edit the launch settings under `AirTransitServer/Properties/launchSettings.json`
* Launch the app, press F5

## How to publish and run the app
Prerequisite : you need `.NET Core 2.0 SDK` on your machine -> [Installation instructions](https://docs.microsoft.com/en-us/dotnet/core/get-started).

Publish via Visual Studio :
* Right click on the project, then click Publish...
* Choose a target folder location (i.e. `bin\Release\PublishOutput`)
* Click Publish

Publish via command line :
* Open a command prompt in the root project folder
* Type `dotnet publish -c Release`
* For more information, see [dotnet publish documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish?tabs=netcore2x)

Finally, to run the application go to the output publish folder with a command prompt and run `dotnet AirTransitServer.dll`. :tada:

## Migrations
[Migrations](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/) provide a way to incrementally apply schema changes to the database to keep it in sync with your EF Core model while preserving existing data in the database.

Every time a model is modified, a migration should be created to apply new changes to the database.

Here is a summary list of command that can be used for migrations : 

PowerShell or Package Manager Console | Command Prompt | Additional Information
------------------------------------- | -------------- | ----------------------
Add-Migration {name} | dotnet ef migrations add {name}
Remove-Migration | dotnet ef migrations remove | Removes the last migration.
Update-Database | dotnet ef database update | Use `-Migration '0'` to revert all migrations.
Drop-Database | dotnet ef database drop
[Complete documentation](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell) | [Complete documentation](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)

## Technology Stack
* .NET Core 2.0 SDK
* Web API with ASP.NET Core using [this tutorial](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api)
* [Swagger](https://swagger.io/)
* Entity Framework Core with [SQLite](https://www.sqlite.org/index.html)
