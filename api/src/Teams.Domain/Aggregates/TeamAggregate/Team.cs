using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.TeamAggregate;

public class Team : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    
    // Private collection fields; for rationale, refer to MS reference repository at
    // https://github.com/dotnet/eShop/blob/main/src/Ordering.Domain/AggregatesModel/OrderAggregate/Order.cs

    private readonly List<TeamMember> _members;
    
    public IReadOnlyCollection<TeamMember> Members => _members.AsReadOnly();
    
    private readonly List<TeamMember> _membershipRequests;

    public Team(string name)
    {
        Name = name;
        _members = [];
        _membershipRequests = [];
    }

    /*
    public void AddMember(int userId, int roleId)
    {
        var existingMember = _members.SingleOrDefault(m => m.Id == userId);
        
        if (existingMember != null)
        {
            //
        }
        else
        {
            // TODO: validate that member is not requesting to join a fully-filled role 
            var newMember = new TeamMember(Id, userId);
            _members.Add(newMember);
        }
    }
    */
}