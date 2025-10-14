using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropProjectRoleSkillPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_role_skills_project_roles_ProjectRoleId",
                table: "project_role_skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project_role_skills",
                table: "project_role_skills");

            migrationBuilder.DropIndex(
                name: "IX_project_role_skills_ProjectRoleId",
                table: "project_role_skills");

            migrationBuilder.DropColumn(
                name: "id",
                table: "project_role_skills");

            migrationBuilder.RenameColumn(
                name: "ProjectRoleId",
                table: "project_role_skills",
                newName: "project_role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "project_role_id",
                table: "project_role_skills",
                newName: "ProjectRoleId");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "project_role_skills",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_role_skills",
                table: "project_role_skills",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_project_role_skills_ProjectRoleId",
                table: "project_role_skills",
                column: "ProjectRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_project_role_skills_project_roles_ProjectRoleId",
                table: "project_role_skills",
                column: "ProjectRoleId",
                principalTable: "project_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
