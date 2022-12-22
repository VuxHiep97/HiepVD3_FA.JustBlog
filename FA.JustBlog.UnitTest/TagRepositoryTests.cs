using FA.JustBlog.Core.DataContext;
using FA.JustBlog.Core.Enum;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FA.JustBlog.UnitTest;

[TestFixture]
public class TagRepositoryTests
{
    private ITagRepository repository;

    private IQueryable<Tag> tags;

    private Mock<DbSet<Tag>> mockSet;
    private Mock<JustBlogContext> mockContext;

    [SetUp]
    public void Setup()
    {
        tags = new List<Tag>
        {
            new Tag
            {
                Id = 1,
                Name = "This is Tag_01",
                UrlSlug = "tag-01",
                Description = "This is Description_01",
                Count = 3,
                Status = Status.Actived
            },
            new Tag
            {
                Id = 2,
                Name = "This is Tag_02",
                UrlSlug = "tag-02",
                Description = "This is Description_02",
                Count = 6,
                Status = Status.Actived
            },
            new Tag
            {
                Id = 3,
                Name = "This is Tag_03",
                UrlSlug = "tag-03",
                Description = "This is Description_03",
                Count = 7,
                Status = Status.Deleted
            }
        }.AsQueryable();

        mockSet = new();
        mockSet.As<IQueryable<Tag>>().Setup(m => m.Provider).Returns(tags.Provider);
        mockSet.As<IQueryable<Tag>>().Setup(m => m.Expression).Returns(tags.Expression);
        mockSet.As<IQueryable<Tag>>().Setup(m => m.ElementType).Returns(tags.ElementType);
        mockSet.As<IQueryable<Tag>>().Setup(m => m.GetEnumerator()).Returns(() => tags.GetEnumerator());

        mockContext = new();

        mockContext.Setup(c => c.Set<Tag>()).Returns(mockSet.Object);
        mockContext.Setup(c => c.Tags).Returns(mockSet.Object); ;

        repository = new TagRepository(mockContext.Object);
    }

    [Test]
    public void GetAllTags_WhenCalledWithStatusActived_ReturnAllTags()
    {
        // Act
        var result = repository.GetAllTags().Count;
        var excepted = tags.Where(t => t.Status == Status.Actived).Count();

        // Assert
        Assert.That(result, Is.EqualTo(excepted));
    }

    [Test]
    public void GetTagByUrlSlug_WhenCalledWithValidUrlSlug_ReturnValidTag()
    {
        // Arrange
        string urlSlug = "tag-01";

        // Act
        var result = repository.GetTagByUrlSlug(urlSlug);

        // Assert
        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    [TestCase("not-tag-01", null)]
    [TestCase("", null)]
    public void GetTagByUrlSlug_WhenCalledWithInValidUrlSlug_ReturnEmptyTag(string urlSlug, Tag? result)
    {

        // Act
        result = repository.GetTagByUrlSlug(urlSlug);

        // Assert
        Assert.That(result, Is.Null);
    }
}
