using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropTeamMemberPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_team_members_teams_team_id",
                table: "team_members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_team_members",
                table: "team_members");

            migrationBuilder.DropIndex(
                name: "IX_team_members_team_id",
                table: "team_members");

            migrationBuilder.DropColumn(
                name: "id",
                table: "team_members");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "team_members");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "team_members",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "team_members",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_team_members",
                table: "team_members",
                column: "id");

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
    }
}
