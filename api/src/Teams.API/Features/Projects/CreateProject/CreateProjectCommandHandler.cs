using MediatR;
using Teams.Domain.Aggregates.ProjectAggregate;
using Teams.Infrastructure;

namespace Teams.API.Features.Projects.CreateProject;

public class CreateProjectCommandHandler(TeamDbContext context) : IRequestHandler<CreateProjectCommand, bool>
{
    public async Task<bool> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Name, request.Description);
        
        context.Projects.Add(project);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}