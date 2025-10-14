using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.UserAggregate;
using Teams.Domain.SharedKernel;

namespace Teams.Infrastructure.EntityConfigurations;

public class UserSkillEntityTypeConfiguration : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(us => us.UserId);
        
        builder.HasOne(us => us.Skill)
            .WithMany()
            .HasForeignKey(us => us.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(us => us.Proficiency)
            .HasConversion<int>()
            .IsRequired();
    }
}