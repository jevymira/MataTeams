using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.UserAggregate;

namespace Teams.Infrastructure.EntityConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.Property(member => member.Id)
            .HasColumnName("id");
        
        builder.Property(member => member.IdentityGuid)
            .HasColumnName("identity_guid");
        
        /*
        builder.Metadata
            .FindNavigation((nameof(User.UserSkills)))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder.HasMany(u => u.UserSkills)
            .WithOne()
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        */
    }
}