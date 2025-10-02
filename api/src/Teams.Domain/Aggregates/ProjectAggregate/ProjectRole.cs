using Teams.Domain.SeedWork;
using Teams.Domain.SharedKernel;

namespace Teams.Domain.Aggregates.ProjectAggregate;

public class ProjectRole : Entity
{
    public int ProjectId { get; private set; }
    
    public int RoleId { get; private set; }
    
    public Role Role { get; private set; }
    
    private readonly List<ProjectRoleSkill> _skills;
    
    public IReadOnlyCollection<ProjectRoleSkill> Skills => _skills.AsReadOnly();

    public ProjectRole(int projectId, int roleId) 
    {
        ProjectId = projectId;
        RoleId = roleId;
    }
}