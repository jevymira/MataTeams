using Microsoft.AspNetCore.Identity;

namespace Entities;

public class MataTeamsUser : IdentityUser
{
    public ICollection<Project> Projects { get; set; }
}