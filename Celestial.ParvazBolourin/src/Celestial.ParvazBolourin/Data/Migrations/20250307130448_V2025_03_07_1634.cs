using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_03_07_1634 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "IdentityUsers");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "IdentityUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "IdentityUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "IdentityUsers");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "IdentityUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "IdentityUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "IdentityUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "IdentityUsers",
                column: "NormalizedEmail");
        }
    }
}
