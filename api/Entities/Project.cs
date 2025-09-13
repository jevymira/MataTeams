using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

[Table("projects")]
public class Project
{
   [Column("id")]
   public int Id { get; set; }

   [Column("user_id")]
   public required string UserId { get; set; }
}