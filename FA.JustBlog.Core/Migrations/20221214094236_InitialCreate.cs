using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FA.JustBlog.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UrlSlug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlSlug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(name: "Short Description", type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PostContent = table.Column<string>(name: "Post Content", type: "nvarchar(max)", nullable: false),
                    UrlSlug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Published = table.Column<bool>(type: "bit", nullable: false),
                    PostedOn = table.Column<DateTime>(name: "Posted On", type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTagMaps",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTagMaps", x => new { x.TagId, x.PostId });
                    table.ForeignKey(
                        name: "FK_PostTagMaps_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTagMaps_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "Status", "UrlSlug" },
                values: new object[,]
                {
                    { 1, "This is description about Category_01", "Category 01", 0, "category-01" },
                    { 2, "This is description about Category_01", "Category 02", 0, "category-02" },
                    { 3, "This is description about Category_01", "Category 03", 0, "category-03" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Count", "Description", "Name", "Status", "UrlSlug" },
                values: new object[,]
                {
                    { 1, 3, "This is Description_01", "This is Tag_01", 0, "tag-01" },
                    { 2, 6, "This is Description_02", "This is Tag_02", 0, "tag-02" },
                    { 3, 7, "This is Description_03", "This is Tag_03", 0, "tag-03" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "Modified", "Post Content", "Posted On", "Published", "Short Description", "Status", "Title", "UrlSlug" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 12, 14, 0, 0, 0, 0, DateTimeKind.Local), "This is post content of Post_01", new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Local), true, "This is short description of Post_01", 0, "This is title of Post_01", "post-01" },
                    { 2, 2, new DateTime(2022, 12, 14, 0, 0, 0, 0, DateTimeKind.Local), "This is post content of Post_02", new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Local), true, "This is short description of Post_02", 0, "This is title of Post_02", "post-02" },
                    { 3, 1, new DateTime(2022, 12, 14, 0, 0, 0, 0, DateTimeKind.Local), "This is post content of Post_03", new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Local), true, "This is short description of Post_03", 0, "This is title of Post_03", "post-03" }
                });

            migrationBuilder.InsertData(
                table: "PostTagMaps",
                columns: new[] { "PostId", "TagId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 2 },
                    { 1, 3 },
                    { 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTagMaps_PostId",
                table: "PostTagMaps",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTagMaps");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
