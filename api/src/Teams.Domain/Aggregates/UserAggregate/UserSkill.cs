using Teams.Domain.SeedWork;
using Teams.Domain.SharedKernel;

namespace Teams.Domain.Aggregates.UserAggregate;

public class UserSkill : Entity
{
    public Guid UserId { get; private set; }
    public Guid SkillId { get; private set; }
    public Skill Skill { get; private set; } // set by EF Core after construction
    public Proficiency Proficiency { get; set; }

    public UserSkill(Guid id, Guid userId, Guid skillId, Proficiency proficiency)
    {
        Id = id;
        UserId = userId;
        SkillId = skillId;
        Proficiency = proficiency;
    }
}