using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.Aggregates.UserAggregate;

namespace Teams.Infrastructure.EntityConfigurations;

public class TeamMemberEntityTypeConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        // No `Member` entity exists; instead it forms join with `User`.
        // As a result, table name defaults to "team_member" (singular),
        // if not defined explicitly as here.
        builder.ToTable("team_members");

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(u => u.UserId);
        
        builder.HasOne<ProjectRole>()
            .WithMany()
            .HasForeignKey(r => r.ProjectRoleId);
    }
}