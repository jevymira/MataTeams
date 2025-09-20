using Microsoft.EntityFrameworkCore;
using Teams.Domain.Aggregates.TeamAggregate;
using Teams.Infrastructure.EntityConfigurations;

namespace Teams.Infrastructure;

public class TeamDbContext : DbContext
{
    public TeamDbContext(DbContextOptions<TeamDbContext> options) : base(options) { }
    
    public DbSet<Team> Teams { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TeamEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TeamMemberEntityTypeConfiguration());
    }
}