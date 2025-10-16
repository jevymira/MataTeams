using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaderIdToTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "leader_id",
                table: "teams",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_teams_leader_id",
                table: "teams",
                column: "leader_id");

            migrationBuilder.AddForeignKey(
                name: "fk_teams_users_leader_id",
                table: "teams",
                column: "leader_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_teams_users_leader_id",
                table: "teams");

            migrationBuilder.DropIndex(
                name: "ix_teams_leader_id",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "leader_id",
                table: "teams");
        }
    }
}
