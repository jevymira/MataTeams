using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropProjectIdFKFromProjectRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_roles_projects_project_id",
                table: "project_roles");

            migrationBuilder.RenameColumn(
                name: "project_id",
                table: "project_roles",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_project_roles_project_id",
                table: "project_roles",
                newName: "IX_project_roles_ProjectId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "project_roles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_project_roles_projects_ProjectId",
                table: "project_roles",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_roles_projects_ProjectId",
                table: "project_roles");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "project_roles",
                newName: "project_id");

            migrationBuilder.RenameIndex(
                name: "IX_project_roles_ProjectId",
                table: "project_roles",
                newName: "IX_project_roles_project_id");

            migrationBuilder.AlterColumn<int>(
                name: "project_id",
                table: "project_roles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_project_roles_projects_project_id",
                table: "project_roles",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
