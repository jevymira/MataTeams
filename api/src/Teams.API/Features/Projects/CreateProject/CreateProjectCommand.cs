using MediatR;

namespace Teams.API.Features.Projects.CreateProject;

public sealed record CreateProjectCommand : IRequest<bool>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string OwnerIdentityGuid { get; set; }
}