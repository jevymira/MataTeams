using Teams.Domain.SeedWork;
using Teams.Domain.SharedKernel;

namespace Teams.Domain.Aggregates.UserAggregate;

public class UserSkill
{
    public int UserId { get; private set; }
    public int SkillId { get; private set; }
    public Skill Skill { get; private set; } // set by EF Core after construction
    public Proficiency Proficiency { get; set; }

    public UserSkill(int userId, int skillId, Proficiency proficiency)
    {
        UserId = userId;
        SkillId = skillId;
        Proficiency = proficiency;
    }
}