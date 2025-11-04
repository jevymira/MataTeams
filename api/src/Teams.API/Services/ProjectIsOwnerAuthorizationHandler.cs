using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Teams.Domain.Aggregates.ProjectAggregate;

namespace Teams.API.Services;

public class ProjectIsOwnerAuthorizationHandler : AuthorizationHandler<IsOwnerRequirement, Project>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        IsOwnerRequirement requirement,
        Project project)
    {
        if (context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value == project.Owner.IdentityGuid)
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

public class IsOwnerRequirement : IAuthorizationRequirement { }