using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byd.LogManagement.Migrations.AuditLogging
{
    /// <inheritdoc />
    public partial class RenameAbpColumnsToSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "log_management",
                table: "EntityPropertyChanges",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "PropertyTypeFullName",
                schema: "log_management",
                table: "EntityPropertyChanges",
                newName: "property_type_full_name");

            migrationBuilder.RenameColumn(
                name: "PropertyName",
                schema: "log_management",
                table: "EntityPropertyChanges",
                newName: "property_name");

            migrationBuilder.RenameColumn(
                name: "OriginalValue",
                schema: "log_management",
                table: "EntityPropertyChanges",
                newName: "original_value");

            migrationBuilder.RenameColumn(
                name: "NewValue",
                schema: "log_management",
                table: "EntityPropertyChanges",
                newName: "new_value");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "log_management",
                table: "EntityChanges",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "ExtraProperties",
                schema: "log_management",
                table: "EntityChanges",
                newName: "extra_properties");

            migrationBuilder.RenameColumn(
                name: "EntityTypeFullName",
                schema: "log_management",
                table: "EntityChanges",
                newName: "entity_type_full_name");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                schema: "log_management",
                table: "EntityChanges",
                newName: "entity_id");

            migrationBuilder.RenameColumn(
                name: "ChangeType",
                schema: "log_management",
                table: "EntityChanges",
                newName: "change_type");

            migrationBuilder.RenameColumn(
                name: "ChangeTime",
                schema: "log_management",
                table: "EntityChanges",
                newName: "change_time");

            migrationBuilder.RenameColumn(
                name: "AuditLogId",
                schema: "log_management",
                table: "EntityChanges",
                newName: "audit_log_id");

            migrationBuilder.RenameColumn(
                name: "Url",
                schema: "log_management",
                table: "AuditLogs",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "Comments",
                schema: "log_management",
                table: "AuditLogs",
                newName: "comments");

            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "log_management",
                table: "AuditLogs",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "log_management",
                table: "AuditLogs",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "TenantName",
                schema: "log_management",
                table: "AuditLogs",
                newName: "tenant_name");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "log_management",
                table: "AuditLogs",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "ImpersonatorUserName",
                schema: "log_management",
                table: "AuditLogs",
                newName: "impersonator_user_name");

            migrationBuilder.RenameColumn(
                name: "ImpersonatorUserId",
                schema: "log_management",
                table: "AuditLogs",
                newName: "impersonator_user_id");

            migrationBuilder.RenameColumn(
                name: "ImpersonatorTenantName",
                schema: "log_management",
                table: "AuditLogs",
                newName: "impersonator_tenant_name");

            migrationBuilder.RenameColumn(
                name: "ImpersonatorTenantId",
                schema: "log_management",
                table: "AuditLogs",
                newName: "impersonator_tenant_id");

            migrationBuilder.RenameColumn(
                name: "HttpStatusCode",
                schema: "log_management",
                table: "AuditLogs",
                newName: "http_status_code");

            migrationBuilder.RenameColumn(
                name: "HttpMethod",
                schema: "log_management",
                table: "AuditLogs",
                newName: "http_method");

            migrationBuilder.RenameColumn(
                name: "ExtraProperties",
                schema: "log_management",
                table: "AuditLogs",
                newName: "extra_properties");

            migrationBuilder.RenameColumn(
                name: "ExecutionDuration",
                schema: "log_management",
                table: "AuditLogs",
                newName: "execution_duration");

            migrationBuilder.RenameColumn(
                name: "CorrelationId",
                schema: "log_management",
                table: "AuditLogs",
                newName: "correlation_id");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                schema: "log_management",
                table: "AuditLogs",
                newName: "concurrency_stamp");

            migrationBuilder.RenameColumn(
                name: "ClientName",
                schema: "log_management",
                table: "AuditLogs",
                newName: "client_name");

            migrationBuilder.RenameColumn(
                name: "ClientIpAddress",
                schema: "log_management",
                table: "AuditLogs",
                newName: "client_ip_address");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                schema: "log_management",
                table: "AuditLogs",
                newName: "client_id");

            migrationBuilder.RenameColumn(
                name: "BrowserInfo",
                schema: "log_management",
                table: "AuditLogs",
                newName: "browser_info");

            migrationBuilder.RenameColumn(
                name: "ApplicationName",
                schema: "log_management",
                table: "AuditLogs",
                newName: "application_name");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "log_management",
                table: "AuditLogExcelFiles",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "FileName",
                schema: "log_management",
                table: "AuditLogExcelFiles",
                newName: "file_name");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                schema: "log_management",
                table: "AuditLogExcelFiles",
                newName: "creator_id");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                schema: "log_management",
                table: "AuditLogExcelFiles",
                newName: "creation_time");

            migrationBuilder.RenameColumn(
                name: "Parameters",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "parameters");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "ServiceName",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "service_name");

            migrationBuilder.RenameColumn(
                name: "MethodName",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "method_name");

            migrationBuilder.RenameColumn(
                name: "ExtraProperties",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "extra_properties");

            migrationBuilder.RenameColumn(
                name: "ExecutionTime",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "execution_time");

            migrationBuilder.RenameColumn(
                name: "ExecutionDuration",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "execution_duration");

            migrationBuilder.RenameColumn(
                name: "AuditLogId",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "audit_log_id");

            // Indeks adi degismez: eski ve yeni ad PostgreSQL'in 63 karakter sinirinda ayni ada kisaliyor.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tenant_id",
                schema: "log_management",
                table: "EntityPropertyChanges",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "property_type_full_name",
                schema: "log_management",
                table: "EntityPropertyChanges",
                newName: "PropertyTypeFullName");

            migrationBuilder.RenameColumn(
                name: "property_name",
                schema: "log_management",
                table: "EntityPropertyChanges",
                newName: "PropertyName");

            migrationBuilder.RenameColumn(
                name: "original_value",
                schema: "log_management",
                table: "EntityPropertyChanges",
                newName: "OriginalValue");

            migrationBuilder.RenameColumn(
                name: "new_value",
                schema: "log_management",
                table: "EntityPropertyChanges",
                newName: "NewValue");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                schema: "log_management",
                table: "EntityChanges",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "extra_properties",
                schema: "log_management",
                table: "EntityChanges",
                newName: "ExtraProperties");

            migrationBuilder.RenameColumn(
                name: "entity_type_full_name",
                schema: "log_management",
                table: "EntityChanges",
                newName: "EntityTypeFullName");

            migrationBuilder.RenameColumn(
                name: "entity_id",
                schema: "log_management",
                table: "EntityChanges",
                newName: "EntityId");

            migrationBuilder.RenameColumn(
                name: "change_type",
                schema: "log_management",
                table: "EntityChanges",
                newName: "ChangeType");

            migrationBuilder.RenameColumn(
                name: "change_time",
                schema: "log_management",
                table: "EntityChanges",
                newName: "ChangeTime");

            migrationBuilder.RenameColumn(
                name: "audit_log_id",
                schema: "log_management",
                table: "EntityChanges",
                newName: "AuditLogId");

            migrationBuilder.RenameColumn(
                name: "url",
                schema: "log_management",
                table: "AuditLogs",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "comments",
                schema: "log_management",
                table: "AuditLogs",
                newName: "Comments");

            migrationBuilder.RenameColumn(
                name: "user_name",
                schema: "log_management",
                table: "AuditLogs",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "log_management",
                table: "AuditLogs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "tenant_name",
                schema: "log_management",
                table: "AuditLogs",
                newName: "TenantName");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                schema: "log_management",
                table: "AuditLogs",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "impersonator_user_name",
                schema: "log_management",
                table: "AuditLogs",
                newName: "ImpersonatorUserName");

            migrationBuilder.RenameColumn(
                name: "impersonator_user_id",
                schema: "log_management",
                table: "AuditLogs",
                newName: "ImpersonatorUserId");

            migrationBuilder.RenameColumn(
                name: "impersonator_tenant_name",
                schema: "log_management",
                table: "AuditLogs",
                newName: "ImpersonatorTenantName");

            migrationBuilder.RenameColumn(
                name: "impersonator_tenant_id",
                schema: "log_management",
                table: "AuditLogs",
                newName: "ImpersonatorTenantId");

            migrationBuilder.RenameColumn(
                name: "http_status_code",
                schema: "log_management",
                table: "AuditLogs",
                newName: "HttpStatusCode");

            migrationBuilder.RenameColumn(
                name: "http_method",
                schema: "log_management",
                table: "AuditLogs",
                newName: "HttpMethod");

            migrationBuilder.RenameColumn(
                name: "extra_properties",
                schema: "log_management",
                table: "AuditLogs",
                newName: "ExtraProperties");

            migrationBuilder.RenameColumn(
                name: "execution_duration",
                schema: "log_management",
                table: "AuditLogs",
                newName: "ExecutionDuration");

            migrationBuilder.RenameColumn(
                name: "correlation_id",
                schema: "log_management",
                table: "AuditLogs",
                newName: "CorrelationId");

            migrationBuilder.RenameColumn(
                name: "concurrency_stamp",
                schema: "log_management",
                table: "AuditLogs",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "client_name",
                schema: "log_management",
                table: "AuditLogs",
                newName: "ClientName");

            migrationBuilder.RenameColumn(
                name: "client_ip_address",
                schema: "log_management",
                table: "AuditLogs",
                newName: "ClientIpAddress");

            migrationBuilder.RenameColumn(
                name: "client_id",
                schema: "log_management",
                table: "AuditLogs",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "browser_info",
                schema: "log_management",
                table: "AuditLogs",
                newName: "BrowserInfo");

            migrationBuilder.RenameColumn(
                name: "application_name",
                schema: "log_management",
                table: "AuditLogs",
                newName: "ApplicationName");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                schema: "log_management",
                table: "AuditLogExcelFiles",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "file_name",
                schema: "log_management",
                table: "AuditLogExcelFiles",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "creator_id",
                schema: "log_management",
                table: "AuditLogExcelFiles",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "creation_time",
                schema: "log_management",
                table: "AuditLogExcelFiles",
                newName: "CreationTime");

            migrationBuilder.RenameColumn(
                name: "parameters",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "Parameters");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "service_name",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "ServiceName");

            migrationBuilder.RenameColumn(
                name: "method_name",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "MethodName");

            migrationBuilder.RenameColumn(
                name: "extra_properties",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "ExtraProperties");

            migrationBuilder.RenameColumn(
                name: "execution_time",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "ExecutionTime");

            migrationBuilder.RenameColumn(
                name: "execution_duration",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "ExecutionDuration");

            migrationBuilder.RenameColumn(
                name: "audit_log_id",
                schema: "log_management",
                table: "AuditLogActions",
                newName: "AuditLogId");
        }
    }
}
