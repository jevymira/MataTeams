namespace Projects.API.Model;

public class Project
{
   /// <summary>
   /// The unique identifier for the project.
   /// </summary>
   public int Id { get; set; }

   public required string Name { get; set; }
   
   public required string Description { get; set; }
   
   // public ProjectType ProjectType { get; set; }
   
   // public List<Skill> Skills { get; set; }
   
   // public ProjectStatus ProjectStatus { get; set; }
   
   // public int TeamId { get; set }
   
   // public int CreatedBy { get; set; }
}