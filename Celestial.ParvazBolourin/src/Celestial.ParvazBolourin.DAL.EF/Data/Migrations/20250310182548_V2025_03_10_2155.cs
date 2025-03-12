using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_03_10_2155 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelPicture");

            migrationBuilder.DropColumn(
                name: "PicturePath",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "PlanPicturePath",
                table: "TourPlans",
                newName: "PlanPictureId");

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "Tours",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PictureId",
                table: "Tours",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Order of the image for display purposes.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "PlanPictureId",
                table: "TourPlans",
                newName: "PlanPicturePath");

            migrationBuilder.AddColumn<string>(
                name: "PicturePath",
                table: "Tours",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "HotelPicture",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<long>(type: "bigint", nullable: false),
                    VirtualPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelPicture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelPicture_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelPicture_DisplayOrder",
                table: "HotelPicture",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_HotelPicture_HotelId",
                table: "HotelPicture",
                column: "HotelId");
        }
    }
}
