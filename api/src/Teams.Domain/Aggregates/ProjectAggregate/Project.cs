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
   
   // public ICollection<int> TeamIds { get; private set; }
   
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

   public ProjectRole AddProjectRole(Guid roleId, int positionCount)
   {
      var projectRole = new ProjectRole(Id, roleId, positionCount);
      _roles.Add(projectRole);
      return projectRole;
   }

   public Team? AddTeamToProject(Guid leaderId)
   {
      // TODO: check flag for open creation of teams by users other than project owner
      
      var team = new Team(Id, leaderId);
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
      
      return team.RespondToMembershipRequest(membershipRequestId, newStatus, positionLimit);
   }
}