using Microsoft.EntityFrameworkCore;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.Aggregates.UserAggregate;
using Teams.Domain.SharedKernel;
using Teams.Infrastructure.EntityConfigurations;

namespace Teams.Infrastructure;

public class TeamDbContext : DbContext
{
    public TeamDbContext(DbContextOptions<TeamDbContext> options) : base(options) { }
   
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; } 
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectRole> ProjectRoles { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<TeamMembershipRequest> TeamMembershipRequests { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectRoleEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RecommendationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SkillEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TeamEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TeamMemberEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TeamMembershipRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}