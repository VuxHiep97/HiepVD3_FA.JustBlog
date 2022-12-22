namespace FA.JustBlog.Core.Models;

public partial class Post
{
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
}
