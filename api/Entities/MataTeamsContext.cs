using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entities;

public class MataTeamsContext : IdentityDbContext<MataTeamsUser>
{
    public MataTeamsContext() {}
    
    public MataTeamsContext(DbContextOptions<MataTeamsContext> options) : base(options) {}
    
    public DbSet<Project> Projects { get; set; }
    
    // (OnConfiguring() discouraged for production applications)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<MataTeamsContext>();
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseNpgsql(configuration["DefaultConnection"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // keys of Identity tables mapped in IdentityDbContext
        
        modelBuilder.Entity<MataTeamsUser>()
            .HasMany(e => e.Projects)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}