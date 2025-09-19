using Microsoft.EntityFrameworkCore;
using Projects.API.Infrastructure.EntityConfigurations;
using Projects.API.Model;

namespace Projects.API.Infrastructure;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext() { }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }
    
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
            .AddUserSecrets<ProjectDbContext>();
        IConfigurationRoot configuration = builder.Build();
        
        optionsBuilder.UseNpgsql(configuration["DefaultConnection"]);
        // Adapted from: https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
        // Called as part of `dotnet ef database update`, even if there are no model changes.
        optionsBuilder.UseSeeding((context, _) =>
        {
            /*
            var testProject = context.Set<Project>()
                .FirstOrDefault(p => p.OwnerUserId == testUser.Id);
            if (testProject == null)
            {
                context.Set<Project>().Add(new Project { OwnerUserId = testUser.Id });
                context.SaveChanges();
            }
            */
        });
        // .UseAsyncSeeding(async (context, _, cancellationToken) => {});
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectEntityTypeConfiguration());
    }
}