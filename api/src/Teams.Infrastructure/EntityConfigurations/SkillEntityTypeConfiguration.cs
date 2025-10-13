using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.SharedKernel;

namespace Teams.Infrastructure.EntityConfigurations;

public class SkillEntityTypeConfiguration : IEntityTypeConfiguration<Skill>
{
    
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        
    }
}