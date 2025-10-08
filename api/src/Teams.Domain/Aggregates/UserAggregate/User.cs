using Teams.Domain.SeedWork;
using Teams.Domain.SharedKernel;

namespace Teams.Domain.Aggregates.UserAggregate;

public class User //: Entity
{  
    public Guid Id { get; private set; }
    
    public string IdentityGuid { get; private set; }
    
    private readonly List<UserSkill> _userSkills;
    
    // public IReadOnlyCollection<UserSkill> UserSkills => _userSkills.AsReadOnly();

    protected User()
    {
        _userSkills = new List<UserSkill>();
    }
    
    public User(Guid id, string identityGuid) : this()
    {
        Id = id;
        IdentityGuid = !string.IsNullOrWhiteSpace(identityGuid)
            ? identityGuid 
            : throw new ArgumentNullException(nameof(identityGuid));
    }

    /// <remarks>
    /// Avoids passing in raw skill IDs, to ensure the `Skill` is valid.
    /// </remarks>
    /*
    public void AddSkill(Skill skill, Proficiency proficiency)
    {
        if (_userSkills.Any(s => s.SkillId == skill.Id))
            throw new InvalidOperationException($"Skill with ID {skill.Id} is already added.");
        
        _userSkills.Add(new UserSkill(Id, skill.Id, proficiency));
    }
    */

    /*
    public void UpdateSkillProficiency(int skillId, Proficiency newProficiency)
    {
        var userSkill = _userSkills.FirstOrDefault(s => s.SkillId == skillId);
        if (userSkill == null)
            throw new InvalidOperationException($"Skill with ID {skillId} not found.");
        
        userSkill.Proficiency = newProficiency;
    }

    public void RemoveSkill(int skillId)
    {
        var userSkill =  _userSkills.FirstOrDefault(s => s.SkillId == skillId);
        if (userSkill == null)
            throw new InvalidOperationException($"Skill with ID {skillId} not found.");
        
        _userSkills.Remove(userSkill);
    }
    */
}