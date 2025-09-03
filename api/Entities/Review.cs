using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

[Table("reviews")]
public class Review
{
   [Column("id")]
   public int Id { get; set; }
   
   [Column("review_body")]
   public string Body { get; set; }
   
   [Column("user_id")]
   public string UserId { get; set; }
}