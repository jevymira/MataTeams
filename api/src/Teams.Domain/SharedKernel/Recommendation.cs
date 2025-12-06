using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.Aggregates.UserAggregate;
using Teams.Domain.SeedWork;

namespace Teams.Domain.SharedKernel;

public class Recommendation : Entity
{
    public User User { get; set; }
    public Project Project { get; set; }
    public decimal MatchPercentage { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
