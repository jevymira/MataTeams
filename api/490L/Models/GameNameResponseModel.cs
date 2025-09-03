using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _490L.Models;

public class GameNameResponseModel
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    public List<Genre>? Genres { get; set; }
}