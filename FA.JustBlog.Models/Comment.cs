namespace FA.JustBlog.Models;

public class Comment : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string CommentHeader { get; set; } = null!;
    public string CommentText { get; set; } = null!;
    public DateTime CommentTime { get; set; }

    public int PostId { get; set; }
    public virtual Post Post { get; set; } = null!;
}
