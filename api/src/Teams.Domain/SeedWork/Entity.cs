namespace Teams.Domain.SeedWork;

public abstract class Entity
{
    public Guid Id
    {
        get;
        protected set; // accessible by EF via reflection
    }
}