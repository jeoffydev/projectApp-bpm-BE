# API.net core Building Property project

Check dotnet --version 7

# Install .net core

dotnet new --help
dotnet new webapi


# Run dotnet 

dotnet build
dotnet run

# Use postman to check APIs

# Add Automapper

Automapper.Extensions.Microsoft.DependencyInjection
or dotnet add package Automapper.Extensions.Microsoft.DependencyInjection

## Starting SQL server

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef

## Go to hub.docker.com 
Search for microsoft-mssql-server
Result and select: Microsoft SQL Server - Ubuntu based images

``` Powershell run the $sa_password as well
$sa_password="[SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolumes:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
```

## Add SQL server
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

This is where to store our data files in docker to -v sqlVolume:/var/opt/mssql, --rm (remove) and 
--name rosterMssql for not getting a random name in container

# Secret manager setup for DB password to run into terminal
run first to get the secret ID:
dotnet user-secrets init 

``` run this
$sa_password="[SA PASSWORD HERE]"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost; Database=BuildingPropertyAppDb; User Id=sa; Password=$sa_password; TrustServerCertificate=True"
```
Check if working:
dotnet user-secrets list


Connection String appsettings.json:
``` 
"ConnectionStrings":{
    "DefaultConnection": "Server=localhost; Database=HealthAppDb; User Id=sa; Password=PASSWORD TrustServerCertificate=True"
}
``` 

Program.cs
``` 
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<DataContext>(connString);
``` 

# run Migration EF
dotnet ef migrations add InitialCreate

dotnet ef database update

Run all:
dotnet ef migrations script
dotnet ef database update

Seeding
dotnet ef migrations add InitialCreateAdmin

# Login to Azure App Services for deployment

dotnet add package Microsoft.Extensions.Logging.AzureAppServices

Install azure tools extension in VS

# JWT see the appsettings.json first
add secret key here 
Install - dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

# !!!!!!!!!!!Authentication files!!!!!!!!!!!!!!!!!!1
Data/AuthRepository
Data/IAuthRepository
appsettings.json - token
Program.cs - builder.Services.AddAuthentication() and app.UseAuthentication() and app.UseAuthorization()


# Unit test
dotnet add package xunit
# xunit  and test folder should be outside the root directory of app (asp-core7-BE)
dotnet new xunit -o unit-test-invoicequote
dotnet add unit-test-invoicequote/unit-test-invoicequote.csproj reference asp-core7-BE/asp-core7-BE.csproj

dotnet add package NUnit
dotnet add package EntityFrameworkCoreMock.Moq

RUN:
dotnet test unit-test-invoicequote/unit-test-invoicequote.csproj
dotnet test unit-test-invoicequote.csproj

# Swagger authentication test
dotnet add package Swashbuckle.AspNetCore.Filters
Go to program.cs builder.Services.AddSwaggerGen();