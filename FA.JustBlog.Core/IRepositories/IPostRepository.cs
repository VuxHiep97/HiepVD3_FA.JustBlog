using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Models;

namespace FA.JustBlog.Core.IRepositories;

public interface IPostRepository : IGenericRepository<Post>
{
    Post? FindPost(int year, int month, string urlSlug);
    Post? FindPost(string title, int year, int month);
    Post? FindPost(int postId);
    IList<Post> GetAllPosts();
    IList<Post> GetPublishedPosts();
    IList<Post> GetUnpublishedPosts();
    IList<Post> GetLatestPost(int size);
    IList<Post> GetPostsByMonth(DateTime monthYear);
    int CountPostsForCategory(string categoryName);
    IList<Post> GetPostsByCategory(string categoryName);
    int CountPostsForTag(string tagName);
    IList<Post> GetPostsByTag(string tagName);
    IList<Post> GetMostViewedPost(int size);
    IList<Post> GetHighestPosts(int size);
}
