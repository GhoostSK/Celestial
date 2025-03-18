using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_03_17_0154 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Visas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "Visas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Visas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Visas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExecDate",
                table: "Tours",
                type: "NVARCHAR(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Tours",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "Tours",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Tours",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Tours",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Hotels",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "Hotels",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Hotels",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Hotels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Blog",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywords",
                table: "Blog",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Blog",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Blog",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Visas");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "Visas");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Visas");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Visas");

            migrationBuilder.DropColumn(
                name: "ExecDate",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "MetaKeywords",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Blog");
        }
    }
}
