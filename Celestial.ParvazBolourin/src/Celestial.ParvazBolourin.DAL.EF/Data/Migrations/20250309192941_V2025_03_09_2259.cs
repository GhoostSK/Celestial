using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_03_09_2259 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KeyGroup",
                table: "Images",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "Images",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                comment: "Path to the stored image.");

            migrationBuilder.CreateIndex(
                name: "IX_Images_KeyGroup_EntityId",
                table: "Images",
                columns: new[] { "KeyGroup", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_Images_PicturePath",
                table: "Images",
                column: "PicturePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_KeyGroup_EntityId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PicturePath",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "KeyGroup",
                table: "Images",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
