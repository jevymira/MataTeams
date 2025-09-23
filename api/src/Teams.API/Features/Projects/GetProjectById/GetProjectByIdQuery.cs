using MediatR;

namespace Teams.API.Features.Projects.GetProjectById;

public record GetProjectByIdQuery(int Id) : IRequest<GetProjectByIdResponse>;