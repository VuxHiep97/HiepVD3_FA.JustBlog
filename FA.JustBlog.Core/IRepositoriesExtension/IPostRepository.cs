using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.IRepositories;

public partial interface IPostRepository
{
    IList<Post> GetMostViewedPost(int size);
    IList<Post> GetHighestPosts(int size);
}
