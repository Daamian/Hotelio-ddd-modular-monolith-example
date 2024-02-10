using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotelio.Modules.Availability.Infrastructure.Migrations
{
    public partial class AvailabilityInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "availability");

            migrationBuilder.CreateTable(
                name: "Resources",
                schema: "availability",
                columns: table => new
                {
                    _id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    group_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "availability",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    resource_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Book_Resources_resource_id",
                        column: x => x.resource_id,
                        principalSchema: "availability",
                        principalTable: "Resources",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_resource_id",
                schema: "availability",
                table: "Book",
                column: "resource_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book",
                schema: "availability");

            migrationBuilder.DropTable(
                name: "Resources",
                schema: "availability");
        }
    }
}
