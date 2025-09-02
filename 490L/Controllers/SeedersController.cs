using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _490L.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ReviewContext _context;
    private readonly IConfiguration _configuration;

    public SeedersController(
        UserManager<ApplicationUser> userManager,
        ReviewContext reviewContext,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _context = reviewContext;
        _configuration = configuration;
    }
    
    [HttpPost("Users")]
    public async Task SeedUser()
    {
        ApplicationUser user = new()
        {
            UserName = "user",
            Email = "user@email.com",
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        
        var x = await _userManager.CreateAsync(user, "P@ssw0rd!");
        var y = await _context.SaveChangesAsync();
    }
}