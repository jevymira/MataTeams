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


