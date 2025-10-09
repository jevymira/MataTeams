using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropProjectRoleIdFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_role_skills_project_roles_project_role_id",
                table: "project_role_skills");

            migrationBuilder.RenameColumn(
                name: "project_role_id",
                table: "project_role_skills",
                newName: "ProjectRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_project_role_skills_project_role_id",
                table: "project_role_skills",
                newName: "IX_project_role_skills_ProjectRoleId");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectRoleId",
                table: "project_role_skills",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_project_role_skills_project_roles_ProjectRoleId",
                table: "project_role_skills",
                column: "ProjectRoleId",
                principalTable: "project_roles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_role_skills_project_roles_ProjectRoleId",
                table: "project_role_skills");

            migrationBuilder.RenameColumn(
                name: "ProjectRoleId",
                table: "project_role_skills",
                newName: "project_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_project_role_skills_ProjectRoleId",
                table: "project_role_skills",
                newName: "IX_project_role_skills_project_role_id");

            migrationBuilder.AlterColumn<int>(
                name: "project_role_id",
                table: "project_role_skills",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_project_role_skills_project_roles_project_role_id",
                table: "project_role_skills",
                column: "project_role_id",
                principalTable: "project_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
