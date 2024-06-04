using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotelio.Modules.Availability.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AvailabilityRemoveGroupAndType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "availability",
                table: "Resources");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                schema: "availability",
                table: "Resources",
                newName: "ExternalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExternalId",
                schema: "availability",
                table: "Resources",
                newName: "GroupId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "availability",
                table: "Resources",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
