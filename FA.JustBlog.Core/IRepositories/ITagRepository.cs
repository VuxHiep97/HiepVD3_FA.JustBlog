using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Models;

namespace FA.JustBlog.Core.IRepositories;

public interface ITagRepository : IGenericRepository<Tag>
{
    IList<Tag> GetAllTags();
    Tag? GetTagByUrlSlug(string urlSlug);
    IList<Tag> GetTagsByPostId(int postId);
    IEnumerable<int> AddTagByString(string tags);
}
