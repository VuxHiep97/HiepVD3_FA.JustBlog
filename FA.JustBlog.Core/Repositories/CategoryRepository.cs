using FA.JustBlog.Core.DataContext;
using FA.JustBlog.Models.Enum;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Models;

namespace FA.JustBlog.Core.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(JustBlogContext injected) : base(injected) { }

    public IList<Category> GetAllCategories() => context.Categories.Where(c => c.Status == Status.Actived).ToList();
}
