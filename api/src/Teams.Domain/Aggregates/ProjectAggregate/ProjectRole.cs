using Teams.Domain.SeedWork;
using Teams.Domain.SharedKernel;

namespace Teams.Domain.Aggregates.ProjectAggregate;

/// <remarks>
/// An Entity (with a surrogate key) instead of a Value Object;
/// it retains its identity despite, e.g., Skills being added/removed.
/// </remarks>
public class ProjectRole : Entity
{
    public Guid ProjectId { get; private set; }
    
    public Guid RoleId { get; private set; }
    
    public Role Role { get; private set; }
    
    /// <summary>
    // The maximum number of positions for a role,
    // each to be "filled" by a team member.
    /// </summary>
    public int PositionCount { get; private set; }
    
    private readonly List<Skill> _skills;
    
    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();

    protected ProjectRole()
    {
        _skills = new List<Skill>();
    }
    
    public ProjectRole(Guid projectId, Guid roleId, int positionCount) : this()
    {
        Id = Guid.CreateVersion7();
        ProjectId = projectId;
        RoleId = roleId;
        PositionCount = positionCount;
    }

    public void AddProjectSkill(Skill skill)
    {
        _skills.Add(skill);
    }
}