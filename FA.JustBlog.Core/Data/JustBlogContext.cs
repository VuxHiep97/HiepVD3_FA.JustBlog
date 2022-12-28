using FA.JustBlog.Core.Data;
using FA.JustBlog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FA.JustBlog.Core.DataContext;

public class JustBlogContext : IdentityDbContext
{
    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Post> Posts { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;
    public virtual DbSet<PostTagMap> PostTagMaps { get; set; } = null!;
    public virtual DbSet<Comment> Comments { get; set; } = null!;

    public JustBlogContext() { }

    public JustBlogContext(DbContextOptions<JustBlogContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var types = modelBuilder.Model.GetEntityTypes().Select(t => t.ClrType).ToHashSet();
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            t => t.GetInterfaces()
                .Any(i => i.IsGenericType
                    && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
                    && types.Contains(i.GenericTypeArguments[0]))
            );

        modelBuilder.SeedBlog();
    }
}
