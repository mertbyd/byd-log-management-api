using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byd.LogManagement.Migrations.AuditLogging
{
    /// <inheritdoc />
    public partial class CreateAuditLoggingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "log_management");

            migrationBuilder.CreateTable(
                name: "AuditLogExcelFiles",
                schema: "log_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    FileName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_log_excel_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "log_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationName = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ImpersonatorUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ImpersonatorUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ImpersonatorTenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ImpersonatorTenantName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    execution_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "integer", nullable: false),
                    ClientIpAddress = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ClientId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    CorrelationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    BrowserInfo = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    HttpMethod = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    Url = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    exceptions = table.Column<string>(type: "text", nullable: true),
                    Comments = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    HttpStatusCode = table.Column<int>(type: "integer", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogActions",
                schema: "log_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuditLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    MethodName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Parameters = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_log_actions", x => x.id);
                    table.ForeignKey(
                        name: "fk_audit_log_actions_audit_logs_audit_log_id",
                        column: x => x.AuditLogId,
                        principalSchema: "log_management",
                        principalTable: "AuditLogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityChanges",
                schema: "log_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChangeTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChangeType = table.Column<byte>(type: "smallint", nullable: false),
                    entity_tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    EntityTypeFullName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entity_changes", x => x.id);
                    table.ForeignKey(
                        name: "fk_entity_changes_audit_logs_audit_log_id",
                        column: x => x.AuditLogId,
                        principalSchema: "log_management",
                        principalTable: "AuditLogs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityPropertyChanges",
                schema: "log_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    entity_change_id = table.Column<Guid>(type: "uuid", nullable: false),
                    NewValue = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    OriginalValue = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    PropertyName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PropertyTypeFullName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entity_property_changes", x => x.id);
                    table.ForeignKey(
                        name: "fk_entity_property_changes_entity_changes_entity_change_id",
                        column: x => x.entity_change_id,
                        principalSchema: "log_management",
                        principalTable: "EntityChanges",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_audit_log_actions_audit_log_id",
                schema: "log_management",
                table: "AuditLogActions",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "ix_audit_log_actions_tenant_id_service_name_method_name_execution_ti",
                schema: "log_management",
                table: "AuditLogActions",
                columns: new[] { "TenantId", "ServiceName", "MethodName", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "ix_audit_logs_tenant_id_execution_time",
                schema: "log_management",
                table: "AuditLogs",
                columns: new[] { "TenantId", "execution_time" });

            migrationBuilder.CreateIndex(
                name: "ix_audit_logs_tenant_id_user_id_execution_time",
                schema: "log_management",
                table: "AuditLogs",
                columns: new[] { "TenantId", "UserId", "execution_time" });

            migrationBuilder.CreateIndex(
                name: "ix_entity_changes_audit_log_id",
                schema: "log_management",
                table: "EntityChanges",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "ix_entity_changes_tenant_id_entity_type_full_name_entity_id",
                schema: "log_management",
                table: "EntityChanges",
                columns: new[] { "TenantId", "EntityTypeFullName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "ix_entity_property_changes_entity_change_id",
                schema: "log_management",
                table: "EntityPropertyChanges",
                column: "entity_change_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogActions",
                schema: "log_management");

            migrationBuilder.DropTable(
                name: "AuditLogExcelFiles",
                schema: "log_management");

            migrationBuilder.DropTable(
                name: "EntityPropertyChanges",
                schema: "log_management");

            migrationBuilder.DropTable(
                name: "EntityChanges",
                schema: "log_management");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "log_management");
        }
    }
}
