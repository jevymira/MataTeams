using Teams.Domain.SeedWork;

namespace Teams.Domain.SharedKernel;

public class Skill : Entity
{
    public string Name { get; private set; }

    public Skill(string name)
    {
        Name = name;
    }
    
    // Implementation for .Except().
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    
    // Implementation for .Except().
    public override bool Equals(object? obj)
    {
        if (!(obj is Skill))
            throw new ArgumentException("obj is not a Skill.");
        var skill = obj as Skill;
        if (skill == null)
            return false;
        return Id.Equals(skill.Id);
    }
}