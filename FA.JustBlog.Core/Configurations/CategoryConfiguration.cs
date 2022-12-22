using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(c => c.Posts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(255).IsRequired();
            builder.Property(c => c.UrlSlug).HasMaxLength(255);
            builder.Property(c => c.Description).HasMaxLength(1024);
        }
    }
}
