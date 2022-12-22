using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.Infrastructures;

public interface IUnitOfWork : IDisposable
{
    GenericRepository<Category> CategoryRepository { get; }
    GenericRepository<Post> PostRepository { get; }
    GenericRepository<Tag> TagRepository { get; }
    int SaveChanges();
}