using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotelio.Modules.Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BookingInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "booking");

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "booking",
                columns: table => new
                {
                    id = table.Column<string>(name: "_id", type: "nvarchar(450)", nullable: false),
                    amenities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hotelid = table.Column<string>(name: "hotel_id", type: "nvarchar(max)", nullable: false),
                    numberofguests = table.Column<int>(name: "number_of_guests", type: "int", nullable: false),
                    ownerid = table.Column<string>(name: "owner_id", type: "nvarchar(max)", nullable: false),
                    paymenttype = table.Column<int>(name: "payment_type", type: "int", nullable: false),
                    pricepayed = table.Column<double>(name: "price_payed", type: "float", nullable: false),
                    pricetopay = table.Column<double>(name: "price_to_pay", type: "float", nullable: false),
                    roomid = table.Column<string>(name: "room_id", type: "nvarchar(max)", nullable: true),
                    roomtype = table.Column<int>(name: "room_type", type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    startdate = table.Column<DateTime>(name: "start_date", type: "datetime2", nullable: true),
                    enddate = table.Column<DateTime>(name: "end_date", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "booking");
        }
    }
}
