using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamsNavigationToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "project_id",
                table: "teams",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_teams_project_id",
                table: "teams",
                column: "project_id");

            migrationBuilder.AddForeignKey(
                name: "fk_teams_projects_project_id",
                table: "teams",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_teams_projects_project_id",
                table: "teams");

            migrationBuilder.DropIndex(
                name: "ix_teams_project_id",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "project_id",
                table: "teams");
        }
    }
}
