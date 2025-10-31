using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Teams.Domain.Aggregates.ProjectAggregate;

namespace Teams.API.Services;

public class TeamIsLeaderAuthorizationHandler : AuthorizationHandler<IsLeaderRequirement, Team>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        IsLeaderRequirement requirement,
        Team resource)
    {
        if (context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value == resource.Leader.IdentityGuid)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        
        return Task.CompletedTask;
    }
}

public class IsLeaderRequirement : IAuthorizationRequirement { }