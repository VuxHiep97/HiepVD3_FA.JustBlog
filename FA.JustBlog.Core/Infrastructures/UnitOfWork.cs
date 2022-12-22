using FA.JustBlog.Core.DataContext;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.Infrastructures;

public class UnitOfWork : IUnitOfWork
{
    private readonly JustBlogContext context = new();

    private GenericRepository<Category> categoryRepository = null!;
    private GenericRepository<Post> postRepository = null!;
    private GenericRepository<Tag> tagRepository = null!;

    private bool disposed = false;

    public UnitOfWork(JustBlogContext context)
    {
        this.context = context;
    }

    public GenericRepository<Category> CategoryRepository
    {
        get
        {
            categoryRepository ??= new GenericRepository<Category>(context);

            return categoryRepository;
        }
    }
    public GenericRepository<Post> PostRepository
    {
        get
        {
            postRepository ??= new GenericRepository<Post>(context);

            return postRepository;
        }
    }
    public GenericRepository<Tag> TagRepository
    {
        get
        {
            tagRepository ??= new GenericRepository<Tag>(context);

            return tagRepository;
        }
    }

    public int SaveChanges() => context.SaveChanges();

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
            if (disposing)
                context.Dispose();

        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
