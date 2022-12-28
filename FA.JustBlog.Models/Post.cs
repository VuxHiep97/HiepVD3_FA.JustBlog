using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlog.Models;

public class Post : BaseEntity
{
    public string Title { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string PostContent { get; set; } = null!;
    public string UrlSlug { get; set; } = null!;
    public bool Published { get; set; }
    public int CategoryId { get; set; }
    public int ViewCount { get; set; }
    public int RateCount { get; set; }
    public int TotalRate { get; set; }

    private decimal rate;
    public decimal Rate
    {
        get => rate;
        set
        {
            rate = TotalRate / RateCount;
        }
    }

    public virtual ICollection<Comment> Comments { get; set; } = null!;

    public Category Category { get; set; } = null!;

    public ICollection<PostTagMap> PostTags { get; set; } = null!;
}
