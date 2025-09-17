using Application.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Data;

public class MataTeamsContext : IdentityDbContext<MataTeamsUser>
{
    public MataTeamsContext() {}

    public MataTeamsContext(DbContextOptions<MataTeamsContext> options) : base(options) {}
    
    public DbSet<Project> Projects { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        // else: configure context for migrations
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<MataTeamsContext>();
        IConfigurationRoot configuration = builder.Build();
        
        optionsBuilder.UseNpgsql(configuration["DefaultConnection"]);
        // Adapted from: https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
        // Called as part of `dotnet ef database update`, even if there are no model changes.
        optionsBuilder.UseSeeding((context, _) =>
        {
            var hasher = new PasswordHasher<MataTeamsUser>();
            
            var testUser = context.Set<MataTeamsUser>()
                .FirstOrDefault(u => u.UserName == "user");
            if (testUser == null)
            {
                testUser = new MataTeamsUser()
                {
                    UserName = "user",
                    Email = "user@email.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                // Underlying PasswordHasher used by ASP.NET Core Identity.
                testUser.PasswordHash = hasher 
                    .HashPassword(testUser, configuration["SeedUser:Password"]!);
                context.Set<MataTeamsUser>().Add(testUser);
                context.SaveChanges();  
            }
            
            Console.WriteLine(hasher
                .VerifyHashedPassword(testUser, testUser.PasswordHash, configuration["SeedUser:Password"])
                .ToString()
            );
            
            var testProject = context.Set<Project>()
                .FirstOrDefault(p => p.OwnerUserId == testUser.Id);
            if (testProject == null)
            {
                context.Set<Project>().Add(new Project { OwnerUserId = testUser.Id });
                context.SaveChanges();
            }
        });
        // .UseAsyncSeeding(async (context, _, cancellationToken) => {});
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // keys of Identity tables mapped in IdentityDbContext
        
        modelBuilder.Entity<MataTeamsUser>()
            .HasMany(e => e.Projects)
            .WithOne()
            .HasForeignKey(e => e.OwnerUserId)
            .IsRequired();
    }
}