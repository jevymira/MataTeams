using System.Security.Claims;

namespace Teams.API.Services;

public interface IIdentityService
{
    string GetUserIdentity();

    string GetUserName();

    ClaimsPrincipal GetUser();
}