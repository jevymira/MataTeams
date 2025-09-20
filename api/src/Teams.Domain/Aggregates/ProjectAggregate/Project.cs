using Teams.Domain.SeedWork;

namespace Teams.Domain.Aggregates.ProjectAggregate;

public class Project : Entity
{
   public string Name { get; private set; }
   
   public  string Description { get; private set; }
   
   // public ProjectType ProjectType { get; set; }
   
   // public List<Skill> Skills { get; set; }
   
   // public ProjectStatus ProjectStatus { get; set; }
   
   // public int TeamId { get; set }
   
   // public int CreatedBy { get; set; }

   public Project(string name, string description)
   {
      Name = name;
      Description = description;
   }
}