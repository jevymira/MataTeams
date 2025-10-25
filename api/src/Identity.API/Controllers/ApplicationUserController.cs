using Identity.API.Dtos;
using Identity.API.Model;
using Identity.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Identity.API.Dtos.LoginRequest;

namespace Identity.API.Controllers;

[ApiController]
[Route("api/auth")]
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
    
    [HttpPost("login")]
    [HttpPost("/login")] // Deprecated.
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

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequest request)
    {
        // By default in Identity, UserName and Email are the same value.
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is not null)
        {
            // Re: catch-all 200 OK https://stackoverflow.com/a/53144807
            return Ok(new RegistrationResponse
            {
                Success = false,
                Message = "Email already in use.",
                Token = null
            });
        }

        user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };
        
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new RegistrationResponse
            {
                Success = false,
                Message = result.Errors.First().Description,
                Token = null
            });
        }
    
        var token = await _jwtManager.GetJwtAsync(user);
        
        return Ok(new RegistrationResponse
        {
            Success = true,
            Message = "Registration success.",
            Token = token
        });
        
    }
}