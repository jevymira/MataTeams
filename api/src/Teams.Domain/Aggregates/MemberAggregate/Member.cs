using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.MemberAggregate;

public class Member : Entity
{  
    public string IdentityGuid { get; private set; }

    public Member(string identity)
    {
        IdentityGuid = !string.IsNullOrWhiteSpace(identity)
            ? identity
            : throw new ArgumentNullException(nameof(identity));
    }
}