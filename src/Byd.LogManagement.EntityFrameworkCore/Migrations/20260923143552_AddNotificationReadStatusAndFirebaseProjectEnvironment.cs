using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byd.LogManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationReadStatusAndFirebaseProjectEnvironment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_firebase_projects_tenant_id_key",
                schema: "log_management",
                table: "FirebaseProjects");

            migrationBuilder.DropIndex(
                name: "ix_firebase_projects_tenant_id_name",
                schema: "log_management",
                table: "FirebaseProjects");

            migrationBuilder.AddColumn<bool>(
                name: "is_read",
                schema: "log_management",
                table: "Notifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "environment",
                schema: "log_management",
                table: "FirebaseProjects",
                type: "character varying(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.Sql(
                "UPDATE log_management.\"FirebaseProjects\" SET environment = 'legacy' WHERE environment IS NULL;");

            migrationBuilder.AlterColumn<string>(
                name: "environment",
                schema: "log_management",
                table: "FirebaseProjects",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_firebase_projects_tenant_id_environment_key",
                schema: "log_management",
                table: "FirebaseProjects",
                columns: new[] { "tenant_id", "environment", "key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_firebase_projects_tenant_id_environment_name",
                schema: "log_management",
                table: "FirebaseProjects",
                columns: new[] { "tenant_id", "environment", "name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_firebase_projects_tenant_id_environment_key",
                schema: "log_management",
                table: "FirebaseProjects");

            migrationBuilder.DropIndex(
                name: "ix_firebase_projects_tenant_id_environment_name",
                schema: "log_management",
                table: "FirebaseProjects");

            migrationBuilder.DropColumn(
                name: "is_read",
                schema: "log_management",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "environment",
                schema: "log_management",
                table: "FirebaseProjects");

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
        }
    }
}
