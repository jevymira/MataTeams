using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.Aggregates.UserAggregate;

namespace Teams.Infrastructure.EntityConfigurations;

public class TeamMembershipRequestEntityTypeConfiguration : IEntityTypeConfiguration<TeamMembershipRequest>
{
    public void Configure(EntityTypeBuilder<TeamMembershipRequest> builder)
    {
        builder.Property(r => r.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne<Team>()
            .WithMany()
            .HasForeignKey(r => r.TeamId);
        
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(r => r.UserId);
    }
}