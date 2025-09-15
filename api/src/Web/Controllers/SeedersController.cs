using Application;
using Application.Data;
using Application.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _490L.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedersController : ControllerBase
{
    private readonly UserManager<MataTeamsUser> _userManager;
    private readonly MataTeamsContext _context;
    private readonly IConfiguration _configuration;

    public SeedersController(
        UserManager<MataTeamsUser> userManager,
        MataTeamsContext mataTeamsContext,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _context = mataTeamsContext;
        _configuration = configuration;
    }
    
    [HttpPost("Users")]
    public async Task SeedUser()
    {
        var user = new MataTeamsUser()
        {
            UserName = "user",
            Email = "user@email.com",
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        
        await _userManager.CreateAsync(user, _configuration["SeedUser:Password"]);
        await _context.SaveChangesAsync();
    }
}