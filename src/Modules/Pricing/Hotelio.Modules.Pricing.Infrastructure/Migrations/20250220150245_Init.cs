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
                name: "AmenityTariff",
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
                    table.PrimaryKey("PK_AmenityTariff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmenityTariff_HotelTariffs_HotelTariffId",
                        column: x => x.HotelTariffId,
                        principalSchema: "pricing",
                        principalTable: "HotelTariffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoomTariff",
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
                    table.PrimaryKey("PK_RoomTariff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomTariff_HotelTariffs_HotelTariffId",
                        column: x => x.HotelTariffId,
                        principalSchema: "pricing",
                        principalTable: "HotelTariffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PeriodPrice",
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
                    table.PrimaryKey("PK_PeriodPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodPrice_RoomTariff_RoomTariffId",
                        column: x => x.RoomTariffId,
                        principalSchema: "pricing",
                        principalTable: "RoomTariff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmenityTariff_HotelTariffId",
                schema: "pricing",
                table: "AmenityTariff",
                column: "HotelTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_PeriodPrice_RoomTariffId",
                schema: "pricing",
                table: "PeriodPrice",
                column: "RoomTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTariff_HotelTariffId",
                schema: "pricing",
                table: "RoomTariff",
                column: "HotelTariffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenityTariff",
                schema: "pricing");

            migrationBuilder.DropTable(
                name: "PeriodPrice",
                schema: "pricing");

            migrationBuilder.DropTable(
                name: "RoomTariff",
                schema: "pricing");

            migrationBuilder.DropTable(
                name: "HotelTariffs",
                schema: "pricing");
        }
    }
}
