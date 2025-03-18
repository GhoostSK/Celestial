using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2025_03_12_2205 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmationCodeRequestedAt",
                table: "IdentityUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FailedConfirmationAttempts",
                table: "IdentityUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockedUntil",
                table: "IdentityUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberConfirmationCode",
                table: "IdentityUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationCodeRequestedAt",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "FailedConfirmationAttempts",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "LockedUntil",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmationCode",
                table: "IdentityUsers");
        }
    }
}
