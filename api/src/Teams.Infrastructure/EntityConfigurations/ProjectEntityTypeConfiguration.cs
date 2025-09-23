using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.Aggregates.MemberAggregate;
using Teams.Domain.Aggregates.ProjectAggregate;

namespace Teams.Infrastructure.EntityConfigurations;

public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(64);
        
        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasMaxLength(1024);
        
        builder.HasOne<Member>()
            .WithMany()
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(p => p.OwnerId)
            .HasColumnName("owner_id");
    }
}