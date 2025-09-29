using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.SharedKernel;

namespace Teams.Infrastructure.EntityConfigurations;

public class SkillEntityTypeConfiguration : IEntityTypeConfiguration<Skill>
{
    
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("skills");
        
        builder.Property(s => s.Id)
            .HasColumnName("id");
        
        builder.Property(s => s.Name)
            .HasColumnName("name");
    }
}