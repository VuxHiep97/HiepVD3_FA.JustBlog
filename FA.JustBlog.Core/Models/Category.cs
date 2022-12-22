using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Core.Models;

public class Category : BaseEntity
{
    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string UrlSlug { get; set; } = null!;

    [StringLength(1024)]
    public string Description { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = null!;
}
