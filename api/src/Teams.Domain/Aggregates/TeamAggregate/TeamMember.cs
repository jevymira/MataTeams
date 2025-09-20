using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.TeamAggregate;

public class TeamMember : Entity
{
    public int RoleId { get; private set; }

    public TeamMember(int userId, int roleId)
    {
        Id = userId;
        RoleId = roleId;
    }
}