# Local Development

## Initial Setup (JetBrains Rider OR EF Core tools CLI)

1. Define a `secrets.json` for `Identity.API` (**Rider**: right-click `Identity.API` > Add > Tools > .NET User Secrets):
   * `DefaultConnection` is your local PostgreSQL connection string, e.g., `"Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=matateams_identity"`
   * `SecurityKey` must be 32 characters or above.

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

2. Have PostgreSQL running.
3. Create and seed a local database for `Identity.API`... (disregard any connection error)
   * **Rider**: (right-click) Entity Framework Core > Update Database... > OK
   * **EF Core** tools CLI: `cd src/Identity.API` > `dotnet ef database update`
4. Create another `secrets.json`, this time for `Teams.API`. Copy over most of the same values from `Identity.API` except for:
   * The `Database` defined in the connection string. It should differ, e.g., `matateams` without the `_identity` postfix. This will produce a separate table, matching the intended architecture and making error resolution in the next section less painful.
   * `IdentityGuid`, which should be the identifier of the `ApplicationUser` seeded in the previous step to the table `AspNetUsers`. Query the `AspNetUsers` table in your local identity database for this value, e.g, run `SELECT "Id" FROM "AspNetUsers";`.

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
    "SeedUser": {
        "IdentityGuid": ""
    }
}
```

5. Create and seed the core database with a sample domain `User` (tied to the `IdentityUser`) and some sample `Project`/`Skill` entities:
   * **Rider**: (right-click) Entity Framework Core > Update Database... > Migrations project: `Teams.Infrastructure` + Startup project: `Teams.API` > OK
   * **EF Core tools CLI**: `cd src` >`dotnet ef database update --project Teams.Infrastructure --startup-project Teams.API`

## Error Resolution

### Errors when calling `dotnet ef database update` following `git pull` (2 steps):

1. Drop the core database:
   * **Rider**: (right click) Teams.Infrastructure > Entity Framework Core > Drop Database > set "Startup project" to `Teams.API`.
   * **EF Core tools CLI**: `cd src` > `dotnet ef database drop --project Teams.Infrastructure --startup-project Teams.API`. 

2. Re-create and re-seed the core database (and disregard the connection error that appears):
   * **Rider**: (right click) Teams.Infrastructure > Entity Framework Core > Update Database > (if not already: set "Startup project" to Teams.API).
   * **EF Core tools CLI**: `cd src` > `dotnet ef database update --project Teams.Infrastructure --startup-project Teams.API`.  


