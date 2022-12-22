using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.IRepositories;

public interface ICommentRepository : IGenericRepository<Comment>
{
    IList<Comment> GetAllComments();
    IList<Comment> GetCommentsForPost(int postId);
    IList<Comment> GetCommentsForPost(Post post);
}
