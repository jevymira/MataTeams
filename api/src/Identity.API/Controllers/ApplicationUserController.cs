using Identity.API.Dtos;
using Identity.API.Model;
using Identity.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationUserController : ControllerBase
{
    private readonly ILogger<ApplicationUserController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtManager _jwtManager;

    public ApplicationUserController(
        ILogger<ApplicationUserController> logger,
        UserManager<ApplicationUser> userManager,
        JwtManager jwtManager)
    {
        _logger = logger;
        _userManager = userManager;
        _jwtManager = jwtManager;
    }

    // [HttpPost("/login")]
    // public async Task<IActionResult> LoginAsync(string userName, string password)
    // {
    //     var user = await _userManager.FindByNameAsync(userName);

    //     if (user == null || !await _userManager.CheckPasswordAsync(user, password))
    //     {
    //         return Unauthorized("Invalid username or password");
    //     }

    //     var token = await _jwtManager.GetJwtAsync(user);

    //     var response = new LoginResponse
    //     {
    //         Success = true,
    //         Message = "Login success.",
    //         Token = token,
    //     };

    //     return Ok(response);
    // }
    
    [HttpPost("/login")]
public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
{
    var user = await _userManager.FindByNameAsync(request.UserName);

    if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
    {
        return Unauthorized("Invalid username or password");
    }

    var token = await _jwtManager.GetJwtAsync(user);

    var response = new LoginResponse
    {
        Success = true,
        Message = "Login success.",
        Token = token,
    };

    return Ok(response);
}

}