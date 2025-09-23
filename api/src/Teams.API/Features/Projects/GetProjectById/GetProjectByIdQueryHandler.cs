using MediatR;
using Microsoft.EntityFrameworkCore;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects.GetProjectById;

public class GetProjectByIdQueryHandler(TeamDbContext context) : IRequestHandler<GetProjectByIdQuery, GetProjectByIdResponse>
{
    public async Task<GetProjectByIdResponse> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await context.Projects
            .FirstOrDefaultAsync(p => p.Id == request.Id);

        if (project is null)
        {
            throw new KeyNotFoundException();
        }

        return new GetProjectByIdResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
        };
    } 
}