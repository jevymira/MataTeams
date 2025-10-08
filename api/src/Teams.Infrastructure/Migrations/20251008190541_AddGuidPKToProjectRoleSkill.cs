using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGuidPKToProjectRoleSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "project_role_skills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_role_skills",
                table: "project_role_skills",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_project_role_skills_project_role_id",
                table: "project_role_skills",
                column: "project_role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_role_skills_project_roles_project_role_id",
                table: "project_role_skills",
                column: "project_role_id",
                principalTable: "project_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_role_skills_project_roles_project_role_id",
                table: "project_role_skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project_role_skills",
                table: "project_role_skills");

            migrationBuilder.DropIndex(
                name: "IX_project_role_skills_project_role_id",
                table: "project_role_skills");

            migrationBuilder.DropColumn(
                name: "id",
                table: "project_role_skills");
        }
    }
}
