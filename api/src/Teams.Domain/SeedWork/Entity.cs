namespace Teams.Domain.SeedWork;

public abstract class Entity
{
    public int Id
    {
        get;
        protected set; // accessible by EF via reflection
    }
}