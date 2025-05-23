using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_05_23_1022 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTagsMap");

            migrationBuilder.AddColumn<string>(
                name: "TagsId",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagsId",
                table: "Blog");

            migrationBuilder.CreateTable(
                name: "BlogTagsMap",
                columns: table => new
                {
                    BlogId = table.Column<long>(type: "bigint", nullable: false),
                    BlogTagId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTagsMap", x => new { x.BlogId, x.BlogTagId });
                    table.ForeignKey(
                        name: "FK_BlogTagsMap_BlogTags_BlogTagId",
                        column: x => x.BlogTagId,
                        principalTable: "BlogTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTagsMap_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagsMap_BlogTagId",
                table: "BlogTagsMap",
                column: "BlogTagId");
        }
    }
}
