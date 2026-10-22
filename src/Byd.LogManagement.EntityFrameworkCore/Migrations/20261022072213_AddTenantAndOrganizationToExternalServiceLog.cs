using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byd.LogManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantAndOrganizationToExternalServiceLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "organization_id",
                schema: "log_management",
                table: "ExternalServiceLogs",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "organization_name",
                schema: "log_management",
                table: "ExternalServiceLogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "log_management",
                table: "ExternalServiceLogs",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tenant_name",
                schema: "log_management",
                table: "ExternalServiceLogs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "organization_id",
                schema: "log_management",
                table: "ExternalServiceLogs");

            migrationBuilder.DropColumn(
                name: "organization_name",
                schema: "log_management",
                table: "ExternalServiceLogs");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "log_management",
                table: "ExternalServiceLogs");

            migrationBuilder.DropColumn(
                name: "tenant_name",
                schema: "log_management",
                table: "ExternalServiceLogs");
        }
    }
}
