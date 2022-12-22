using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FA.JustBlog.Core.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasMany(t => t.PostTags)
            .WithOne(pt => pt.Tag)
            .HasForeignKey(pt => pt.TagId);

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired();
        builder.Property(t => t.UrlSlug).HasMaxLength(255);
        builder.Property(t => t.Description).HasMaxLength(1024).IsRequired();
    }
}
