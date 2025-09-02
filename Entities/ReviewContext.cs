using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class ReviewContext : IdentityDbContext<ApplicationUser>
{
    public ReviewContext() {}
    
    public ReviewContext(DbContextOptions<ReviewContext> options) : base(options) {}
    
    public DbSet<Review> Reviews { get; set; }
    
    // (OnConfiguring() discouraged for production applications)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=Boyce-Codd normal form;Database=490L");

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