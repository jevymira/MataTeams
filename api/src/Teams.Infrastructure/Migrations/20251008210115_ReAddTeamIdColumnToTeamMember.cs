using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReAddTeamIdColumnToTeamMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "team_members");

            migrationBuilder.AddColumn<Guid>(
                name: "team_id",
                table: "team_members",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_team_members_team_id",
                table: "team_members",
                column: "team_id");

            migrationBuilder.AddForeignKey(
                name: "FK_team_members_teams_team_id",
                table: "team_members",
                column: "team_id",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_team_members_teams_team_id",
                table: "team_members");

            migrationBuilder.DropIndex(
                name: "IX_team_members_team_id",
                table: "team_members");

            migrationBuilder.DropColumn(
                name: "team_id",
                table: "team_members");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "team_members",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
