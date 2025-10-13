using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.TeamAggregate;

namespace Teams.Infrastructure.EntityConfigurations;

public class TeamMemberEntityTypeConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {

    }
}