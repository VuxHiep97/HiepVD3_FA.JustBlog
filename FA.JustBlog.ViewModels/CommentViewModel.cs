using FA.JustBlog.Models;
using FA.JustBlog.Models.Enum;

namespace FA.JustBlog.ViewModels;

public class CommentViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string CommentHeader { get; set; } = null!;
    public string CommentText { get; set; } = null!;
    public DateTime CommentTime { get; set; }
    public Status Status { get; set; }
    public int PostId { get; set; }
    //public virtual Post Post { get; set; } = null!;
}
