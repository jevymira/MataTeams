using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProjectRoleSkillUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_role_skills_skills_skill_id",
                table: "project_role_skills");

            migrationBuilder.DropIndex(
                name: "IX_project_role_skills_skill_id",
                table: "project_role_skills");

            migrationBuilder.CreateIndex(
                name: "IX_project_role_skills_skill_id",
                table: "project_role_skills",
                column: "skill_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_role_skills_skills_skill_id",
                table: "project_role_skills",
                column: "skill_id",
                principalTable: "skills",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_role_skills_skills_skill_id",
                table: "project_role_skills");

            migrationBuilder.DropIndex(
                name: "IX_project_role_skills_skill_id",
                table: "project_role_skills");

            migrationBuilder.CreateIndex(
                name: "IX_project_role_skills_skill_id",
                table: "project_role_skills",
                column: "skill_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_project_role_skills_skills_skill_id",
                table: "project_role_skills",
                column: "skill_id",
                principalTable: "skills",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
