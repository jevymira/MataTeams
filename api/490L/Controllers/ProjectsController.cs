using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _490L.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly MataTeamsContext _context;

    public ProjectsController(MataTeamsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<Project>> GetProjectsAsync()
    {
        return await _context.Projects.ToListAsync();
    }
}