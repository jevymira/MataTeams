using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.MemberAggregate;

namespace Teams.Infrastructure.EntityConfigurations;

public class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("members");
        
        builder.Property(member => member.Id)
            .HasColumnName("id");
        
        builder.Property(member => member.IdentityGuid)
            .HasColumnName("identity_guid");
    }
}