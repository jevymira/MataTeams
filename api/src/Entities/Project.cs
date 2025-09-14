using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

[Table("projects")]
public class Project
{
   /// <summary>
   /// The unique identifier for the project.
   /// </summary>
   [Column("id")]
   public int Id { get; set; }

   /// <summary>
   /// The unique identifier of the user who created the project
   /// and who has the highest permissions by default.
   /// </summary>
   [Column("owner_id")]
   public required string OwnerUserId { get; set; }
}