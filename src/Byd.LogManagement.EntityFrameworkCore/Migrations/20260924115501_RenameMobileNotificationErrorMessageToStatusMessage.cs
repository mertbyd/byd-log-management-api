using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byd.LogManagement.Migrations
{
    /// <inheritdoc />
    public partial class RenameMobileNotificationErrorMessageToStatusMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "error_message",
                schema: "log_management",
                table: "MobileNotifications",
                newName: "status_message");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status_message",
                schema: "log_management",
                table: "MobileNotifications",
                newName: "error_message");
        }
    }
}
