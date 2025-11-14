# MataTeams

A CSUN student-project matchmaker.

## Local Development

### Initial Setup (JetBrains Rider OR EF Core tools CLI)

1. Define a `secrets.json` for `Identity.API` (**Rider**: right-click `Identity.API` > Add > Tools > .NET User Secrets):
   * `DefaultConnection` is your local PostgreSQL connection string, e.g., `"Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=matateams_identity"`
   * `SecurityKey` must be 32 characters or more.

```
{
    "ConnectionStrings": {
        "DefaultConnection": ""
    },
    "Jwt": {
        "ValidAudience": "",
        "ValidIssuer": "",
        "SecurityKey": "" 
    },
    "SeedUser": {
        "UserName": "",
        "Email": "",
        "Password": ""
    }
}
```

2. Run PostgreSQL.
3. Create and seed a local database for `Identity.API`... (and disregard any connection error that may appear).
   * **Rider**: (right-click) Entity Framework Core > Update Database... > OK
   * **EF Core** tools CLI: `cd src/Identity.API` > `dotnet ef database update`
4. Create another `secrets.json`, this time for `Teams.API`. Copy over the same values from `Identity.API` except for:
   * `Database` defined for `"DefaultConnection"`, since it should differ to produce a separate database, e.g., `matateams` without the `_identity` postfix. (This carries the additional benefit of making error resolution described in other sections less painful.)
   * `"IdentityGuid"`, the unique identifiers of the `ApplicationUser` entities seeded to table `AspNetUsers`. Query the `AspNetUsers` table in your local identity database to obtain these values; e.g, run `SELECT "Id" FROM "AspNetUsers";`.

```
{
    "ConnectionStrings": {
        DefaultConnection": ""
    },
    "Jwt": {
        "ValidAudience": "",
        "ValidIssuer": "",
        "SecurityKey": ""
    },
    "SeedUsers": [
    {
      "IdentityGuid": ""
    },
    {
      "IdentityGuid": ""
    }
  ]
}
```

5. Create and seed the core database with sample domain `User` entities (tied to their respective `IdentityUser`) and sample `Project`/`Skill` entities:
   * **Rider**: (right-click) Entity Framework Core > Update Database... > Migrations project: `Teams.Infrastructure` + Startup project: `Teams.API` > OK
   * **EF Core tools CLI**: `cd src` >`dotnet ef database update --project Teams.Infrastructure --startup-project Teams.API`

### Error Resolution

#### Errors when calling `dotnet ef database update` following `git pull` (2 steps):

1. Drop the core database:
   * **Rider**: (right click) Teams.Infrastructure > Entity Framework Core > Drop Database > set "Startup project" to `Teams.API`.
   * **EF Core tools CLI**: `cd src` > `dotnet ef database drop --project Teams.Infrastructure --startup-project Teams.API`. 

2. Re-create and re-seed the core database (and disregard the connection error that appears):
   * **Rider**: (right click) Teams.Infrastructure > Entity Framework Core > Update Database > (if not already: set "Startup project" to Teams.API).
   * **EF Core tools CLI**: `cd src` > `dotnet ef database update --project Teams.Infrastructure --startup-project Teams.API`.

### Migrating to SQL Server (Windows or Linux)

#### Windows

1. Install Microsoft SQL Server 2022 (Developer ed.) and SQL Server Management Studio.

2. Within directory `MataTeams/api`, update the `Identity.API` and `Teams.API` connection strings:

```
dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
    "Server=localhost;Database=MataTeamsIdentity;Integrated Security=True;TrustServerCertificate=True;" \
    --project src/Identity.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
    "Server=localhost;Database=MataTeams;Integrated Security=True;TrustServerCertificate=True;" \
    --project src/Identity.API
```

```
dotnet user-secrets set "SeedUsers:0:IdentityGuid" \
    "00000000-0000-0000-0000-000000000000" \
    --project src/Teams.API
dotnet user-secrets set "SeedUsers:1:IdentityGuid" \
    "00000000-0000-0000-0000-000000000001" \ 
    --project src/Teams.API
```

3. Generate the databases; run the following within directory `MataTeams/api`:

```
dotnet ef database update --project src/Identity.API
dotnet ef database update --project src/Teams.Infrastructure --startup-project src/Teams.API
```

#### Linux (Docker)

1. Pull the Microsoft SQL Server 2022 image and define a password (as here):

```
docker pull mcr.microsoft.com/mssql/server:2022-latest
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Passw0rd" \      
   -p 1433:1433 --name sql2022 --hostname sql2022 \
   -d \
   mcr.microsoft.com/mssql/server:2022-latest
```

(MS SQL Server can also be installed natively.)

2. Within directory `MataTeams/api`, update the `Identity.API` and `Teams.API` connection strings (broadly similar between containers and native installations):

```
dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
    "Data Source=localhost;Initial Catalog=MataTeamsIdentity;User ID=SA;Password=Passw0rd;TrustServerCertificate=True" \
    --project src/Identity.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
    "Data Source=localhost;Initial Catalog=MataTeams;User ID=SA;Password=Passw0rd;TrustServerCertificate=True" \
    --project src/Teams.API
```

```
dotnet user-secrets set "SeedUsers:0:IdentityGuid" \
    "00000000-0000-0000-0000-000000000000" \
    --project src/Teams.API
dotnet user-secrets set "SeedUsers:1:IdentityGuid" \
    "00000000-0000-0000-0000-000000000001" \
    --project src/Teams.API
```

3. Generate the databases; run the following within directory `MataTeams/api`:

```
dotnet ef database update --project src/Identity.API
dotnet ef database update --project src/Teams.Infrastructure --startup-project src/Teams.API
```