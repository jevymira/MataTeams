using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.ProjectAggregate;

public class Team : Entity
{
    public Guid ProjectId { get; private set; }
    
    public Guid LeaderId { get; private set; }
    
    // Private collection fields; for rationale, refer to MS reference repository at
    // https://github.com/dotnet/eShop/blob/main/src/Ordering.Domain/AggregatesModel/OrderAggregate/Order.cs
    
    private readonly List<TeamMember> _members;
    
    public IReadOnlyCollection<TeamMember> Members => _members.AsReadOnly();
    
    private readonly List<TeamMembershipRequest> _membershipRequests;
    
    public IReadOnlyCollection<TeamMembershipRequest> MembershipRequests => _membershipRequests.AsReadOnly();

    public Team(Guid projectId, Guid leaderId)
    {
        ProjectId = projectId;
        LeaderId = leaderId;
        _members = [];
        _membershipRequests = [];
    }

    public TeamMembershipRequest AddMembershipRequest(Guid userId, Guid projectRoleId)
    {
        // TODO: validate that the user is not already a team member
        var request = new TeamMembershipRequest(Id, userId, projectRoleId);
        _membershipRequests.Add(request);
        return request;
    }

    public TeamMembershipRequest RespondToMembershipRequest(Guid requestId, TeamMembershipRequestStatus status)
    {
        var request = _membershipRequests.FirstOrDefault(r => r.Id == requestId);
        return request!.UpdateStatus(status);
    }
}