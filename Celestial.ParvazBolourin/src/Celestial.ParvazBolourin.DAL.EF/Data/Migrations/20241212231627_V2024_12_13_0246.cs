using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.ParvazBolourin.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2024_12_13_0246 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Likes = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "Number of likes for the blog post."),
                    PicturePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Path to the picture associated with the blog."),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HotelLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HotelContinent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    HotelCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HotelScore = table.Column<int>(type: "int", nullable: false),
                    BookingUrl = table.Column<string>(type: "nvarchar(max)", maxLength: 500, nullable: true),
                    HotelCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HotelCoordinate = table.Column<string>(type: "nvarchar(max)", maxLength: 500, nullable: false),
                    HotelReception = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LanguageCulture = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Rtl = table.Column<bool>(type: "bit", nullable: false),
                    Published = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalizedProperty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalKeyGroup = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LocalKey = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LocalValue = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedProperty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourPrimaryTransport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeadFlightCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReturnFlightCode = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeadStartTime = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LeadEndTime = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReturnStartTime = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReturnEndTimeTime = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPrimaryTransport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VisaIssuer = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    IssueDate = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelPicture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    VirtualPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Countries = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Cities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TourStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassengerCapacity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ImportantTips = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Hotels = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    TourContinent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TourType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourScope = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayCount = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    RequiresVisa = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_TourPrimaryTransport_Id",
                        column: x => x.Id,
                        principalTable: "TourPrimaryTransport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TourPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    PlanPicturePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourPlans_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourTransPorts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    TransPortType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTransPorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourTransPorts_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
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

            migrationBuilder.CreateIndex(
                name: "IX_TourPlans_TourId",
                table: "TourPlans",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPrimaryTransport_LeadFlightCode",
                table: "TourPrimaryTransport",
                column: "LeadFlightCode");

            migrationBuilder.CreateIndex(
                name: "IX_TourTransPorts_TourId",
                table: "TourTransPorts",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Visas_Country",
                table: "Visas",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_Visas_Status",
                table: "Visas",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "HotelPicture");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "LocalizedProperty");

            migrationBuilder.DropTable(
                name: "TourPlans");

            migrationBuilder.DropTable(
                name: "TourTransPorts");

            migrationBuilder.DropTable(
                name: "Visas");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "TourPrimaryTransport");
        }
    }
}
