using FA.JustBlog.Core.Data.DbInitializer;
using FA.JustBlog.Core.DataContext;
using FA.JustBlog.Core.Infrastructures;
using FA.JustBlog.Models;
using FA.JustBlog.Web.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<JustBlogContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DBInitializer>();

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<JustBlogContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "detailsPost",
    pattern: "Post/{year}/{month}/{title}",
    new { controller = "Post", action = "DetailsPostWithTime" },
    new { year = @"\d{4}", month = @"\d{2}" });

app.MapControllerRoute(
    name: "category",
    pattern: "Category/{name}",
    new { controller = "Category", action = "index" },
    new { name = @"^(?!\s*$).+" });

app.MapControllerRoute(
    name: "tag",
    pattern: "Tag/{name}",
    defaults: new { controller = "Tag", action = "Index", name = "name" });

app.MapRazorPages();

app.Run();
