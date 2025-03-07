using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_02_26_2227 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelCountry",
                table: "Hotels");

            migrationBuilder.RenameColumn(
                name: "Countries",
                table: "Tours",
                newName: "CountriesId");

            migrationBuilder.RenameColumn(
                name: "Cities",
                table: "Tours",
                newName: "CitiesId");

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "Hotels",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HotelState",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "HotelState",
                table: "Hotels");

            migrationBuilder.RenameColumn(
                name: "CountriesId",
                table: "Tours",
                newName: "Countries");

            migrationBuilder.RenameColumn(
                name: "CitiesId",
                table: "Tours",
                newName: "Cities");

            migrationBuilder.AddColumn<string>(
                name: "HotelCountry",
                table: "Hotels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
