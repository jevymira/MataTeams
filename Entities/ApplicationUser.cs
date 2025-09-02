using Microsoft.AspNetCore.Identity;

namespace Entities;

public class ApplicationUser : IdentityUser
{
    public ICollection<Review> Reviews { get; set; }
}