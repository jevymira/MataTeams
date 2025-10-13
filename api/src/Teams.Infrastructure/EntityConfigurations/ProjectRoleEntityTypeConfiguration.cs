using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.SharedKernel;

namespace Teams.Infrastructure.EntityConfigurations;

public class ProjectRoleEntityTypeConfiguration : IEntityTypeConfiguration<ProjectRole>
{
    public void Configure(EntityTypeBuilder<ProjectRole> builder)
    {
        builder.HasOne<Role>(pr => pr.Role)
            .WithMany()
            .HasForeignKey(pr => pr.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany<ProjectRoleSkill>(prs => prs.Skills)
            .WithOne()
            .HasForeignKey(prs => prs.ProjectRoleId);
    }
}