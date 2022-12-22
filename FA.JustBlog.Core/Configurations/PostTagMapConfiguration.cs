using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.JustBlog.Core.Configurations;

public class PostTagMapConfiguration : IEntityTypeConfiguration<PostTagMap>
{
    public void Configure(EntityTypeBuilder<PostTagMap> builder)
    {
        builder.HasKey(pt => new { pt.TagId, pt.PostId });
    }
}
