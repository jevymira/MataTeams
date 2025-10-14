using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropRoleIdFKFromProjectRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_roles_roles_role_id",
                table: "project_roles");

            migrationBuilder.DropIndex(
                name: "IX_project_roles_role_id",
                table: "project_roles");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "project_roles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "project_roles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_project_roles_role_id",
                table: "project_roles",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_roles_roles_role_id",
                table: "project_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
