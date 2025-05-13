using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_05_13_2057 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog_BlogCategory");

            migrationBuilder.AddColumn<long>(
                name: "BlogId",
                table: "BlogCategory",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategory_BlogId",
                table: "BlogCategory",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategory_Blog_BlogId",
                table: "BlogCategory",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategory_Blog_BlogId",
                table: "BlogCategory");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategory_BlogId",
                table: "BlogCategory");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "BlogCategory");

            migrationBuilder.CreateTable(
                name: "Blog_BlogCategory",
                columns: table => new
                {
                    BlogsId = table.Column<long>(type: "bigint", nullable: false),
                    CategoriesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_BlogCategory", x => new { x.BlogsId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_Blog_BlogCategory_BlogCategory_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "BlogCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blog_BlogCategory_Blog_BlogsId",
                        column: x => x.BlogsId,
                        principalTable: "Blog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_BlogCategory_CategoriesId",
                table: "Blog_BlogCategory",
                column: "CategoriesId");
        }
    }
}
