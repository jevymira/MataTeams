using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.ProjectAggregate;

public class TeamMembershipRequest : Entity
{
    public Guid TeamId { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public Guid ProjectRoleId { get; private set; }
    
    public TeamMembershipRequestStatus Status { get; private set; }

    public TeamMembershipRequest(Guid teamId, Guid userId, Guid projectRoleId)
    {
        TeamId = teamId;
        UserId = userId;
        ProjectRoleId = projectRoleId;
        Status = TeamMembershipRequestStatus.Pending;
    }
    
    public TeamMembershipRequest UpdateStatus(TeamMembershipRequestStatus status)
    {
        Status = status;
        return this;
    }
}