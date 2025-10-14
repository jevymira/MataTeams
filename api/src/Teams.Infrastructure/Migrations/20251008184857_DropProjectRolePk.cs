using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropProjectRolePk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_role_skills_project_roles_ProjectRoleId",
                table: "project_role_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_project_roles_projects_project_id",
                table: "project_roles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_project_roles_Guid",
                table: "project_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project_roles",
                table: "project_roles");

            migrationBuilder.DropIndex(
                name: "IX_project_roles_project_id",
                table: "project_roles");

            migrationBuilder.DropIndex(
                name: "IX_project_role_skills_ProjectRoleId",
                table: "project_role_skills");

            migrationBuilder.DropColumn(
                name: "id",
                table: "project_roles");

            migrationBuilder.DropColumn(
                name: "ProjectRoleId",
                table: "project_role_skills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "project_roles",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ProjectRoleId",
                table: "project_role_skills",
                type: "integer",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_project_roles_Guid",
                table: "project_roles",
                column: "Guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_roles",
                table: "project_roles",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_project_roles_project_id",
                table: "project_roles",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_role_skills_ProjectRoleId",
                table: "project_role_skills",
                column: "ProjectRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_project_role_skills_project_roles_ProjectRoleId",
                table: "project_role_skills",
                column: "ProjectRoleId",
                principalTable: "project_roles",
                principalColumn: "id");

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
