using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Core.Models;

public class Tag : BaseEntity
{
    [Required(ErrorMessage = "Tag name is required")]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string UrlSlug { get; set; } = null!;

    [Required(ErrorMessage = "Description content is required")]
    public string Description { get; set; } = null!;

    public int Count { get; set; }

    public ICollection<PostTagMap> PostTags { get; set; } = null!;
}
