using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_05_28_1111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_TourPrimaryTransport_Id",
                table: "Tours");

            migrationBuilder.AddColumn<string>(
                name: "NecessaryEquipment",
                table: "Tours",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnNecessaryEquipment",
                table: "Tours",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocalKeyGroup",
                table: "LocalizedProperty",
                type: "int",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.CreateTable(
                name: "TourEquipments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourEquipments", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_TourPrimaryTransport_Id",
                table: "Tours",
                column: "Id",
                principalTable: "TourPrimaryTransport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_TourPrimaryTransport_Id",
                table: "Tours");

            migrationBuilder.DropTable(
                name: "TourEquipments");

            migrationBuilder.DropColumn(
                name: "NecessaryEquipment",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "UnNecessaryEquipment",
                table: "Tours");

            migrationBuilder.AlterColumn<string>(
                name: "LocalKeyGroup",
                table: "LocalizedProperty",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 40);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_TourPrimaryTransport_Id",
                table: "Tours",
                column: "Id",
                principalTable: "TourPrimaryTransport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
