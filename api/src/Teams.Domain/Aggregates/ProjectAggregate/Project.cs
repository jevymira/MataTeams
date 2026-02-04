using Teams.Domain.Aggregates.UserAggregate;
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
   
   public IReadOnlyCollection<ProjectRole> Roles => _roles.AsReadOnly();
   
   private readonly List<Team> _teams;
   
   public IReadOnlyCollection<Team> Teams => _teams.AsReadOnly();

   public Guid OwnerId { get; private set; }
   
   public User Owner { get; private set; }

   protected Project()
   {
      _roles = new List<ProjectRole>();
      _teams = new List<Team>();
   }

   public Project(string name, string description, ProjectType type, ProjectStatus status, Guid ownerId) : this()
   {
      if (!type.AllowedStatuses.Contains(status))
         throw new InvalidOperationException("Invalid status for project type.");

      Id = Guid.CreateVersion7();
      Name = name;
      Description = description;
      Type = type;
      Status = status;
      OwnerId = ownerId;
   }

   public void Rename(string newName)
   {
      Name = newName;
   }

   public void ChangeDescription(string newDescription)
   {
      Description = newDescription;
   }

   public void SetType(ProjectType type)
   {
      Type = type;
   }

   public void SetStatus(ProjectStatus status)
   {
      Status = status;
   }

   public ProjectRole AddProjectRole(Guid roleId, int positionCount, List<Skill> skills)
   {
      var projectRole = new ProjectRole(Id, roleId, positionCount, skills);
      _roles.Add(projectRole);
      return projectRole;
   }

   public void RemoveRole(Guid projectRoleId)
   {
      if (_teams.Any(t => t.Members.Any(m => m.ProjectRoleId == projectRoleId)))
      {
         throw new InvalidOperationException("Cannot delete project role with assigned team members.");
      }

      var role = _roles.SingleOrDefault(t => t.Id == projectRoleId);
      
      if (role is not null)
      {
         _roles.Remove(role);
      }
   }

   public void UpdateRole(Guid projectRoleId, Guid roleId, int positionCount, List<Skill> skills)
   {
      var role =  _roles.SingleOrDefault(t => t.Id == projectRoleId);
      role?.Update(roleId, positionCount, skills);
   }
   
   /// <summary>
   /// Remove those teams excluded from the list of teams to retain.
   /// </summary>
   public void RemoveExcludedTeams(IEnumerable<Guid> teamsToRetainIds)
   {
      _teams.RemoveAll(t => !teamsToRetainIds.Contains(t.Id));
   }

   public Team? AddTeamToProject(string teamName, Guid leaderId)
   {
      // TODO: check flag for open creation of teams by users other than project owner
      
      var team = new Team(teamName, Id, leaderId);
      _teams.Add(team);
      return team;
   }

   public TeamMembershipRequest AddTeamMembershipRequest(Guid teamId, Guid userId, Guid projectRoleId)
   {
      // TODO: validate that the role is not already at capacity
      var team = _teams.FirstOrDefault(t => t.Id == teamId)
         ?? throw new KeyNotFoundException($"{nameof(Team)} not found with id: {teamId}.");
      return team.AddMembershipRequest(userId, projectRoleId);
   }

   public TeamMembershipRequest RespondToMembershipRequest(
      Guid userId,
      Guid membershipRequestId,
      TeamMembershipRequestStatus newStatus)
   {
      var team = _teams
         .FirstOrDefault(t => t.MembershipRequests
            .Any(m => m.Id == membershipRequestId))
         ?? throw new KeyNotFoundException($"Membership request not found with id: {membershipRequestId}.");

      // Validate that the user providing the response has team-level permissions
      // (i.e., is Project Owner)
      if (userId != team.LeaderId)
      {
         throw new UnauthorizedAccessException("User lacks team-level permissions to manage membership requests " +
                                               "(is not team leader).");
      }
      
      var projectRoleId = team.MembershipRequests
         .FirstOrDefault(m => m.Id == membershipRequestId)!
         .ProjectRoleId;
      
      var positionLimit = _roles
         .FirstOrDefault(r => r.Id == projectRoleId)!
         .PositionCount;
      
      return team.RespondToMembershipRequest(membershipRequestId, projectRoleId, newStatus, positionLimit);
   }
}