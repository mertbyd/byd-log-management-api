using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byd.LogManagement.Migrations
{
    /// <inheritdoc />
    public partial class CreateMobileNotificationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "read_time",
                schema: "log_management",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "log_management",
                table: "Notifications",
                newName: "data");

            migrationBuilder.AddColumn<Guid>(
                name: "firebase_project_id",
                schema: "log_management",
                table: "Notifications",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "topic",
                schema: "log_management",
                table: "Notifications",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FirebaseProjects",
                schema: "log_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    key = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_firebase_projects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MobileNotifications",
                schema: "log_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    notification_id = table.Column<Guid>(type: "uuid", nullable: false),
                    target = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    is_success = table.Column<bool>(type: "boolean", nullable: false),
                    status_code = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    error_message = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    execution_duration = table.Column<double>(type: "double precision", nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mobile_notifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_mobile_notifications_notifications_notification_id",
                        column: x => x.notification_id,
                        principalSchema: "log_management",
                        principalTable: "Notifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMobileTokens",
                schema: "log_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    firebase_project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    language_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    creation_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modification_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modifier_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_mobile_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_mobile_tokens_firebase_projects_firebase_project_id",
                        column: x => x.firebase_project_id,
                        principalSchema: "log_management",
                        principalTable: "FirebaseProjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_firebase_projects_tenant_id_key",
                schema: "log_management",
                table: "FirebaseProjects",
                columns: new[] { "tenant_id", "key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_firebase_projects_tenant_id_name",
                schema: "log_management",
                table: "FirebaseProjects",
                columns: new[] { "tenant_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_mobile_notifications_notification_id",
                schema: "log_management",
                table: "MobileNotifications",
                column: "notification_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_mobile_tokens_firebase_project_id",
                schema: "log_management",
                table: "UserMobileTokens",
                column: "firebase_project_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_mobile_tokens_user_id_firebase_project_id",
                schema: "log_management",
                table: "UserMobileTokens",
                columns: new[] { "user_id", "firebase_project_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileNotifications",
                schema: "log_management");

            migrationBuilder.DropTable(
                name: "UserMobileTokens",
                schema: "log_management");

            migrationBuilder.DropTable(
                name: "FirebaseProjects",
                schema: "log_management");

            migrationBuilder.DropColumn(
                name: "firebase_project_id",
                schema: "log_management",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "topic",
                schema: "log_management",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "data",
                schema: "log_management",
                table: "Notifications",
                newName: "description");

            migrationBuilder.AddColumn<DateTime>(
                name: "read_time",
                schema: "log_management",
                table: "Notifications",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
