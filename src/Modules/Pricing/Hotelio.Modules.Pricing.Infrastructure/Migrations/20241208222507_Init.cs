using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotelio.Modules.Pricing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pricing");

            migrationBuilder.CreateTable(
                name: "HotelTariffs",
                schema: "pricing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePriceNetAmount = table.Column<double>(type: "float", nullable: false),
                    BasePriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePriceTaxRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelTariffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AmenityTariffs",
                schema: "pricing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmenityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceNetAmount = table.Column<double>(type: "float", nullable: false),
                    PriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceTaxRate = table.Column<int>(type: "int", nullable: false),
                    HotelTariffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityTariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmenityTariffs_HotelTariffs_HotelTariffId",
                        column: x => x.HotelTariffId,
                        principalSchema: "pricing",
                        principalTable: "HotelTariffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoomTariffs",
                schema: "pricing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomTypeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePriceNetAmount = table.Column<double>(type: "float", nullable: false),
                    BasePriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePriceTaxRate = table.Column<int>(type: "int", nullable: false),
                    HotelTariffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomTariffs_HotelTariffs_HotelTariffId",
                        column: x => x.HotelTariffId,
                        principalSchema: "pricing",
                        principalTable: "HotelTariffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PeriodPrices",
                schema: "pricing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceNetAmount = table.Column<double>(type: "float", nullable: false),
                    PriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceTaxRate = table.Column<int>(type: "int", nullable: false),
                    RoomTariffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodPrices_RoomTariffs_RoomTariffId",
                        column: x => x.RoomTariffId,
                        principalSchema: "pricing",
                        principalTable: "RoomTariffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmenityTariffs_HotelTariffId",
                schema: "pricing",
                table: "AmenityTariffs",
                column: "HotelTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodPrices_RoomTariffId",
                schema: "pricing",
                table: "PeriodPrices",
                column: "RoomTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTariffs_HotelTariffId",
                schema: "pricing",
                table: "RoomTariffs",
                column: "HotelTariffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenityTariffs",
                schema: "pricing");

            migrationBuilder.DropTable(
                name: "PeriodPrices",
                schema: "pricing");

            migrationBuilder.DropTable(
                name: "RoomTariffs",
                schema: "pricing");

            migrationBuilder.DropTable(
                name: "HotelTariffs",
                schema: "pricing");
        }
    }
}
