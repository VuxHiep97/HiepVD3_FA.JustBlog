using FA.JustBlog.Core.Configurations;
using FA.JustBlog.Core.Data;
using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.DataContext;

public class JustBlogContext : DbContext
{
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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TagConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostTagMapConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommentConfiguration).Assembly);

        modelBuilder.Seed();
    }
}
