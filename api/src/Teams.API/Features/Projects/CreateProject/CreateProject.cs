using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects.CreateProject;

public sealed record CreateProjectRequest(
    string Name,
    string Description);

public sealed record CreateProjectCommand : IRequest<bool>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    
    /// <remarks>
    /// To retrieve the corresponding user's id within this context.
    /// </remarks>
    public required string OwnerIdentityGuid { get; init; }
}

public class CreateProjectEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapPost("api/projects", CreateProjectAsync)
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

internal sealed class CreateProjectCommandHandler(TeamDbContext context) : IRequestHandler<CreateProjectCommand, bool>
{
    public async Task<bool> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var owner = await context.Members
            .FirstOrDefaultAsync(m => m.IdentityGuid == request.OwnerIdentityGuid, cancellationToken);
        
        var project = new Project(request.Name, request.Description, owner!.Id);
        
        context.Projects.Add(project);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}