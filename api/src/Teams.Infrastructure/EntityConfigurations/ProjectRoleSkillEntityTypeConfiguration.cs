using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.SharedKernel;

namespace Teams.Infrastructure.EntityConfigurations;

public class ProjectRoleSkillEntityTypeConfiguration : IEntityTypeConfiguration<ProjectRoleSkill>
{
    public void Configure(EntityTypeBuilder<ProjectRoleSkill> builder)
    {
        builder.ToTable("project_role_skills");

        builder.Property(rs => rs.Id)
            .HasColumnName("id");
        
        builder.Property(rs => rs.ProjectRoleId)
            .HasColumnName("project_role_id");
        
        builder.Property(rs => rs.SkillId)
            .HasColumnName("skill_id");
        
        builder.HasOne<Skill>(rs => rs.Skill)
            .WithMany()
            .HasForeignKey(rs => rs.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(us => us.Proficiency)
            .HasColumnName("proficiency")
            .HasConversion<int>()
            .IsRequired();
    }
}