using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.UserAggregate;
using Teams.Domain.SharedKernel;

namespace Teams.Infrastructure.EntityConfigurations;

public class UserSkillEntityTypeConfiguration : IEntityTypeConfiguration<UserSkill>
{
    public void Configure(EntityTypeBuilder<UserSkill> builder)
    {
        builder.ToTable("user_skills");
        
        builder.HasKey(us => new { us.UserId, us.SkillId });
        
        builder.Property(us => us.UserId)
            .HasColumnName("user_id");
        
        builder.Property(us => us.SkillId)
            .HasColumnName("skill_id");
        
        builder.HasOne(us => us.Skill)
            .WithMany()
            .HasForeignKey(us => us.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(us => us.Proficiency)
            .HasColumnName("proficiency")
            .HasConversion<int>()
            .IsRequired();
    }
}