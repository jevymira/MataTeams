using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReAddTeamMembershipRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_team_membership_request_project_roles_project_role_id",
                table: "team_membership_request");

            migrationBuilder.DropForeignKey(
                name: "fk_team_membership_request_teams_team_id",
                table: "team_membership_request");

            migrationBuilder.DropForeignKey(
                name: "fk_team_membership_request_users_user_id",
                table: "team_membership_request");

            migrationBuilder.DropPrimaryKey(
                name: "pk_team_membership_request",
                table: "team_membership_request");

            migrationBuilder.RenameTable(
                name: "team_membership_request",
                newName: "team_membership_requests");

            migrationBuilder.RenameIndex(
                name: "ix_team_membership_request_user_id",
                table: "team_membership_requests",
                newName: "ix_team_membership_requests_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_team_membership_request_team_id",
                table: "team_membership_requests",
                newName: "ix_team_membership_requests_team_id");

            migrationBuilder.RenameIndex(
                name: "ix_team_membership_request_project_role_id",
                table: "team_membership_requests",
                newName: "ix_team_membership_requests_project_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_team_membership_requests",
                table: "team_membership_requests",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_team_membership_requests_project_roles_project_role_id",
                table: "team_membership_requests",
                column: "project_role_id",
                principalTable: "project_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_team_membership_requests_teams_team_id",
                table: "team_membership_requests",
                column: "team_id",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_team_membership_requests_users_user_id",
                table: "team_membership_requests",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_team_membership_requests_project_roles_project_role_id",
                table: "team_membership_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_team_membership_requests_teams_team_id",
                table: "team_membership_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_team_membership_requests_users_user_id",
                table: "team_membership_requests");

            migrationBuilder.DropPrimaryKey(
                name: "pk_team_membership_requests",
                table: "team_membership_requests");

            migrationBuilder.RenameTable(
                name: "team_membership_requests",
                newName: "team_membership_request");

            migrationBuilder.RenameIndex(
                name: "ix_team_membership_requests_user_id",
                table: "team_membership_request",
                newName: "ix_team_membership_request_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_team_membership_requests_team_id",
                table: "team_membership_request",
                newName: "ix_team_membership_request_team_id");

            migrationBuilder.RenameIndex(
                name: "ix_team_membership_requests_project_role_id",
                table: "team_membership_request",
                newName: "ix_team_membership_request_project_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_team_membership_request",
                table: "team_membership_request",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_team_membership_request_project_roles_project_role_id",
                table: "team_membership_request",
                column: "project_role_id",
                principalTable: "project_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_team_membership_request_teams_team_id",
                table: "team_membership_request",
                column: "team_id",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_team_membership_request_users_user_id",
                table: "team_membership_request",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
