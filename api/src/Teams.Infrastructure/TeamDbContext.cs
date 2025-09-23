using Microsoft.EntityFrameworkCore;
using Teams.Domain.Aggregates.MemberAggregate;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.Aggregates.TeamAggregate;
using Teams.Infrastructure.EntityConfigurations;

namespace Teams.Infrastructure;

public class TeamDbContext : DbContext
{
    public TeamDbContext(DbContextOptions<TeamDbContext> options) : base(options) { }
    
    public DbSet<Member> Members { get; set; } 
    public DbSet<Project> Projects { get; set; }
    
    public DbSet<Team> Teams {  get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MemberEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TeamEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TeamMemberEntityTypeConfiguration());
    }
}