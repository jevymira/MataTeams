using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReAddColumnsWithSkillGuidAsPrincipalKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "skill_id",
                table: "user_skills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "skill_id",
                table: "project_role_skills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_user_skills_skill_id",
                table: "user_skills",
                column: "skill_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_user_skills_skills_skill_id",
                table: "user_skills",
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

            migrationBuilder.DropForeignKey(
                name: "FK_user_skills_skills_skill_id",
                table: "user_skills");

            migrationBuilder.DropIndex(
                name: "IX_user_skills_skill_id",
                table: "user_skills");

            migrationBuilder.DropIndex(
                name: "IX_project_role_skills_skill_id",
                table: "project_role_skills");

            migrationBuilder.DropColumn(
                name: "skill_id",
                table: "user_skills");

            migrationBuilder.DropColumn(
                name: "skill_id",
                table: "project_role_skills");
        }
    }
}
