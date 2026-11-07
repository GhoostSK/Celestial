using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.Agency.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2026_05_15_0129 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Tours_ToursId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_TourPlans_Tours_ToursId",
                table: "TourPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_TourPrimaryTransport_Tours_TourId",
                table: "TourPrimaryTransport");

            migrationBuilder.DropForeignKey(
                name: "FK_TourTransPorts_Tours_ToursId",
                table: "TourTransPorts");

            migrationBuilder.DropTable(
                name: "TourEquipmentsTours");

            migrationBuilder.DropTable(
                name: "TourEquipmentsTours1");

            migrationBuilder.DropTable(
                name: "Visas");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_TourTransPorts_ToursId",
                table: "TourTransPorts");

            migrationBuilder.DropIndex(
                name: "IX_TourPlans_ToursId",
                table: "TourPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_States",
                table: "States");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalizedProperties",
                table: "LocalizedProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogCategories",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "ToursId",
                table: "TourTransPorts");

            migrationBuilder.DropColumn(
                name: "ToursId",
                table: "TourPlans");

            migrationBuilder.RenameTable(
                name: "States",
                newName: "State");

            migrationBuilder.RenameTable(
                name: "LocalizedProperties",
                newName: "LocalizedProperty");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Language");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "Currency");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "City");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blog");

            migrationBuilder.RenameTable(
                name: "BlogCategories",
                newName: "BlogCategory");

            migrationBuilder.RenameColumn(
                name: "ToursId",
                table: "Hotels",
                newName: "TourId");

            migrationBuilder.RenameIndex(
                name: "IX_Hotels_ToursId",
                table: "Hotels",
                newName: "IX_Hotels_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_Currencies_Name",
                table: "Currency",
                newName: "IX_Currency_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Countries_Shortname",
                table: "Country",
                newName: "IX_Country_Shortname");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_CategoryId",
                table: "Blog",
                newName: "IX_Blog_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_State",
                table: "State",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalizedProperty",
                table: "LocalizedProperty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currency",
                table: "Currency",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogCategory",
                table: "BlogCategory",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Countries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TourStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RegistrationDeadline = table.Column<DateOnly>(type: "date", nullable: false),
                    PassengerCapacity = table.Column<int>(type: "int", nullable: false),
                    TourUserRegistrations = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ForeignPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ForeignCurrency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PictureId = table.Column<long>(type: "bigint", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    TourContinent = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TourType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TourScope = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RequiresVisa = table.Column<bool>(type: "bit", nullable: false),
                    ExecDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDay = table.Column<int>(type: "int", nullable: false),
                    Views = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visa",
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
                    table.PrimaryKey("PK_Visa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourTourEquipments",
                columns: table => new
                {
                    NecessaryEquipmentId = table.Column<long>(type: "bigint", nullable: false),
                    TourId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTourEquipments", x => new { x.NecessaryEquipmentId, x.TourId });
                    table.ForeignKey(
                        name: "FK_TourTourEquipments_TourEquipments_NecessaryEquipmentId",
                        column: x => x.NecessaryEquipmentId,
                        principalTable: "TourEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourTourEquipments_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourTourEquipments1",
                columns: table => new
                {
                    Tour1Id = table.Column<long>(type: "bigint", nullable: false),
                    UnNecessaryEquipmentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTourEquipments1", x => new { x.Tour1Id, x.UnNecessaryEquipmentId });
                    table.ForeignKey(
                        name: "FK_TourTourEquipments1_TourEquipments_UnNecessaryEquipmentId",
                        column: x => x.UnNecessaryEquipmentId,
                        principalTable: "TourEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourTourEquipments1_Tour_Tour1Id",
                        column: x => x.Tour1Id,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourPlans_TourId",
                table: "TourPlans",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_TourCode",
                table: "Tour",
                column: "TourCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tour_TourScope",
                table: "Tour",
                column: "TourScope");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_TourStatus",
                table: "Tour",
                column: "TourStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TourTourEquipments_TourId",
                table: "TourTourEquipments",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTourEquipments1_UnNecessaryEquipmentId",
                table: "TourTourEquipments1",
                column: "UnNecessaryEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Visa_Country",
                table: "Visa",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_Visa_Status",
                table: "Visa",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Tour_TourId",
                table: "Hotels",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourPlans_Tour_TourId",
                table: "TourPlans",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourPrimaryTransport_Tour_TourId",
                table: "TourPrimaryTransport",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourTransPorts_Tour_TourId",
                table: "TourTransPorts",
                column: "TourId",
                principalTable: "Tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Tour_TourId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_TourPlans_Tour_TourId",
                table: "TourPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_TourPrimaryTransport_Tour_TourId",
                table: "TourPrimaryTransport");

            migrationBuilder.DropForeignKey(
                name: "FK_TourTransPorts_Tour_TourId",
                table: "TourTransPorts");

            migrationBuilder.DropTable(
                name: "TourTourEquipments");

            migrationBuilder.DropTable(
                name: "TourTourEquipments1");

            migrationBuilder.DropTable(
                name: "Visa");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropIndex(
                name: "IX_TourPlans_TourId",
                table: "TourPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_State",
                table: "State");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalizedProperty",
                table: "LocalizedProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currency",
                table: "Currency");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogCategory",
                table: "BlogCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.RenameTable(
                name: "State",
                newName: "States");

            migrationBuilder.RenameTable(
                name: "LocalizedProperty",
                newName: "LocalizedProperties");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "Languages");

            migrationBuilder.RenameTable(
                name: "Currency",
                newName: "Currencies");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "Cities");

            migrationBuilder.RenameTable(
                name: "BlogCategory",
                newName: "BlogCategories");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "Blogs");

            migrationBuilder.RenameColumn(
                name: "TourId",
                table: "Hotels",
                newName: "ToursId");

            migrationBuilder.RenameIndex(
                name: "IX_Hotels_TourId",
                table: "Hotels",
                newName: "IX_Hotels_ToursId");

            migrationBuilder.RenameIndex(
                name: "IX_Currency_Name",
                table: "Currencies",
                newName: "IX_Currencies_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Country_Shortname",
                table: "Countries",
                newName: "IX_Countries_Shortname");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_CategoryId",
                table: "Blogs",
                newName: "IX_Blogs_CategoryId");

            migrationBuilder.AddColumn<long>(
                name: "ToursId",
                table: "TourTransPorts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ToursId",
                table: "TourPlans",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_States",
                table: "States",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalizedProperties",
                table: "LocalizedProperties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogCategories",
                table: "BlogCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Countries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForeignCurrency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ForeignPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PassengerCapacity = table.Column<int>(type: "int", nullable: false),
                    PictureId = table.Column<long>(type: "bigint", nullable: false),
                    RegistrationDeadline = table.Column<DateOnly>(type: "date", nullable: false),
                    RequiresVisa = table.Column<bool>(type: "bit", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalDay = table.Column<int>(type: "int", nullable: false),
                    TourCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TourContinent = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TourScope = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TourStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TourType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TourUserRegistrations = table.Column<int>(type: "int", nullable: false),
                    Views = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IssueDate = table.Column<int>(type: "int", nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    VisaIssuer = table.Column<int>(type: "int", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visas", x => x.Id);
                });

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
                name: "IX_TourTransPorts_ToursId",
                table: "TourTransPorts",
                column: "ToursId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPlans_ToursId",
                table: "TourPlans",
                column: "ToursId");

            migrationBuilder.CreateIndex(
                name: "IX_TourEquipmentsTours_ToursId",
                table: "TourEquipmentsTours",
                column: "ToursId");

            migrationBuilder.CreateIndex(
                name: "IX_TourEquipmentsTours1_UnNecessaryEquipmentId",
                table: "TourEquipmentsTours1",
                column: "UnNecessaryEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourCode",
                table: "Tours",
                column: "TourCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourScope",
                table: "Tours",
                column: "TourScope");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourStatus",
                table: "Tours",
                column: "TourStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Visas_Country",
                table: "Visas",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_Visas_Status",
                table: "Visas",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Tours_ToursId",
                table: "Hotels",
                column: "ToursId",
                principalTable: "Tours",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourPlans_Tours_ToursId",
                table: "TourPlans",
                column: "ToursId",
                principalTable: "Tours",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourPrimaryTransport_Tours_TourId",
                table: "TourPrimaryTransport",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourTransPorts_Tours_ToursId",
                table: "TourTransPorts",
                column: "ToursId",
                principalTable: "Tours",
                principalColumn: "Id");
        }
    }
}
