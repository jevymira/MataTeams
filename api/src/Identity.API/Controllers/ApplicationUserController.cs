using EventBus.Commands;
using Identity.API.Dtos;
using Identity.API.Model;
using Identity.API.Services;
using MassTransit;
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
    private readonly IBus _bus;

    public ApplicationUserController(
        ILogger<ApplicationUserController> logger,
        UserManager<ApplicationUser> userManager,
        JwtManager jwtManager,
        IBus bus)
    {
        _logger = logger;
        _userManager = userManager;
        _jwtManager = jwtManager;
        _bus = bus;
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LoginAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
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

    [HttpPost("/register")]
    public async Task RegisterAsync()
    {
        var guid = Guid.NewGuid().ToString(); // PLACEHOLDER

        var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/create-user"));

        await endpoint.Send<CreateUser>(new { IdentityGuid = guid });
    }
}