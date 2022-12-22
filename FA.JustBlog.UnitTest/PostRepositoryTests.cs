using FA.JustBlog.Core.DataContext;
using FA.JustBlog.Core.Enum;
using FA.JustBlog.Core.IRepositories;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Reflection.Emit;

namespace FA.JustBlog.UnitTest
{
    [TestFixture]
    public class PostRepositoryTests
    {
        private IPostRepository repository;

        private IQueryable<Category> categories;
        private IQueryable<Post> posts;
        private IQueryable<Tag> tags;
        private IQueryable<PostTagMap> postTagMaps;

        private Mock<DbSet<Post>> mockPost;
        private Mock<DbSet<Category>> mockCategory;
        private Mock<DbSet<PostTagMap>> mockPostTag;
        private Mock<DbSet<Tag>> mockTag;
        private Mock<JustBlogContext> mockContext;

        [SetUp]
        public void Setup()
        {
            posts = new List<Post>()
            {
                new Post
                {
                    Id = 1,
                    Title = "This is mock title of Post_01",
                    ShortDescription = "This is short description of Post_01",
                    PostContent = "This is post content of Post_01",
                    UrlSlug = "post-01",
                    Published = false,
                    PostedOn = DateTime.Today.AddDays(-2),
                    Modified = DateTime.Today,
                    CategoryId = 1,
                    Status = Status.Actived
                },
                new Post
                {
                    Id = 2,
                    Title = "This is mock title of Post_02",
                    ShortDescription = "This is short description of Post_02",
                    PostContent = "This is post content of Post_02",
                    UrlSlug = "post-02",
                    Published = true,
                    PostedOn = DateTime.Today.AddDays(-2),
                    Modified = DateTime.Today,
                    CategoryId = 2,
                    Status = Status.Actived
                },
                new Post
                {
                    Id = 3,
                    Title = "This is mock title of Post_03",
                    ShortDescription = "This is short description of Post_03",
                    PostContent = "This is post content of Post_03",
                    UrlSlug = "post-03",
                    Published = true,
                    PostedOn = DateTime.Today.AddDays(-2),
                    Modified = DateTime.Today,
                    CategoryId = 1,
                    Status = Status.Deleted
                },
            }.AsQueryable();

            categories = new List<Category>()
            {
                new()
                {
                    Id = 1,
                    Name = "Mock Category 01",
                    UrlSlug = "category-01",
                    Description = "This is description about Category_01",
                    Status = Status.Actived,
                    Posts = posts.ToList()
                }
            }.AsQueryable();

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
                    Status = Status.Actived
                }
            }.AsQueryable();

            postTagMaps = new List<PostTagMap>()
            {
                new PostTagMap
                {
                    PostId = 1,
                    TagId = 1
                },
                new PostTagMap
                {
                    PostId = 2,
                    TagId = 1
                },
                new PostTagMap
                {
                    PostId = 2,
                    TagId = 2
                },
                new PostTagMap
                {
                    PostId = 2,
                    TagId = 3
                },
                new PostTagMap
                {
                    PostId = 1,
                    TagId = 2
                },
            }.AsQueryable();

            mockPost = new();
            mockPost.As<IQueryable<Post>>().Setup(m => m.Provider).Returns(posts.Provider);
            mockPost.As<IQueryable<Post>>().Setup(m => m.Expression).Returns(posts.Expression);
            mockPost.As<IQueryable<Post>>().Setup(m => m.ElementType).Returns(posts.ElementType);
            mockPost.As<IQueryable<Post>>().Setup(m => m.GetEnumerator()).Returns(() => posts.GetEnumerator());

            mockCategory = new();
            mockCategory.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(categories.Provider);
            mockCategory.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.Expression);
            mockCategory.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.ElementType);
            mockCategory.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(() => categories.GetEnumerator());

            mockTag = new();
            mockTag.As<IQueryable<Tag>>().Setup(m => m.Provider).Returns(tags.Provider);
            mockTag.As<IQueryable<Tag>>().Setup(m => m.Expression).Returns(tags.Expression);
            mockTag.As<IQueryable<Tag>>().Setup(m => m.ElementType).Returns(tags.ElementType);
            mockTag.As<IQueryable<Tag>>().Setup(m => m.GetEnumerator()).Returns(() => tags.GetEnumerator());

            mockPostTag = new();
            mockPostTag.As<IQueryable<PostTagMap>>().Setup(m => m.Provider).Returns(postTagMaps.Provider);
            mockPostTag.As<IQueryable<PostTagMap>>().Setup(m => m.Expression).Returns(postTagMaps.Expression);
            mockPostTag.As<IQueryable<PostTagMap>>().Setup(m => m.ElementType).Returns(postTagMaps.ElementType);
            mockPostTag.As<IQueryable<PostTagMap>>().Setup(m => m.GetEnumerator()).Returns(() => postTagMaps.GetEnumerator());

            mockContext = new();

            mockContext.Setup(c => c.Set<Post>()).Returns(mockPost.Object);
            mockContext.Setup(c => c.Posts).Returns(mockPost.Object);

            mockContext.Setup(c => c.Set<Category>()).Returns(mockCategory.Object);
            mockContext.Setup(c => c.Categories).Returns(mockCategory.Object);

            mockContext.Setup(c => c.Set<Tag>()).Returns(mockTag.Object);
            mockContext.Setup(c => c.Tags).Returns(mockTag.Object);

            mockContext.Setup(c => c.Set<PostTagMap>()).Returns(mockPostTag.Object);
            mockContext.Setup(c => c.PostTagMaps).Returns(mockPostTag.Object);

            repository = new PostRepository(mockContext.Object);
        }

        [Test]
        public void CountPostsForCategory_WhenCategoryNameIsValid_ReturnNumberOfPosts()
        {
            // Arrange
            string categoryName = "Mock Category 01";

            // Act
            var result = repository.CountPostsForCategory(categoryName);

            // Assert
            Assert.That(result, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        [TestCase("Mock", -1)]
        [TestCase("", -1)]
        public void CountPostsForCategory_WhenCategoryNameIsNotExistOrEmpty_ReturnMinusOne(string categoryName, int result)
        {
            // Act
            result = repository.CountPostsForCategory(categoryName);

            // Assert
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void CountPostsForTag_WhenTagNameIsValid_ReturnNumberOfPosts()
        {
            // Arrange
            string tagName = "This is Tag_01";

            // Act
            var result = repository.CountPostsForTag(tagName);

            // Assert
            Assert.That(result, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        [TestCase("Mock", -1)]
        [TestCase("", -1)]
        public void CountPostsForTag_WhenTagNameIsNotExistOrEmpty_ReturnMinusOne(string tagName, int result)
        {
            // Act
            result = repository.CountPostsForTag(tagName);

            // Assert
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void FindPost_WhenCalledByYearAndMonthAndUrlSlug_ReturnPostWithStatusActive()
        {
            // Arrange
            Post post = new()
            {
                Id = 1,
                UrlSlug = "post-01",
                Published = true,
                PostedOn = DateTime.Today.AddDays(-2),
                Modified = DateTime.Today,
                CategoryId = 1,
                Status = Status.Actived
            };

            // Act
            var result = repository.FindPost(post.PostedOn.Year, post.PostedOn.Month, post.UrlSlug);

            // Assert
            Assert.That(result.Id, Is.EqualTo(post.Id));
        }

        [Test]
        public void Find_WhenCalled_ReturnListOfPostsWithStatusActive()
        {
            // Act
            var countOfActivedPosts = repository.Find(p => p.Status == Status.Actived).Count();
            var expectedResult = posts.Count(p => p.Status == Status.Actived);

            // Assert
            Assert.That(expectedResult, Is.EqualTo(countOfActivedPosts));
        }

        [Test]
        [TestCase(0, 2, "post-1", null)]
        [TestCase(0, 0, "post-1", null)]
        [TestCase(2, 0, "post-1", null)]
        [TestCase(2, 0, "abc.com", null)]
        [TestCase(2, 0, "", null)]
        public void FindPost_WhenCallWithInvalidArguments_ReturnNull(int year, int month, string urlSlug, Post expectedResult)
        {
            // Act
            var result = repository.FindPost(year, month, urlSlug);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void FindPost_WhenCallById_ReturnPostWithStatusActive()
        {
            // Act
            var result = repository.FindPost(1);

            // Assert
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test]
        public void FindPost_WhenCallByIdNotInData_ReturnNull()
        {
            // Act
            var result = repository.FindPost(4);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetAllPosts_WhenCall_ReturnAllPostsWithStatusActive()
        {
            // Act
            var activePosts = repository.GetAllPosts().Count(p => p.Status == Status.Actived);
            var result = posts.Count(p => p.Status == Status.Actived);

            // Assert
            Assert.That(activePosts, Is.EqualTo(result));
        }

        [Test]
        public void GetLatestPost_WhenCall_ReturnNumbersOfLatestPostBySize()
        {
            // Arrange
            int size = 2;

            // Act
            var result = repository.GetLatestPost(size).Count;

            // Assert
            Assert.That(result, Is.EqualTo(size));
        }

        [Test]
        [TestCase(0, null)]
        [TestCase(-1, null)]
        public void GetLatestPost_WhenSizeLessThanOrEqualToZero_ReturnEmptyList(int size, IList<Post> result)
        {
            // Act
            result = repository.GetLatestPost(size);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetLatestPost_WhenSizeBiggerThanData_ReturnAllOrderByDescendingPost()
        {
            // Act
            var size = 4;
            var activedPosts = posts.Where(p => p.Status == Status.Actived).Count();
            var result = repository.GetLatestPost(size).Count;

            // Assert
            Assert.That(result, Is.EqualTo(activedPosts));
        }

        [Test]
        public void GetPostsByCategory_WhenCategoryNameExistInDatabase_ReturnPostsFound()
        {
            // Arrange 
            string categoryName = "Mock Category 01";

            // Act
            var result = repository.GetPostsByCategory(categoryName)?.Count;
            var excepted = repository.GetAllPosts().Where(p => p.CategoryId == 1 && p.Status == Status.Actived).Count();

            // Assert
            Assert.That(result, Is.EqualTo(excepted));
        }

        [Test]
        [TestCase("Something", null)]
        [TestCase("", null)]
        public void GetPostsByCategory_WhenCategoryNameNotExistOrEmpty_ReturnNull(string categoryName, IList<Post> result)
        {
            // Act
            result = repository.GetPostsByCategory(categoryName);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetPostsByMonth_WhenDateIsValid_ReturnListOfPostIfFound()
        {
            // Arrange
            Post post = new()
            {
                PostedOn = DateTime.Today.AddDays(-2),
                Modified = DateTime.Today,
            };

            // Act
            int result = repository.GetPostsByMonth(post.PostedOn).Count;
            int excepted = posts.Where(p => p.PostedOn.Year == post.PostedOn.Year && p.PostedOn.Month == post.PostedOn.Month && p.Status == Status.Actived).Count();

            // Assert
            Assert.That(result, Is.EqualTo(excepted));
        }

        [Test]
        public void GetPostsByMonth_WhenDateIsNotEqualToDatabase_ReturnEmpty()
        {
            // Arrange
            Post post = new()
            {
                PostedOn = DateTime.Today.AddYears(-5),
                Modified = DateTime.Today,
            };

            // Act
            int result = repository.GetPostsByMonth(post.PostedOn).Count;

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void GetPostsByTag_WhenValidTagName_ReturnPosts()
        {
            // Arrange
            string tagName = "This is Tag_01";

            // Act
            var result = repository.GetPostsByTag(tagName);

            // Assert
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        [TestCase("abc", null)]
        [TestCase("", null)]
        public void GetPostsByTag_WhenTagNameIsNotExistOrEmpty_ReturnEmpty(string tagName, IList<Post> result)
        {
            // Act
            result = repository.GetPostsByTag(tagName);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetPublishedPosts_WhenCalled_ReturnPublishedPosts()
        {
            // Act
            var result = repository.GetPublishedPosts().Count;
            var excepted = posts.Where(p => p.Published && p.Status == Status.Actived).Count();

            // Assert
            Assert.That(result, Is.EqualTo(excepted));
        }

        [Test]
        public void GetPublishedPosts_WhenCalled_ReturnEmptyPosts()
        {
            // Arrange
            var posts = new List<Post>().AsQueryable();

            mockPost = new();
            mockPost.As<IQueryable<Post>>().Setup(m => m.Provider).Returns(posts.Provider);
            mockPost.As<IQueryable<Post>>().Setup(m => m.Expression).Returns(posts.Expression);
            mockPost.As<IQueryable<Post>>().Setup(m => m.ElementType).Returns(posts.ElementType);
            mockPost.As<IQueryable<Post>>().Setup(m => m.GetEnumerator()).Returns(() => posts.GetEnumerator());

            mockContext.Setup(c => c.Set<Post>()).Returns(mockPost.Object);
            mockContext.Setup(c => c.Posts).Returns(mockPost.Object);

            // Act
            var result = repository.GetPublishedPosts();

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetUnpublishedPosts_WhenCalled_ReturnUnPublishedPosts()
        {
            // Act
            var result = repository.GetUnpublishedPosts().Count;

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void GetUnpublishedPosts_WhenCalled_ReturnEmptyPosts()
        {
            // Arrange
            var posts = new List<Post>().AsQueryable();

            mockPost = new();
            mockPost.As<IQueryable<Post>>().Setup(m => m.Provider).Returns(posts.Provider);
            mockPost.As<IQueryable<Post>>().Setup(m => m.Expression).Returns(posts.Expression);
            mockPost.As<IQueryable<Post>>().Setup(m => m.ElementType).Returns(posts.ElementType);
            mockPost.As<IQueryable<Post>>().Setup(m => m.GetEnumerator()).Returns(() => posts.GetEnumerator());

            mockContext.Setup(c => c.Set<Post>()).Returns(mockPost.Object);
            mockContext.Setup(c => c.Posts).Returns(mockPost.Object);

            // Act
            var result = repository.GetPublishedPosts();

            // Assert
            Assert.That(result, Is.Empty);
        }
    }
}