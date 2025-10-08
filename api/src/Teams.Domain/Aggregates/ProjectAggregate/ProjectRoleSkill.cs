using Teams.Domain.SeedWork;
using Teams.Domain.SharedKernel;

namespace Teams.Domain.Aggregates.ProjectAggregate;

public class ProjectRoleSkill : Entity
{
    public int ProjectRoleId { get; private set; }
    
    public Guid SkillId { get; private set; }
    
    public Skill Skill { get; private set; }
    
    public Proficiency Proficiency { get; private set; }

    public ProjectRoleSkill(int projectRoleId, Guid skillId, Proficiency proficiency)
    {
        ProjectRoleId = projectRoleId;
        SkillId = skillId;
        Proficiency = proficiency;
    }
}