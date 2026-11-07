using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celestial.Agency.DAL.EF.Data.Migrations
{
    /// <inheritdoc />
    public partial class V2026_05_15_0138 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTags");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "TourPlans");

            migrationBuilder.DropTable(
                name: "TourTourEquipments");

            migrationBuilder.DropTable(
                name: "TourTourEquipments1");

            migrationBuilder.DropTable(
                name: "TourTransPorts");

            migrationBuilder.DropTable(
                name: "TourEquipments");

            migrationBuilder.CreateTable(
                name: "BlogTag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HotelLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HotelContinent = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    HotelScore = table.Column<int>(type: "int", nullable: false),
                    BookingUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HotelCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HotelCoordinate = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    HotelReception = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TourId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotel_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyGroup = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourEquipment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourEquipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourPlan",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<long>(type: "bigint", nullable: false),
                    PlanPictureId = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourPlan_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourTransPort",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<long>(type: "bigint", nullable: false),
                    TransPortType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTransPort", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourTransPort_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourTourEquipment",
                columns: table => new
                {
                    NecessaryEquipmentId = table.Column<long>(type: "bigint", nullable: false),
                    TourId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTourEquipment", x => new { x.NecessaryEquipmentId, x.TourId });
                    table.ForeignKey(
                        name: "FK_TourTourEquipment_TourEquipment_NecessaryEquipmentId",
                        column: x => x.NecessaryEquipmentId,
                        principalTable: "TourEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourTourEquipment_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourTourEquipment1",
                columns: table => new
                {
                    Tour1Id = table.Column<long>(type: "bigint", nullable: false),
                    UnNecessaryEquipmentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTourEquipment1", x => new { x.Tour1Id, x.UnNecessaryEquipmentId });
                    table.ForeignKey(
                        name: "FK_TourTourEquipment1_TourEquipment_UnNecessaryEquipmentId",
                        column: x => x.UnNecessaryEquipmentId,
                        principalTable: "TourEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourTourEquipment1_Tour_Tour1Id",
                        column: x => x.Tour1Id,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTag_ViewCount",
                table: "BlogTag",
                column: "ViewCount");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_CountryId_CityId_StateId",
                table: "Hotel",
                columns: new[] { "CountryId", "CityId", "StateId" });

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_HotelCode",
                table: "Hotel",
                column: "HotelCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_TourId",
                table: "Hotel",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_KeyGroup_EntityId",
                table: "Image",
                columns: new[] { "KeyGroup", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_TourPlan_TourId",
                table: "TourPlan",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTourEquipment_TourId",
                table: "TourTourEquipment",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTourEquipment1_UnNecessaryEquipmentId",
                table: "TourTourEquipment1",
                column: "UnNecessaryEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTransPort_TourId",
                table: "TourTransPort",
                column: "TourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTag");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "TourPlan");

            migrationBuilder.DropTable(
                name: "TourTourEquipment");

            migrationBuilder.DropTable(
                name: "TourTourEquipment1");

            migrationBuilder.DropTable(
                name: "TourTransPort");

            migrationBuilder.DropTable(
                name: "TourEquipment");

            migrationBuilder.CreateTable(
                name: "BlogTags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    HotelCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HotelContinent = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    HotelCoordinate = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    HotelLocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HotelReception = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HotelScore = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    KeyGroup = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourEquipments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourEquipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourPlans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanPictureId = table.Column<string>(type: "varchar(max)", nullable: false),
                    TourId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourPlans_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourTransPorts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TourId = table.Column<long>(type: "bigint", nullable: false),
                    TransPortType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTransPorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourTransPorts_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_BlogTags_ViewCount",
                table: "BlogTags",
                column: "ViewCount");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CountryId_CityId_StateId",
                table: "Hotels",
                columns: new[] { "CountryId", "CityId", "StateId" });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelCode",
                table: "Hotels",
                column: "HotelCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_TourId",
                table: "Hotels",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_KeyGroup_EntityId",
                table: "Images",
                columns: new[] { "KeyGroup", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_TourPlans_TourId",
                table: "TourPlans",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTourEquipments_TourId",
                table: "TourTourEquipments",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTourEquipments1_UnNecessaryEquipmentId",
                table: "TourTourEquipments1",
                column: "UnNecessaryEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTransPorts_TourId",
                table: "TourTransPorts",
                column: "TourId");
        }
    }
}
