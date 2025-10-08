using Teams.Domain.SeedWork;
using Teams.Domain.SharedKernel;

namespace Teams.Domain.Aggregates.ProjectAggregate;

/// <remarks>
/// An Entity (with a surrogate key) instead of a Value Object;
/// it retains its identity despite, e.g., Skills being added/removed.
/// </remarks>
public class ProjectRole // : Entity
{
    public Guid Id { get; private set; }
    
    public Guid ProjectId { get; private set; }
    
    public Guid RoleId { get; private set; }
    
    public Role Role { get; private set; }
    
    public int PositionCount { get; private set; }
    
    private readonly List<ProjectRoleSkill> _skills;
    
    public IReadOnlyCollection<ProjectRoleSkill> Skills => _skills.AsReadOnly();

    protected ProjectRole()
    {
        _skills = new List<ProjectRoleSkill>();
    }
    
    public ProjectRole(Guid projectId, Guid roleId, int positionCount) : this()
    {
        ProjectId = projectId;
        RoleId = roleId;
        PositionCount = positionCount;
    }

    public void AddProjectSkill(Guid skillId, Proficiency proficiency)
    {
        var skill = new ProjectRoleSkill(Id, skillId, proficiency);
        _skills.Add(skill);
    }
}