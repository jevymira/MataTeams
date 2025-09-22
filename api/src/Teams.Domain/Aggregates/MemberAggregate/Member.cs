using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.MemberAggregate;

public class Member : Entity
{  
    public string IdentityGuid { get; private set; }

    public Member(string identityGuid)
    {
        IdentityGuid = !string.IsNullOrWhiteSpace(identityGuid)
            ? identityGuid 
            : throw new ArgumentNullException(nameof(identityGuid));
    }
}