using FA.JustBlog.Models;
using FA.JustBlog.Models.Enum;
using FA.JustBlog.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Data
{
    public static class ApplicationInitializer
    {
        public static void SeedBlog(this ModelBuilder modelBuilder)
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

        public static void SeedUsers(this ModelBuilder builder)
        {
            ApplicationUser admin = new()
            {
                Id = "605ae5b4-c85d-488e-bc90-a3e2aed93759",
                Name = "admin",
                UserName = "Admin",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
                NormalizedUserName = "Admin",
                NormalizedEmail = "admin@gmail.com"
            };

            ApplicationUser contributor = new()
            {
                Id = "4ff25c26-e45a-4464-8c43-182fcda3b4a3",
                Name = "contributor",
                UserName = "Contributor",
                Email = "contributor@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
                NormalizedUserName = "Contributor",
                NormalizedEmail = "contributor@gmail.com"
            };

            ApplicationUser user = new()
            {
                Id = "85997ef9-9005-4c8d-b923-ae92318047c0",
                Name = "user",
                UserName = "User",
                Email = "user@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
                NormalizedUserName = "User",
                NormalizedEmail = "user@gmail.com"
            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            passwordHasher.HashPassword(admin, "admin");
            builder.Entity<ApplicationUser>().HasData(admin);

            passwordHasher.HashPassword(contributor, "contributor");
            builder.Entity<ApplicationUser>().HasData(contributor);

            passwordHasher.HashPassword(user, "user");
            builder.Entity<ApplicationUser>().HasData(user);

            builder.Entity<IdentityRole>().HasData(
                    new IdentityRole()
                    {
                        Id = "9c0902bb-34ce-46ef-aaca-4c15c26ed1de",
                        Name = Roles.BLOG_OWNER,
                        ConcurrencyStamp = "1",
                        NormalizedName = "Blog Owner"
                    },
                    new IdentityRole()
                    {
                        Id = "41eab30e-92dc-47b6-a3a6-224b28f9ca92",
                        Name = Roles.CONTRIBUTOR,
                        ConcurrencyStamp = "2",
                        NormalizedName = "Contributor"
                    },
                    new IdentityRole()
                    {
                        Id = "c0fddeaa-3cd2-42f9-8b62-f06a3f047c16",
                        Name = Roles.USER,
                        ConcurrencyStamp = "3",
                        NormalizedName = "User"
                    }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    UserId = "605ae5b4-c85d-488e-bc90-a3e2aed93759",
                    RoleId = "9c0902bb-34ce-46ef-aaca-4c15c26ed1de"
                },
                new IdentityUserRole<string>()
                {
                    UserId = "4ff25c26-e45a-4464-8c43-182fcda3b4a3",
                    RoleId = "41eab30e-92dc-47b6-a3a6-224b28f9ca92"
                },
                new IdentityUserRole<string>()
                {
                    UserId = "85997ef9-9005-4c8d-b923-ae92318047c0",
                    RoleId = "c0fddeaa-3cd2-42f9-8b62-f06a3f047c16"
                }
            );
        }
    }
}
