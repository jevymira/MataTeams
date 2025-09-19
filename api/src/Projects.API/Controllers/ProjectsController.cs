using Microsoft.AspNetCore.Mvc;
using Projects.API.Model;
using Projects.API.Services;

namespace Projects.API.Controllers;

/// <summary>
/// Controller for Project entities. 
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProjectsController(ProjectService projectService) : ControllerBase
{
    /// <summary>
    /// Retrieves all projects.
    /// </summary>
    /// <returns>List of projects.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Project>>> GetProjectsAsync()
    {
        var projects = await projectService.GetProjectsAsync();
        var projectResponseModels = 
            projects.Select(p => new ProjectGetResponseModel()
        {
            Id = p.Id,
            OwnerUserId = p.OwnerUserId,
        });
        return Ok(projectResponseModels);
    }
}