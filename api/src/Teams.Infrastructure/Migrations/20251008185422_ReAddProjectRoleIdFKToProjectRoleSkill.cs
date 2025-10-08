using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReAddProjectRoleIdFKToProjectRoleSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectRoleId",
                table: "project_role_skills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_roles_projects_project_id",
                table: "project_roles",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_role_skills_project_roles_ProjectRoleId",
                table: "project_role_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_project_roles_projects_project_id",
                table: "project_roles");

            migrationBuilder.DropIndex(
                name: "IX_project_roles_project_id",
                table: "project_roles");

            migrationBuilder.DropIndex(
                name: "IX_project_role_skills_ProjectRoleId",
                table: "project_role_skills");

            migrationBuilder.DropColumn(
                name: "ProjectRoleId",
                table: "project_role_skills");
        }
    }
}
