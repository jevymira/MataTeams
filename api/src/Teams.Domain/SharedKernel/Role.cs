using Teams.Domain.SeedWork;

namespace Teams.Domain.SharedKernel;

public class Role //: Entity
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; }

    public Role(string name)
    {
        Name = name;
    }
}