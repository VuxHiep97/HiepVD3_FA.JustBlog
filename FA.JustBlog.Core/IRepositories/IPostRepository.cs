using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.IRepositories;

public partial interface IPostRepository : IGenericRepository<Post>
{
    Post? FindPost(int year, int month, string urlSlug);
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
}
