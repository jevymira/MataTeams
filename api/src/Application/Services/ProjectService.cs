using Application.Data;
using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ProjectService(MataTeamsContext context)
{
    public async Task<List<Project>> GetProjectsAsync()
    {
        return await context.Projects.ToListAsync();
    }
}