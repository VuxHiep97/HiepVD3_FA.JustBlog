using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Models;

namespace FA.JustBlog.Core.Infrastructures;

public interface IUnitOfWork : IDisposable
{
    public ICategoryRepository CategoryRepository { get; }
    public IPostRepository PostRepository { get; }
    public ITagRepository TagRepository { get; }
    public ICommentRepository CommentRepository { get; }

    int SaveChanges();
}