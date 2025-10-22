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

   protected Project()
   {
      _roles = new List<ProjectRole>();
      _teams = new List<Team>();
   }

   public Project(Guid id, string name, string description, ProjectType type, ProjectStatus status, Guid ownerId) : this()
   {
      if (!type.AllowedStatuses.Contains(status))
         throw new InvalidOperationException("Invalid status for project type.");

      Id = id;
      Name = name;
      Description = description;
      Type = type;
      Status = status;
      OwnerId = ownerId;
   }

   public ProjectRole AddProjectRole(Guid projectRoleId, Guid roleId, int positionCount)
   {
      var projectRole = new ProjectRole(projectRoleId, Id, roleId, positionCount);
      _roles.Add(projectRole);
      return projectRole;
   }

   public Team? AddTeamToProject(Guid userId)
   {
      // TODO: check flag for open creation of teams by users other than project owner

      if (userId != OwnerId) // TODO: extract out into handler, to be invoked at service level
      {
         return null;
      }
      
      var team = new Team(Id, userId);
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

   public TeamMembershipRequest RespondToMembershipRequest(Guid membershipRequestId, TeamMembershipRequestStatus newStatus)
   {
      var team = _teams
         .FirstOrDefault(t => t.MembershipRequests
            .Any(m => m.Id == membershipRequestId))
         ?? throw new KeyNotFoundException($"Membership request not found with id: {membershipRequestId}.");

      var projectRoleId = team.MembershipRequests
         .FirstOrDefault(m => m.Id == membershipRequestId)!
         .ProjectRoleId;
      
      var positionLimit = _roles
         .FirstOrDefault(r => r.Id == projectRoleId)!
         .PositionCount;
      
      return team.RespondToMembershipRequest(membershipRequestId, newStatus, positionLimit);
   }
}