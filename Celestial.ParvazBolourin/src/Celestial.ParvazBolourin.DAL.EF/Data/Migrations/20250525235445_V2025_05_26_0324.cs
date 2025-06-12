using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_05_26_0324 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Tours",
                newName: "BasePrice");

            migrationBuilder.AddColumn<string>(
                name: "ForeignCurrency",
                table: "Tours",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ForeignPrice",
                table: "Tours",
                type: "decimal(18,2)",
                precision: 22,
                scale: 3,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TourUserRegistrations",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForeignCurrency",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "ForeignPrice",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TourUserRegistrations",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "BasePrice",
                table: "Tours",
                newName: "Price");
        }
    }
}
