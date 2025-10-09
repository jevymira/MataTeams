using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGuidAndUserIdFKToUserSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "user_skills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "user_skills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_skills",
                table: "user_skills",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_user_skills_user_id",
                table: "user_skills",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_skills_users_user_id",
                table: "user_skills",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_skills_users_user_id",
                table: "user_skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_skills",
                table: "user_skills");

            migrationBuilder.DropIndex(
                name: "IX_user_skills_user_id",
                table: "user_skills");

            migrationBuilder.DropColumn(
                name: "id",
                table: "user_skills");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "user_skills");
        }
    }
}
