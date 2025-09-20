using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.TeamAggregate;

public class TeamMembershipRequest : Entity
{
    public int TeamId { get; private set; }
    
    public int UserId { get; private set; }
    
    public TeamMembershipRequestStatus Status { get; private set; }
}