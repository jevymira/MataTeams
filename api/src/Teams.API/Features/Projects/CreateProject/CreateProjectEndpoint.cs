using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Teams.API.Features.Projects.CreateProject;

public static class CreateProjectEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("api/project", CreateProjectAsync)
        .RequireAuthorization()
        .WithSummary("Create a new project.");

    private static async Task<Results<Ok, BadRequest<string>>> CreateProjectAsync(
        CreateProjectRequest request, ISender sender, IHttpContextAccessor accessor)
    {
        var result = await sender.Send(new CreateProjectCommand
        {
            Name = request.Name,
            Description = request.Description,
            OwnerIdentityGuid = accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        });
        
       return TypedResults.Ok(); 
    }
}

public record CreateProjectRequest(
    string Name,
    string Description
);