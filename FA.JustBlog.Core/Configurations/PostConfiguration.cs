using FA.JustBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Core.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CategoryId);

        builder.HasMany(p => p.PostTags)
            .WithOne(pt => pt.Post)
            .HasForeignKey(pt => pt.PostId);

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.ShortDescription).HasColumnName("Short Description").HasMaxLength(255);
        builder.Property(p => p.PostContent).HasColumnName("Post Content");
        builder.Property(p => p.UrlSlug).HasMaxLength(255);
        builder.Property(p => p.Published).IsRequired();
        builder.Property(p => p.PostedOn).HasColumnName("Posted On");
    }
}
