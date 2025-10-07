using Teams.Domain.SeedWork;

namespace Teams.Domain.SharedKernel;

public class Role : Entity
{
    public string Name { get; private set; }

    public Role(string name)
    {
        Name = name;
    }
}