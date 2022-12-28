using FA.JustBlog.Core.DataContext;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Models;

namespace FA.JustBlog.Core.Repositories;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(JustBlogContext injected) : base(injected) { }

    public IList<Comment> GetAllComments() => context.Comments.ToList();

    public IList<Comment> GetCommentsForPost(int postId)=>context.Comments.Where(c=>c.PostId == postId).ToList();

    public IList<Comment> GetCommentsForPost(Post post)
    {
        if (post is null)
            return new List<Comment>();

        return context.Comments.Where(c => c.PostId == post.Id).ToList();
    }
}
