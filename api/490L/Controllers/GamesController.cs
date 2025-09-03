using _490L.Models;
using Microsoft.AspNetCore.Mvc;

namespace _490L.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController
{
    private readonly IgdbClient _igdbClient;
    
    public GamesController(IgdbClient igdbClient)
    {
        _igdbClient = igdbClient;
    }
    
    [HttpPost]
    public async Task<GameNameResponseModel[]> FindGames(string name)
    {
        return await _igdbClient.QueryByGameName(name);
    }
}