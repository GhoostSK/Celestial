using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_05_13_2211 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CategoriesId",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriesId",
                table: "Blog");

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
    }
}
