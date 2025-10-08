using Teams.Domain.SeedWork;

namespace Teams.Domain.SharedKernel;

public class Skill // : Entity
{
    public Guid Id { get; private set; }
    
    public string Name { get; private set; }

    public Skill(string name)
    {
        Name = name;
    }
}