using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teams.Domain.SharedKernel;

namespace Teams.Infrastructure.EntityConfigurations;

public class RecommendationEntityTypeConfiguration : IEntityTypeConfiguration<Recommendation>
{
    public void Configure(EntityTypeBuilder<Recommendation> builder)
    {
        builder.Property(r => r.MatchPercentage)
            .HasPrecision(5, 4);

        builder.Property(r => r.ModifiedDate)
            .IsRequired(false)
            .HasDefaultValueSql("GETDATE()");
    }
}