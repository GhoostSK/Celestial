using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.Agency.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2026_11_02_TourBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourBooking",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TourId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PassengerCount = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", maxLength: 500, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(22,3)", precision: 22, scale: 3, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBooking", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourBooking_BookingCode",
                table: "TourBooking",
                column: "BookingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourBooking_TourId",
                table: "TourBooking",
                column: "TourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourBooking");
        }
    }
}
