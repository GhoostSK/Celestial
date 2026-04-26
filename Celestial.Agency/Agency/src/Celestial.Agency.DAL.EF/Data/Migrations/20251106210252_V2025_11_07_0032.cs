using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.Agency.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_11_07_0032 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CitiesId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "CountriesId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Hotels",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "UnNecessaryEquipment",
                table: "Tours",
                newName: "Countries");

            migrationBuilder.RenameColumn(
                name: "NecessaryEquipment",
                table: "Tours",
                newName: "Cities");

            migrationBuilder.AddColumn<long>(
                name: "ToursId",
                table: "Hotels",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TourEquipmentsTours",
                columns: table => new
                {
                    NecessaryEquipmentId = table.Column<long>(type: "bigint", nullable: false),
                    ToursId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourEquipmentsTours", x => new { x.NecessaryEquipmentId, x.ToursId });
                    table.ForeignKey(
                        name: "FK_TourEquipmentsTours_TourEquipments_NecessaryEquipmentId",
                        column: x => x.NecessaryEquipmentId,
                        principalTable: "TourEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourEquipmentsTours_Tours_ToursId",
                        column: x => x.ToursId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourEquipmentsTours1",
                columns: table => new
                {
                    Tours1Id = table.Column<long>(type: "bigint", nullable: false),
                    UnNecessaryEquipmentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourEquipmentsTours1", x => new { x.Tours1Id, x.UnNecessaryEquipmentId });
                    table.ForeignKey(
                        name: "FK_TourEquipmentsTours1_TourEquipments_UnNecessaryEquipmentId",
                        column: x => x.UnNecessaryEquipmentId,
                        principalTable: "TourEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourEquipmentsTours1_Tours_Tours1Id",
                        column: x => x.Tours1Id,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_ToursId",
                table: "Hotels",
                column: "ToursId");

            migrationBuilder.CreateIndex(
                name: "IX_TourEquipmentsTours_ToursId",
                table: "TourEquipmentsTours",
                column: "ToursId");

            migrationBuilder.CreateIndex(
                name: "IX_TourEquipmentsTours1_UnNecessaryEquipmentId",
                table: "TourEquipmentsTours1",
                column: "UnNecessaryEquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Tours_ToursId",
                table: "Hotels",
                column: "ToursId",
                principalTable: "Tours",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Tours_ToursId",
                table: "Hotels");

            migrationBuilder.DropTable(
                name: "TourEquipmentsTours");

            migrationBuilder.DropTable(
                name: "TourEquipmentsTours1");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_ToursId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ToursId",
                table: "Hotels");

            migrationBuilder.RenameColumn(
                name: "Countries",
                table: "Tours",
                newName: "UnNecessaryEquipment");

            migrationBuilder.RenameColumn(
                name: "Cities",
                table: "Tours",
                newName: "NecessaryEquipment");

            migrationBuilder.AddColumn<string>(
                name: "CitiesId",
                table: "Tours",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountriesId",
                table: "Tours",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hotels",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
