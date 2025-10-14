using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserSkillFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_skills_skills_skill_id",
                table: "user_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_user_skills_users_UserId1",
                table: "user_skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_skills",
                table: "user_skills");

            migrationBuilder.DropIndex(
                name: "IX_user_skills_skill_id",
                table: "user_skills");

            migrationBuilder.DropIndex(
                name: "IX_user_skills_UserId1",
                table: "user_skills");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "user_skills");

            migrationBuilder.DropColumn(
                name: "skill_id",
                table: "user_skills");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "user_skills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "user_skills",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "skill_id",
                table: "user_skills",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "user_skills",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_skills",
                table: "user_skills",
                columns: new[] { "user_id", "skill_id" });

            migrationBuilder.CreateIndex(
                name: "IX_user_skills_skill_id",
                table: "user_skills",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_skills_UserId1",
                table: "user_skills",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_user_skills_skills_skill_id",
                table: "user_skills",
                column: "skill_id",
                principalTable: "skills",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_skills_users_UserId1",
                table: "user_skills",
                column: "UserId1",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
