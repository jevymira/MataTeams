using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.TeamAggregate;

public class TeamMember // : Entity
{
    public Guid Id { get; private set; }
    
    public Guid TeamId { get; private set; }
    
    public Guid UserId { get; private set; }

    public TeamMember(Guid teamId, Guid userId)
    {
        TeamId = teamId;
        UserId = userId;
    }
}