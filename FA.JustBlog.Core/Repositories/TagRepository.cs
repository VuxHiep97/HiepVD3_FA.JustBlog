using FA.JustBlog.Core.DataContext;
using FA.JustBlog.Core.Enum;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.Repositories;

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    public TagRepository(JustBlogContext injected) : base(injected) { }

    public IList<Tag> GetAllTags() => context.Tags.Where(t => t.Status == Status.Actived).ToList();

    public Tag? GetTagByUrlSlug(string urlSlug) => context.Tags.FirstOrDefault(t => t.UrlSlug == urlSlug && t.Status == Status.Actived);
}
