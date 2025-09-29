using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.UserAggregate;

public class User : Entity
{  
    public string IdentityGuid { get; private set; }

    public User(string identityGuid)
    {
        IdentityGuid = !string.IsNullOrWhiteSpace(identityGuid)
            ? identityGuid 
            : throw new ArgumentNullException(nameof(identityGuid));
    }
}