using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public int Age { get; set; }

    public string? AboutMe { get; set; }

}
