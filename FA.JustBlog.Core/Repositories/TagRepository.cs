using FA.JustBlog.Core.DataContext;
using FA.JustBlog.Models.Enum;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Models;
using FA.JustBlog.Utility;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Repositories;

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    public TagRepository(JustBlogContext injected) : base(injected) { }

    public IEnumerable<int> AddTagByString(string tags)
    {
        var tagNames = tags.Split(';');

        foreach (var tagName in tagNames)
        {
            var tagExisting = dbSet.Where(t => t.Name.Trim().ToLower() == tagName.Trim().ToLower()).Count();
            if (tagExisting == 0)
            {
                var tag = new Tag()
                {
                    Name = tagName,
                    UrlSlug = SeoUrl.FriendlyUrl(tagName)
                };
                dbSet.Add(tag);
            }
        }

        context.SaveChanges();

        foreach (var tagName in tagNames)
        {
            var tagExisting = dbSet.FirstOrDefault(t => t.Name.Trim().ToLower() == tagName.Trim().ToLower());
            if (tagExisting != null)
            {
                yield return tagExisting.Id;
            }
        }
    }

    public IList<Tag> GetAllTags() => context.Tags.Where(t => t.Status == Status.Actived).ToList();

    public Tag? GetTagByUrlSlug(string urlSlug) => context.Tags.FirstOrDefault(t => t.UrlSlug == urlSlug && t.Status == Status.Actived);

    public IList<Tag> GetTagsByPostId(int postId) => context.PostTagMaps
        .Where(x => x.PostId == postId)
        .Include(x => x.Tag)
        .Select(x => x.Tag)
        .ToList();
}
