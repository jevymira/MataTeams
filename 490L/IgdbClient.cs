using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using _490L.Models;

namespace _490L;

public class IgdbClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    
    public IgdbClient(
        HttpClient httpClient,
        IConfiguration config,
        ILogger<IgdbClient> logger)
    {
        _httpClient = httpClient;
        // Base address requires trailing `/`, see: https://stackoverflow.com/a/23438417
        _httpClient.BaseAddress = new Uri("https://api.igdb.com/v4/");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Client-ID", config["IGDB:ClientId"]);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config["IGDB:AccessToken"]}");
        _logger = logger;
    }

    public async Task<GameNameResponseModel[]> QueryByGameName(string name)
    {
        HttpContent content = new StringContent($"fields name, genres.name; search \"{name}\";",
            Encoding.UTF8, "text/plain");
        using HttpResponseMessage response = await _httpClient.PostAsync("games", content);
        Stream responseBody = response.Content.ReadAsStream();
        // Adapted from: https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/customize-properties?pivots=dotnet-5-0
        // to resolve IGDB's camelCase naming with .NET's Pascal Case convention.
        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        try
        {
            var games = await JsonSerializer
                .DeserializeAsync<GameNameResponseModel[]>(responseBody, serializeOptions);
            return (games is null) ? [] : games;
        }
        catch (JsonException)
        {
            _logger.LogError(response.Content.ToString());
            return [];
        }
        
    }
}