﻿// <auto-generated />
using System;
using FA.JustBlog.Core.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FA.JustBlog.Core.Migrations
{
    [DbContext(typeof(JustBlogContext))]
    [Migration("20221214101123_CreateCommentTable")]
    partial class CreateCommentTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FA.JustBlog.Core.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UrlSlug")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "This is description about Category_01",
                            Name = "Category 01",
                            Status = 0,
                            UrlSlug = "category-01"
                        },
                        new
                        {
                            Id = 2,
                            Description = "This is description about Category_01",
                            Name = "Category 02",
                            Status = 0,
                            UrlSlug = "category-02"
                        },
                        new
                        {
                            Id = 3,
                            Description = "This is description about Category_01",
                            Name = "Category 03",
                            Status = 0,
                            UrlSlug = "category-03"
                        });
                });

            modelBuilder.Entity("FA.JustBlog.Core.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CommentHeader")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CommentTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("FA.JustBlog.Core.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Post Content");

                    b.Property<DateTime>("PostedOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("Posted On");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RateCount")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Short Description");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalRate")
                        .HasColumnType("int");

                    b.Property<string>("UrlSlug")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Modified = new DateTime(2022, 12, 14, 0, 0, 0, 0, DateTimeKind.Local),
                            PostContent = "This is post content of Post_01",
                            PostedOn = new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Local),
                            Published = true,
                            Rate = 0m,
                            RateCount = 0,
                            ShortDescription = "This is short description of Post_01",
                            Status = 0,
                            Title = "This is title of Post_01",
                            TotalRate = 0,
                            UrlSlug = "post-01",
                            ViewCount = 0
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Modified = new DateTime(2022, 12, 14, 0, 0, 0, 0, DateTimeKind.Local),
                            PostContent = "This is post content of Post_02",
                            PostedOn = new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Local),
                            Published = true,
                            Rate = 0m,
                            RateCount = 0,
                            ShortDescription = "This is short description of Post_02",
                            Status = 0,
                            Title = "This is title of Post_02",
                            TotalRate = 0,
                            UrlSlug = "post-02",
                            ViewCount = 0
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Modified = new DateTime(2022, 12, 14, 0, 0, 0, 0, DateTimeKind.Local),
                            PostContent = "This is post content of Post_03",
                            PostedOn = new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Local),
                            Published = true,
                            Rate = 0m,
                            RateCount = 0,
                            ShortDescription = "This is short description of Post_03",
                            Status = 0,
                            Title = "This is title of Post_03",
                            TotalRate = 0,
                            UrlSlug = "post-03",
                            ViewCount = 0
                        });
                });

            modelBuilder.Entity("FA.JustBlog.Core.Models.PostTagMap", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("TagId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("PostTagMaps");

                    b.HasData(
                        new
                        {
                            TagId = 2,
                            PostId = 1
                        },
                        new
                        {
                            TagId = 3,
                            PostId = 1
                        },
                        new
                        {
                            TagId = 2,
                            PostId = 2
                        },
                        new
                        {
                            TagId = 3,
                            PostId = 2
                        });
                });

            modelBuilder.Entity("FA.JustBlog.Core.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UrlSlug")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Count = 3,
                            Description = "This is Description_01",
                            Name = "This is Tag_01",
                            Status = 0,
                            UrlSlug = "tag-01"
                        },
                        new
                        {
                            Id = 2,
                            Count = 6,
                            Description = "This is Description_02",
                            Name = "This is Tag_02",
                            Status = 0,
                            UrlSlug = "tag-02"
                        },
                        new
                        {
                            Id = 3,
                            Count = 7,
                            Description = "This is Description_03",
                            Name = "This is Tag_03",
                            Status = 0,
                            UrlSlug = "tag-03"
                        });
                });

            modelBuilder.Entity("FA.JustBlog.Core.Models.Comment", b =>
                {
                    b.HasOne("FA.JustBlog.Core.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("FA.JustBlog.Core.Models.Post", b =>
                {
                    b.HasOne("FA.JustBlog.Core.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("FA.JustBlog.Core.Models.PostTagMap", b =>
                {
                    b.HasOne("FA.JustBlog.Core.Models.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FA.JustBlog.Core.Models.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("FA.JustBlog.Core.Models.Category", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("FA.JustBlog.Core.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PostTags");
                });

            modelBuilder.Entity("FA.JustBlog.Core.Models.Tag", b =>
                {
                    b.Navigation("PostTags");
                });
#pragma warning restore 612, 618
        }
    }
}