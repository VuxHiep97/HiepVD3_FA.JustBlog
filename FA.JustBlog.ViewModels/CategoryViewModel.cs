using FA.JustBlog.Models;
using FA.JustBlog.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.ViewModels;

public class CategoryViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string UrlSlug { get; set; } = null!;

    [StringLength(1024)]
    public string Description { get; set; } = null!;

    public Status Status { get; set; }

    //public virtual ICollection<Post> Posts { get; set; } = null!;
}
