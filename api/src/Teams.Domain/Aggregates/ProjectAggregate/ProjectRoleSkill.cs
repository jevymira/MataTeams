using Teams.Domain.SharedKernel;

namespace Teams.Domain.Aggregates.ProjectAggregate;

public class ProjectRoleSkill
{
    public int ProjectRoleId { get; private set; }
    
    public int SkillId { get; private set; }
    
    public Skill Skill { get; private set; }
    
    public Proficiency Proficiency { get; private set; }

    public ProjectRoleSkill(int projectRoleId, int skillId, Proficiency proficiency)
    {
        ProjectRoleId = projectRoleId;
        SkillId = skillId;
        Proficiency = proficiency;
    }
}