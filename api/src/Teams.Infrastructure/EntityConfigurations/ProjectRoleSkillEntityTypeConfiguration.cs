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
        
        builder.HasKey(rs => new {rs.ProjectRoleId, rs.SkillId});
        
        builder.Property(rs => rs.ProjectRoleId)
            .HasColumnName("project_role_id");
        
        builder.Property(rs => rs.SkillId)
            .HasColumnName("skill_id");
        
        builder.HasOne(rs => rs.Skill)
            .WithOne()
            .HasForeignKey<ProjectRoleSkill>(rs => rs.SkillId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(us => us.Proficiency)
            .HasColumnName("proficiency")
            .HasConversion<int>()
            .IsRequired();
    }
}