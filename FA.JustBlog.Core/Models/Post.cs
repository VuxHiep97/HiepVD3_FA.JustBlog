using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlog.Core.Models;

public partial class Post : BaseEntity
{
    public string Title { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string PostContent { get; set; } = null!;
    public string UrlSlug { get; set; } = null!;
    public bool Published { get; set; }
    public DateTime PostedOn { get; set; }
    public DateTime Modified { get; set; }
    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<PostTagMap> PostTags { get; set; } = null!;
}
