using FA.JustBlog.Core.Enum;

namespace FA.JustBlog.Core.Models;

public class BaseEntity
{
    public int Id { get; set; }

    public Status Status { get; set; }
}
