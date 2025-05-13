using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_05_13_1522 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_BlogCategory_BlogCategory_BlogCategoriesId",
                table: "Blog_BlogCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog_BlogCategory",
                table: "Blog_BlogCategory");

            migrationBuilder.DropIndex(
                name: "IX_Blog_BlogCategory_BlogsId",
                table: "Blog_BlogCategory");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BlogCategory");

            migrationBuilder.RenameColumn(
                name: "BlogCategoriesId",
                table: "Blog_BlogCategory",
                newName: "CategoriesId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BlogCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog_BlogCategory",
                table: "Blog_BlogCategory",
                columns: new[] { "BlogsId", "CategoriesId" });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_BlogCategory_CategoriesId",
                table: "Blog_BlogCategory",
                column: "CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_BlogCategory_BlogCategory_CategoriesId",
                table: "Blog_BlogCategory",
                column: "CategoriesId",
                principalTable: "BlogCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_BlogCategory_BlogCategory_CategoriesId",
                table: "Blog_BlogCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog_BlogCategory",
                table: "Blog_BlogCategory");

            migrationBuilder.DropIndex(
                name: "IX_Blog_BlogCategory_CategoriesId",
                table: "Blog_BlogCategory");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BlogCategory");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "Blog_BlogCategory",
                newName: "BlogCategoriesId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BlogCategory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Category name for blogs.");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog_BlogCategory",
                table: "Blog_BlogCategory",
                columns: new[] { "BlogCategoriesId", "BlogsId" });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_BlogCategory_BlogsId",
                table: "Blog_BlogCategory",
                column: "BlogsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_BlogCategory_BlogCategory_BlogCategoriesId",
                table: "Blog_BlogCategory",
                column: "BlogCategoriesId",
                principalTable: "BlogCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
