using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSurrogateKeyToProjectRoleSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_project_role_skills",
                table: "project_role_skills");

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
                name: "IX_project_role_skills_project_role_id",
                table: "project_role_skills",
                column: "project_role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_project_role_skills",
                table: "project_role_skills");

            migrationBuilder.DropIndex(
                name: "IX_project_role_skills_project_role_id",
                table: "project_role_skills");

            migrationBuilder.DropColumn(
                name: "id",
                table: "project_role_skills");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_role_skills",
                table: "project_role_skills",
                columns: new[] { "project_role_id", "skill_id" });
        }
    }
}
