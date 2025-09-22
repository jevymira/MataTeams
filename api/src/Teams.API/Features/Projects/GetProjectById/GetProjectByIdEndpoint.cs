using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Teams.API.Features.Projects.GetProjectById;

public static class GetProjectByIdEndpoint
{
    public static void Map(IEndpointRouteBuilder app) => app
        .MapGet("api/project/{id}", GetProjectAsync)
        .WithSummary("Get a project by its unique identifier.");

    private static async Task<Results<Ok<GetProjectByIdResponse>, NotFound>> GetProjectAsync(
        int id, ISender sender)
    {
        try
        {
            var project = await sender.Send(new GetProjectByIdQuery(id));
            return TypedResults.Ok(project);
        }
        catch
        {
            return TypedResults.NotFound();
        }
    }
}