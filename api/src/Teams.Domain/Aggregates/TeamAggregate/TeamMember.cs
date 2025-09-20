using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.TeamAggregate;

public class TeamMember : Entity
{
    public int TeamId { get; private set; }
    
    public int UserId { get; private set; }
        
    public int RoleId { get; private set; }

    public TeamMember(int teamId, int userId, int roleId)
    {
        RoleId = roleId;
    }
}