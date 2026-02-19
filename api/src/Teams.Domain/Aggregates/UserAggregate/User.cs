using Teams.Domain.SeedWork;
using Teams.Domain.SharedKernel;

namespace Teams.Domain.Aggregates.UserAggregate;

public class User : Entity
{  
    public string IdentityGuid { get; private set; }
    
    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }
    
    public bool IsFacultyOrStaff { get; private set; }
    
    private readonly List<string> _programs;
    
    /// <summary>
    /// Academic program(s) of involvement, e.g., Computer Science.
    /// For a complete list, see https://www.csun.edu/node/11001/academic-programs
    /// </summary>
    /// <remarks>
    /// "Program" is term that works for students as well as faculty,
    /// and that tonally suits areas also considered art forms (e.g., "Film").
    /// </remarks>
    public IReadOnlyCollection<string> Programs => _programs.AsReadOnly();
    
    private readonly List<Skill> _skills;
    
    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();

    protected User()
    {
        _programs = new List<string>();
        _skills = new List<Skill>();
    }
    
    public User(Guid id, string firstname, string lastname, bool isFacultyOrStaff, string identityGuid) : this()
    {
        Id = id;
        FirstName = firstname;
        LastName = lastname;
        IsFacultyOrStaff = isFacultyOrStaff;
        IdentityGuid = !string.IsNullOrWhiteSpace(identityGuid)
            ? identityGuid 
            : throw new ArgumentNullException(nameof(identityGuid));
    }

    public void ChangeFirstName(string firstName)
    {
        FirstName = firstName;
    }

    public void ChangeLastName(string lastName)
    {
        LastName = lastName;
    }

    /// <remarks>
    /// Avoids passing in raw skill IDs, to ensure the `Skill` is valid.
    /// </remarks>
    public void AddSkill(Skill skill)
    {
        if (_skills.Any(s => s.Id == skill.Id))
            throw new InvalidOperationException($"Skill with ID {skill.Id} is already added.");
        
        _skills.Add(skill);
    }

    public void RemoveSkill(Skill skill)
    {
        var userSkill =  _skills.FirstOrDefault(s => s.Id == skill.Id);

        if (userSkill is not null)
        {
            _skills.Remove(userSkill);
        }
    }
}