using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Domain.SharedKernel;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects.CreateProject;

public sealed record CreateProjectRequest(
    string Name,
    string Description,
    string Type, 
    string Status);

public sealed record CreateProjectCommand : IRequest<bool>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    
    public required string Type { get; init; }
    
    public required string Status { get; init; }
    
    /// <remarks>
    /// To retrieve the corresponding user's id within this context.
    /// </remarks>
    public required string OwnerIdentityGuid { get; init; }
}

public class CreateProjectEndpoint
{
    public static void Map(RouteGroupBuilder group) => group 
        .MapPost("", CreateProjectAsync)
        .RequireAuthorization()
        .WithSummary("Create a new project.");
    
    private static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> CreateProjectAsync(
        CreateProjectRequest request, ISender sender, IHttpContextAccessor accessor)
    {
        try
        {
            var result = await sender.Send(new CreateProjectCommand
            {
                Name = request.Name,
                Description = request.Description,
                Type = request.Type,
                Status = request.Status,
                OwnerIdentityGuid = accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            });

            if (!result)
            {
                return TypedResults.Problem(detail: "Create project failed to process.", statusCode: 500);
            }

            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }
}

internal sealed class CreateProjectCommandHandler(TeamDbContext context) : IRequestHandler<CreateProjectCommand, bool>
{
    public async Task<bool> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        // Output the corresponding ProjectStatus.
        Enum.TryParse<ProjectStatus>(request.Status, true, out var status);
        
        var owner = await context.Users
            .FirstOrDefaultAsync(m => m.IdentityGuid == request.OwnerIdentityGuid, cancellationToken);
       
        var project = new Project(
            request.Name,
            request.Description,
            ProjectType.FromName(request.Type),
            status, 
            owner!.Id);
        
        context.Projects.Add(project);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}