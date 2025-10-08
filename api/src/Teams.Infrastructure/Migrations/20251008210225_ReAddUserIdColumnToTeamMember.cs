using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReAddUserIdColumnToTeamMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "team_members",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "team_members");
        }
    }
}
