namespace Projects.API.Model;

public class ProjectGetResponseModel
{
    /// <summary>
    /// The unique identifier for the project.
    /// </summary> 
    public int Id { get; set; }
    
    /// <summary>
    /// The unique identifier of the user who created the project
    /// and who has the highest permissions by default.
    /// </summary>
    public required string OwnerUserId { get; set; }
}