using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Teams.Domain.SharedKernel;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.Aggregates.UserAggregate;

namespace Teams.Infrastructure.EntityConfigurations;

public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(64);
        
        builder.Property(p => p.Description).HasMaxLength(1024);

        var projectTypeConverter = new ValueConverter<ProjectType, string>(
            v => v.Name, // when writing 
            v => ProjectType.FromName(v) // when reading
        );
        
        builder.Property(p => p.Type)
            .HasConversion(projectTypeConverter)
            .HasMaxLength(64);
        
        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasMaxLength(64);

        builder.Property(p => p.OwnerId);
        
        builder.HasOne<User>(u => u.Owner)
            .WithMany()
            .HasForeignKey(p => p.OwnerId)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}