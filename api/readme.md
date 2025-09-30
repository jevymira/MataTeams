# Local Development

## Initial Setup (JetBrains Rider OR EF Core tools CLI)

1. Define a `secrets.json` for `Identity.API` (**Rider**: right-click > Add > Tools > .NET User Secrets):
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
3. To create and seed a local database for `Identity.API`... (disregard any connection error)
   * **Rider**: (right-click) Entity Framework Core > Update Database... > OK
   * **EF Core** tools CLI: `cd src/Identity.API` > `dotnet ef database update`
4. Create `Teams.API` .NET User Secrets copy over most all of the same values, except for:
   * The `Database` defined in the connection string. It should differ, i.e., `matateams` without the `_identity` postfix. This will produce a separate table, matching the intended architecture and making error resolution in the next section less painful.
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

5. To create and seed the core database with a sample domain `User` (tied to the `IdentityUser`) and some sample `Project`/`Skill` entities:
   * **Rider**: (right-click) Entity Framework Core > Update Database... > Migrations project: `Teams.Infrastructure` + Startup project: `Teams.API` > OK
   * **EF Core tools CLI**: `cd src` >`dotnet ef database update --project Teams.Infrastructure --startup-project Teams.API`
