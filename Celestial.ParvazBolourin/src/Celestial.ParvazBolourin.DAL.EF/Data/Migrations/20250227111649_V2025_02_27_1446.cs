using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_02_27_1446 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HotelState",
                table: "Hotels",
                newName: "StateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Hotels",
                newName: "HotelState");
        }
    }
}
