using FA.JustBlog.Core.DataContext;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Repositories;
using FA.JustBlog.Models;

namespace FA.JustBlog.Core.Infrastructures;

public class UnitOfWork : IUnitOfWork
{
    private readonly JustBlogContext context;
    private ICategoryRepository categoryRepository = null!;
    private IPostRepository postRepository = null!;
    private ITagRepository tagRepository = null!;
    private ICommentRepository commentRepository = null!;

    public UnitOfWork(JustBlogContext context)
    {
        this.context = context;
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            categoryRepository ??= new CategoryRepository(context);

            return categoryRepository;
        }
    }

    public IPostRepository PostRepository
    {
        get
        {
            postRepository ??= new PostRepository(context);

            return postRepository;
        }
    }

    public ITagRepository TagRepository
    {
        get
        {
            tagRepository ??= new TagRepository(context);

            return tagRepository;
        }
    }

    public ICommentRepository CommentRepository
    {
        get
        {
            commentRepository ??= new CommentRepository(context);

            return commentRepository;
        }
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public int SaveChanges()
    {
        return context.SaveChanges();
    }
}