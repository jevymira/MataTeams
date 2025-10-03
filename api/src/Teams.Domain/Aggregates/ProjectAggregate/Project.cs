using Teams.Domain.SeedWork;
using Teams.Domain.SharedKernel;

namespace Teams.Domain.Aggregates.ProjectAggregate;

public class Project : Entity
{
   public string Name { get; private set; }
   
   public string Description { get; private set; }
   
   public ProjectType Type { get; private set; }
   
   public ProjectStatus Status { get; private set; }
   
   // public bool AutoAllowTeamAcceptance
   
   private readonly List<ProjectRole> _roles;
   
   public IReadOnlyCollection<ProjectRole> ProjectRoles => _roles.AsReadOnly();
   
   // public ICollection<int> TeamIds { get; private set; }
   
   // private readonly List<Team> _teams;
   
   // public IReadOnlyCollection<Team> Teams { get; private set; }

   public int OwnerId { get; private set; }

   protected Project()
   {
      _roles = new List<ProjectRole>();
   }

   public Project(string name, string description, ProjectType type, ProjectStatus status, int ownerId) : this()
   {
      if (!type.AllowedStatuses.Contains(status))
         throw new InvalidOperationException("Invalid status for project type.");
      
      Name = name;
      Description = description;
      Type = type;
      Status = status;
      OwnerId = ownerId;
   }

   public ProjectRole AddProjectRole(int roleId)
   {
      var projectRole = new ProjectRole(Id, roleId);
      _roles.Add(projectRole);
      return projectRole;
   }
}