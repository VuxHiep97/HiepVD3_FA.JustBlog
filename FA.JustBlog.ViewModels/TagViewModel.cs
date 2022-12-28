using FA.JustBlog.Models;
using FA.JustBlog.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.ViewModels;

public class TagViewModel
{
    public int Id { get; set; }

    public Status Status { get; set; }
    public DateTime PostedOn { get; set; } = DateTime.Now;
    public DateTime Modified { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Tag name is required")]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string UrlSlug { get; set; } = null!;

    [Required(ErrorMessage = "Description content is required")]
    public string Description { get; set; } = null!;

    public int Count { get; set; }

    public ICollection<PostTagMap> PostTags { get; set; } = null!;
}
