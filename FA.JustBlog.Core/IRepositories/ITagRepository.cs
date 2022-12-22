using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.IRepositories;

public interface ITagRepository : IGenericRepository<Tag>
{
    IList<Tag> GetAllTags();
    Tag? GetTagByUrlSlug(string urlSlug);
}
