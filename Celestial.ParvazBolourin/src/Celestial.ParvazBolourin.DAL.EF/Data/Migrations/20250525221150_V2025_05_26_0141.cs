using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_05_26_0141 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayCount",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "HotelCode",
                table: "Tours",
                newName: "TourCode");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "Tours",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateOnly>(
                name: "RegistrationDeadline",
                table: "Tours",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourCode",
                table: "Tours",
                column: "TourCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tours_TourCode",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "RegistrationDeadline",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "TourCode",
                table: "Tours",
                newName: "HotelCode");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Tours",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "DayCount",
                table: "Tours",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);
        }
    }
}
