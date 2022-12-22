using FA.JustBlog.Core.Enum;
using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Data
{
    public static class ApplicationInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            List<Category> categories = new()
            {
                new Category
                {
                    Id = 1,
                    Name = "Category 01",
                    UrlSlug = "category-01",
                    Description = "This is description about Category_01",
                    Status = Status.Actived
                },
                new Category
                {
                    Id = 2,
                    Name = "Category 02",
                    UrlSlug = "category-02",
                    Description = "This is description about Category_01",
                    Status = Status.Actived
                },
                new Category
                {
                    Id = 3,
                    Name = "Category 03",
                    UrlSlug = "category-03",
                    Description = "This is description about Category_01",
                    Status = Status.Actived
                },
            };
            modelBuilder.Entity<Category>().HasData(categories);

            List<Post> posts = new()
            {
                new Post
                {
                    Id = 1,
                    Title = "This is title of Post_01",
                    ShortDescription = "This is short description of Post_01",
                    PostContent = "This is post content of Post_01",
                    UrlSlug = "post-01",
                    Published = true,
                    PostedOn = DateTime.Today.AddDays(-2),
                    Modified = DateTime.Today,
                    CategoryId = 1,
                    Status = Status.Actived
                },
                new Post
                {
                    Id = 2,
                    Title = "This is title of Post_02",
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
                    Title = "This is title of Post_03",
                    ShortDescription = "This is short description of Post_03",
                    PostContent = "This is post content of Post_03",
                    UrlSlug = "post-03",
                    Published = true,
                    PostedOn = DateTime.Today.AddDays(-2),
                    Modified = DateTime.Today,
                    CategoryId = 1,
                    Status = Status.Actived
                },
            };
            modelBuilder.Entity<Post>().HasData(posts);

            List<Tag> tags = new()
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
            };
            modelBuilder.Entity<Tag>().HasData(tags);

            List<PostTagMap> postTagMaps = new()
            {
                new PostTagMap
                {
                    PostId = 1,
                    TagId = 2
                },
                new PostTagMap
                {
                    PostId = 1,
                    TagId = 3
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
                }
            };
            modelBuilder.Entity<PostTagMap>().HasData(postTagMaps);
        }
    }
}
