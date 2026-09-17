using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byd.LogManagement.Migrations
{
    /// <inheritdoc />
    public partial class CreateExternalServiceLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExternalServiceLogs",
                schema: "log_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    correlation_id = table.Column<string>(type: "text", nullable: true),
                    external_service_provider_name = table.Column<string>(type: "text", nullable: false),
                    module_name = table.Column<string>(type: "text", nullable: false),
                    method_name = table.Column<string>(type: "text", nullable: false),
                    request_type = table.Column<string>(type: "text", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    execution_duration = table.Column<double>(type: "double precision", nullable: false),
                    response_code = table.Column<int>(type: "integer", nullable: false),
                    exception_message = table.Column<string>(type: "text", nullable: true),
                    response_data = table.Column<string>(type: "text", nullable: true),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_external_service_logs", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalServiceLogs",
                schema: "log_management");
        }
    }
}
