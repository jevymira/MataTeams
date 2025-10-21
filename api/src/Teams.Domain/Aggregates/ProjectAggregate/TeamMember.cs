using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.ProjectAggregate;

public class TeamMember : Entity
{
    public Guid TeamId { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public Guid ProjectRoleId { get; private set; }

    public TeamMember(Guid teamId, Guid userId, Guid projectRoleId)
    {
        TeamId = teamId;
        UserId = userId;
        ProjectRoleId = projectRoleId;
    }
}