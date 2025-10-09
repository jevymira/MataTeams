using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserGuidToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_skills_users_UserGuid",
                table: "user_skills");

            migrationBuilder.DropIndex(
                name: "IX_user_skills_UserGuid",
                table: "user_skills");

            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "user_skills");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "users",
                newName: "id");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "user_skills",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_skills_UserId1",
                table: "user_skills",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_user_skills_users_UserId1",
                table: "user_skills",
                column: "UserId1",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_skills_users_UserId1",
                table: "user_skills");

            migrationBuilder.DropIndex(
                name: "IX_user_skills_UserId1",
                table: "user_skills");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "user_skills");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "guid");

            migrationBuilder.AddColumn<Guid>(
                name: "UserGuid",
                table: "user_skills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_user_skills_UserGuid",
                table: "user_skills",
                column: "UserGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_user_skills_users_UserGuid",
                table: "user_skills",
                column: "UserGuid",
                principalTable: "users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
