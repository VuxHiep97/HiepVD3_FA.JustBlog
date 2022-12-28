using FA.JustBlog.Core.DataContext;
using FA.JustBlog.Models;
using FA.JustBlog.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Data.DbInitializer
{
    public class DBInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JustBlogContext context;

        public DBInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, JustBlogContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public void Initialize()
        {
            try
            {
                if (context.Database.GetPendingMigrations().Count() > 0)
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
            }

            if (!roleManager.RoleExistsAsync(Roles.BLOG_OWNER).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(Roles.BLOG_OWNER)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(Roles.USER)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(Roles.CONTRIBUTOR)).GetAwaiter().GetResult();

                userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin",
                    AboutMe = "About me",
                    Address = "Hanoi",
                    Age = 25,
                    Email = "admin@gmail.com",
                    Name = "admin",
                    PhoneNumber = "0362473058",
                    NormalizedUserName = "admin",
                    NormalizedEmail = "admin@gmail.com"
                }, "Admin123456@").GetAwaiter().GetResult();

                userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "user",
                    AboutMe = "About me",
                    Address = "Hanoi",
                    Age = 25,
                    Email = "user@gmail.com",
                    Name = "user",
                    PhoneNumber = "0362473058",
                }, "User123456@").GetAwaiter().GetResult();

                userManager.CreateAsync(new ApplicationUser { UserName = "contributor", AboutMe = "About me", Address = "Hanoi", Age = 25, Email = "contributor@gmail.com", Name = "contributor", PhoneNumber = "0362473058" }, "Contributor123456@").GetAwaiter().GetResult();

                ApplicationUser? admin = context.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");
                ApplicationUser? contributor = context.ApplicationUsers.FirstOrDefault(u => u.Email == "contributor@gmail.com");
                ApplicationUser? user = context.ApplicationUsers.FirstOrDefault(u => u.Email == "user@gmail.com");

                userManager.AddToRoleAsync(admin, Roles.BLOG_OWNER).GetAwaiter().GetResult();
                userManager.AddToRoleAsync(contributor, Roles.CONTRIBUTOR).GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, Roles.USER).GetAwaiter().GetResult();

                return;
            }
        }
    }
}
