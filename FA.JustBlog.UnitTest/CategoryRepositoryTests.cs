using FA.JustBlog.Core.DataContext;
using FA.JustBlog.Core.Enum;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FA.JustBlog.UnitTest;

[TestFixture]
public class CategoryRepositoryTests
{
    private ICategoryRepository repository;

    private IQueryable<Category> categories;

    private Mock<DbSet<Category>> mockSet;
    private Mock<JustBlogContext> mockContext;

    [SetUp]
    public void Setup()
    {
        categories = new List<Category>()
        {
            new()
            {
                Id = 1,
                Name = "Mock Category 01",
                UrlSlug = "category-01",
                Description = "This is description about Category_01",
                Status = Status.Actived
            }
        }.AsQueryable();

        mockSet = new();
        mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(categories.Provider);
        mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.Expression);
        mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.ElementType);
        mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(() => categories.GetEnumerator());

        mockContext = new();

        mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);
        mockContext.Setup(c => c.Categories).Returns(mockSet.Object); ;

        repository = new CategoryRepository(mockContext.Object);
    }

    [Test]
    public void GetAllCategories_WhenCalled_ReturnAllCategories()
    {
        // Act
        var result = repository.GetAllCategories().Count;
        var excepted=categories.Where(c=>c.Status==Status.Actived).Count();

        // Assert
        Assert.That(result, Is.EqualTo(excepted));
    }
}
