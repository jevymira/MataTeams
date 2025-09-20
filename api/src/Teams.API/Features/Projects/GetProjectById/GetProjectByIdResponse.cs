namespace Teams.API.Features.Projects.GetProjectById;

public class GetProjectByIdResponse
{
    /// <summary>
    /// The unique identifier for the project.
    /// </summary> 
    public int Id { get; set; }

    public required string Name { get; set; }
   
    public required string Description { get; set; }
}