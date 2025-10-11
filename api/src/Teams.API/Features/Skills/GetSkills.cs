using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Teams.Infrastructure;

namespace Teams.API.Features.Skills;

public sealed record GetSkillsQuery : IRequest<List<SkillViewModel>>;

public class GetSkillsEndpoint
{
    public static void Map(RouteGroupBuilder group) => group
        .MapGet("", GetSkillsAsync)
        .WithSummary("Get all skills.");

    private static async Task<Ok<List<SkillViewModel>>> GetSkillsAsync(ISender sender)
    {
        var skills = await sender.Send(new GetSkillsQuery());
        return TypedResults.Ok(skills);
    }
}

internal sealed class GetSkillsQueryHandler(TeamDbContext context)
    : IRequestHandler<GetSkillsQuery, List<SkillViewModel>>
{
    public async Task<List<SkillViewModel>> Handle(
        GetSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await context.Skills
            .ToListAsync(cancellationToken);

        return skills
            .Select(s => new SkillViewModel
            {
                Id = s.Id.ToString(),
                Name = s.Name,
            })
            .ToList();
    }
}