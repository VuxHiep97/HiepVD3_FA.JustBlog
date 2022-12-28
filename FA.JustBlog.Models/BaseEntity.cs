using FA.JustBlog.Models.Enum;

namespace FA.JustBlog.Models;

public class BaseEntity
{
    public int Id { get; set; }

    public Status Status { get; set; }
    public DateTime PostedOn { get; set; } = DateTime.Now;
    public DateTime Modified { get; set; } = DateTime.Now;
}
