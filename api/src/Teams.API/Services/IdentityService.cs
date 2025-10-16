using System.Security.Claims;

namespace Teams.API.Services;

/// <remarks>
/// Abstracts HttpContext for ease of mocking
/// and to avoid application-level dependency on web infrastructure.
/// Adapted from the eShopOnContainers reference application repository:
/// https://github.com/dotnet/eShop/blob/main/src/Ordering.API/Infrastructure/Services/IdentityService.cs
/// </remarks>
public class IdentityService(IHttpContextAccessor context) : IIdentityService
{
    public string GetUserIdentity()
        => context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        // TODO: refactor into: context.HttpContext?.User.FindFirst("sub")?.Value;
        // requires corresponding change to be made in Identity.API.
    
    public string GetUserName()
        => context.HttpContext?.User.Identity?.Name;
}