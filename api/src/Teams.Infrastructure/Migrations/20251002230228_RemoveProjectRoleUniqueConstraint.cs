using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProjectRoleUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_project_roles_role_id",
                table: "project_roles");

            migrationBuilder.CreateIndex(
                name: "IX_project_roles_role_id",
                table: "project_roles",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_project_roles_role_id",
                table: "project_roles");

            migrationBuilder.CreateIndex(
                name: "IX_project_roles_role_id",
                table: "project_roles",
                column: "role_id",
                unique: true);
        }
    }
}
