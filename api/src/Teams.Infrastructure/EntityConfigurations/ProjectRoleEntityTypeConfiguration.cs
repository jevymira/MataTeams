using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.SharedKernel;

namespace Teams.Infrastructure.EntityConfigurations;

public class ProjectRoleEntityTypeConfiguration : IEntityTypeConfiguration<ProjectRole>
{
    public void Configure(EntityTypeBuilder<ProjectRole> builder)
    {
        builder.ToTable("project_roles")
            .HasKey(pr => pr.Id);
        
        builder.Property(pr => pr.Id)
            .HasColumnName("id");
        
        builder.Property(pr => pr.ProjectId)
            .HasColumnName("project_id");
        
        builder.Property(pr => pr.RoleId)
            .HasColumnName("role_id");
       
        builder.HasOne(pr => pr.Role)
            .WithOne()
            .HasForeignKey<ProjectRole>(pr => pr.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<ProjectRoleSkill>(prs => prs.Skills)
            .WithOne()
            .HasForeignKey(prs => prs.ProjectRoleId);
    }
}