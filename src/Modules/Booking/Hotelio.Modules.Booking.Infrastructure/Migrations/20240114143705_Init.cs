using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotelio.Modules.Booking.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "booking");

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "booking",
                columns: table => new
                {
                    _id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    amenities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hotel_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    number_of_guests = table.Column<int>(type: "int", nullable: false),
                    owner_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    payment_type = table.Column<int>(type: "int", nullable: false),
                    price_payed = table.Column<double>(type: "float", nullable: false),
                    price_to_pay = table.Column<double>(type: "float", nullable: false),
                    room_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    room_type = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x._id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "booking");
        }
    }
}
