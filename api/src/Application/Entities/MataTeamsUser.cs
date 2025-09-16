using Microsoft.AspNetCore.Identity;

namespace Application.Entities;

public class MataTeamsUser : IdentityUser
{
    public ICollection<Project> Projects { get; set; }
}