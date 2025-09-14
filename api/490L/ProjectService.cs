using Entities;
using Microsoft.EntityFrameworkCore;

namespace _490L;

public class ProjectService(MataTeamsContext context)
{
    public async Task<List<Project>> GetProjectsAsync()
    {
        return await context.Projects.ToListAsync();
    }
}