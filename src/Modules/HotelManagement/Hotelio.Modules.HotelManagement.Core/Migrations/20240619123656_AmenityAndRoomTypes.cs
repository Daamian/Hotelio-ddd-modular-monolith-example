using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotelio.Modules.HotelManagement.Core.Migrations
{
    /// <inheritdoc />
    public partial class AmenityAndRoomTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxGuests",
                schema: "hotel_management",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "hotel_management",
                table: "Rooms",
                newName: "RoomTypeId");

            migrationBuilder.CreateTable(
                name: "Amenities",
                schema: "hotel_management",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                schema: "hotel_management",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxGuests = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AmenityHotel",
                schema: "hotel_management",
                columns: table => new
                {
                    AmenitiesId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityHotel", x => new { x.AmenitiesId, x.HotelId });
                    table.ForeignKey(
                        name: "FK_AmenityHotel_Amenities_AmenitiesId",
                        column: x => x.AmenitiesId,
                        principalSchema: "hotel_management",
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmenityHotel_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalSchema: "hotel_management",
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                schema: "hotel_management",
                table: "Rooms",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AmenityHotel_HotelId",
                schema: "hotel_management",
                table: "AmenityHotel",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                schema: "hotel_management",
                table: "Rooms",
                column: "RoomTypeId",
                principalSchema: "hotel_management",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                schema: "hotel_management",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "AmenityHotel",
                schema: "hotel_management");

            migrationBuilder.DropTable(
                name: "RoomTypes",
                schema: "hotel_management");

            migrationBuilder.DropTable(
                name: "Amenities",
                schema: "hotel_management");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomTypeId",
                schema: "hotel_management",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "RoomTypeId",
                schema: "hotel_management",
                table: "Rooms",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "MaxGuests",
                schema: "hotel_management",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
