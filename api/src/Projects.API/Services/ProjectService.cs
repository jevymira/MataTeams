using Microsoft.EntityFrameworkCore;
using Projects.API.Infrastructure;
using Projects.API.Model;

namespace Projects.API.Services;

public class ProjectService(ProjectDbContext context)
{
    public async Task<List<Project>> GetProjectsAsync()
    {
        return await context.Projects.ToListAsync();
    }
}