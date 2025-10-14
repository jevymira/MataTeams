using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.SharedKernel;

namespace Teams.Infrastructure.EntityConfigurations;

public class ProjectRoleSkillEntityTypeConfiguration : IEntityTypeConfiguration<ProjectRoleSkill>
{
    public void Configure(EntityTypeBuilder<ProjectRoleSkill> builder)
    {
        builder.HasOne<Skill>(rs => rs.Skill)
            .WithMany()
            .HasForeignKey(rs => rs.SkillId)
            .HasPrincipalKey(s => s.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(us => us.Proficiency)
            .HasConversion<int>()
            .IsRequired();
    }
}