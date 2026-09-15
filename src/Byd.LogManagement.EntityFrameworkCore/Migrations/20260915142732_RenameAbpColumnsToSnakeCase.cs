using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byd.LogManagement.Migrations
{
    /// <inheritdoc />
    public partial class RenameAbpColumnsToSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatorId",
                schema: "log_management",
                table: "ActivityLogs",
                newName: "creator_id");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                schema: "log_management",
                table: "ActivityLogs",
                newName: "creation_time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "creator_id",
                schema: "log_management",
                table: "ActivityLogs",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "creation_time",
                schema: "log_management",
                table: "ActivityLogs",
                newName: "CreationTime");
        }
    }
}
