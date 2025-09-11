using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entities;

public class ReviewContext : IdentityDbContext<ApplicationUser>
{
    public ReviewContext() {}
    
    public ReviewContext(DbContextOptions<ReviewContext> options) : base(options) {}
    
    public DbSet<Review> Reviews { get; set; }
    
    // (OnConfiguring() discouraged for production applications)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<ReviewContext>();
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseNpgsql(configuration["DefaultConnection"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // keys of Identity tables mapped in IdentityDbContext
        
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(e => e.Reviews)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}