using Microsoft.EntityFrameworkCore;
using Projects.API.Infrastructure;
using Projects.API.Model;

namespace Projects.API.Services;

public class ProjectService(ProjectDbContext context)
{
    public async Task<List<ProjectGetResponseModel>> GetProjectsAsync()
    {
        var projects = await context.Projects.ToListAsync();
        return projects.Select(p => new ProjectGetResponseModel
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description
        }).ToList();
    }
}