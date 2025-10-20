using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.ProjectAggregate;

public class TeamMembershipRequest : Entity
{
    public Guid TeamId { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public TeamMembershipRequestStatus Status { get; private set; }
}