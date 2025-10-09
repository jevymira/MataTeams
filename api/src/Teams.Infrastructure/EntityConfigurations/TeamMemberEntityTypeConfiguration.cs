using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.TeamAggregate;

namespace Teams.Infrastructure.EntityConfigurations;

public class TeamMemberEntityTypeConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        builder.ToTable("team_members");
        
        builder.Property(tm => tm.Id)
            .HasColumnName("id");
        
        builder.Property(tm => tm.TeamId)
            .HasColumnName("team_id");
        
        builder.Property(tm => tm.UserId)
            .HasColumnName("user_id");
    }
}