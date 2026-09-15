using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byd.LogManagement.Migrations
{
    /// <inheritdoc />
    public partial class CreateActivityLogsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "log_management");

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                schema: "log_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_name = table.Column<string>(type: "text", nullable: true),
                    organization_name = table.Column<string>(type: "text", nullable: true),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    application_name = table.Column<string>(type: "text", nullable: false),
                    kind = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "jsonb", nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_activity_logs", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs",
                schema: "log_management");
        }
    }
}
