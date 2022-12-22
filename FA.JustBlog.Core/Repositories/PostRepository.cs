using FA.JustBlog.Core.DataContext;
using FA.JustBlog.Core.Enum;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.Repositories;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(JustBlogContext injected) : base(injected) { }

    public int CountPostsForCategory(string categoryName)
    {
        Category? category = context.Categories.FirstOrDefault(c => c.Name == categoryName);

        if (category is not null)
        {
            IList<Post> posts = context.Posts
                .Where(p => p.CategoryId == category.Id).ToList();

            return posts.Count;
        }

        return -1;
    }

    public int CountPostsForTag(string tagName)
    {
        Tag? tag = context.Tags.FirstOrDefault(t => t.Name == tagName);

        if (tag is not null)
        {
            int countPostsByTag = context.PostTagMaps
                .Where(pt => pt.TagId == tag.Id).Select(pt => pt.PostId).Count();
            return countPostsByTag;
        }

        return -1;
    }

    public Post? FindPost(int year, int month, string urlSlug) => context.Posts.FirstOrDefault(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug == urlSlug && p.Status == Status.Actived);

    public Post? FindPost(int postId) => context.Posts.FirstOrDefault(p => p.Id == postId && p.Status == Status.Actived);

    public IList<Post> GetAllPosts() => context.Posts.Where(p => p.Status == Status.Actived).ToList();

    public IList<Post> GetHighestPosts(int size)=> context.Posts.OrderByDescending(p => p.TotalRate).Take(size).ToList();

    public IList<Post> GetLatestPost(int size) => context.Posts.Where(p => p.Status == Status.Actived).OrderByDescending(p => p.PostedOn).Take(size).ToList();

    public IList<Post> GetMostViewedPost(int size) => context.Posts.OrderByDescending(p => p.ViewCount).Take(size).ToList();

    public IList<Post> GetPostsByCategory(string categoryName)
    {
        Category? category = context.Categories.FirstOrDefault(c => c.Name == categoryName && c.Status == Status.Actived);
        IList<Post> posts = new List<Post>();

        if (category is not null)
        {
            posts = context.Posts.Where(p => p.CategoryId == category.Id && p.Status == Status.Actived).ToList();
            return posts;
        }

        return posts;
    }

    public IList<Post> GetPostsByMonth(DateTime monthYear) => context.Posts
        .Where(p => p.PostedOn.Year == monthYear.Year && p.PostedOn.Month == monthYear.Month && p.Status == Status.Actived).ToList();

    public IList<Post> GetPostsByTag(string tagName)
    {
        Post post = null!;
        IList<Post> postsByTag = new List<Post>();

        Tag? tag = context.Tags.FirstOrDefault(t => t.Name == tagName);

        if (tag is not null)
        {
            IList<int> listOfPostsIdByTag = context.PostTagMaps
                .Where(pt => pt.TagId == tag.Id)
                .Select(pt => pt.PostId).ToList();

            foreach (int postId in listOfPostsIdByTag)
            {
                post = context.Posts.First(p => p.Id == postId);
                postsByTag.Add(post);
            }
        }

        return postsByTag;
    }

    public IList<Post> GetPublishedPosts() => context.Posts.Where(p => p.Published && p.Status == Status.Actived).ToList();

    public IList<Post> GetUnpublishedPosts() => context.Posts.Where(p => !p.Published).ToList();
}
