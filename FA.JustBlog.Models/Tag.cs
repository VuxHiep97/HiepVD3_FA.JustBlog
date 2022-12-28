namespace FA.JustBlog.Models;

public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;

    public string UrlSlug { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Count { get; set; }

    public ICollection<PostTagMap> PostTags { get; set; } = null!;
}
