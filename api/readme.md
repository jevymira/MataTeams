# Local Development

1. In `Identity.API` (right-click > Add > Tools > .NET User Secrets):

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

`DefaultConnection` is your local PostgreSQL connection string.

`SecurityKey` must be 32-characters or above.

2. Have PostgreSQL running.
3. In `Identity.API`, do (right-click) Entity Framework Core > Update Database... (or use the EF Core Tools CLI); `UseSeeding` will run and populate the local database with a user having the credentials defined in User Secrets. Verify this in your local database.
4. `Teams.API` .NET User Secrets is much the same, except that `IdentityGuid` requires you to copy the identifier of the user seeded to the table `AspNetUsers` in the previous step.

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

5. In `Teams.API`, do (right-click) Entity Framework Core > Update Database... Select `Teams.Infrastructure` as the Migrations project and `Teams.API` as the Startup project. The database will be seeded with the domain's representation of a user and a few other entities, as defined in `Extensions.cs`.