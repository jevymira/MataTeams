using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdFKToTeamMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "project_role_id",
                table: "team_membership_request",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "project_role_id",
                table: "team_members",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_team_membership_request_project_role_id",
                table: "team_membership_request",
                column: "project_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_team_members_project_role_id",
                table: "team_members",
                column: "project_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_team_members_user_id",
                table: "team_members",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_team_members_project_roles_project_role_id",
                table: "team_members",
                column: "project_role_id",
                principalTable: "project_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_team_members_users_user_id",
                table: "team_members",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_team_membership_request_project_roles_project_role_id",
                table: "team_membership_request",
                column: "project_role_id",
                principalTable: "project_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_team_members_project_roles_project_role_id",
                table: "team_members");

            migrationBuilder.DropForeignKey(
                name: "fk_team_members_users_user_id",
                table: "team_members");

            migrationBuilder.DropForeignKey(
                name: "fk_team_membership_request_project_roles_project_role_id",
                table: "team_membership_request");

            migrationBuilder.DropIndex(
                name: "ix_team_membership_request_project_role_id",
                table: "team_membership_request");

            migrationBuilder.DropIndex(
                name: "ix_team_members_project_role_id",
                table: "team_members");

            migrationBuilder.DropIndex(
                name: "ix_team_members_user_id",
                table: "team_members");

            migrationBuilder.DropColumn(
                name: "project_role_id",
                table: "team_membership_request");

            migrationBuilder.DropColumn(
                name: "project_role_id",
                table: "team_members");
        }
    }
}
