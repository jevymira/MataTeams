using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteMemberFromTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_ProjectRoles_ProjectRoleId",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Teams_TeamId",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembershipRequests_ProjectRoles_ProjectRoleId",
                table: "TeamMembershipRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembershipRequests_Teams_TeamId",
                table: "TeamMembershipRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_ProjectRoles_ProjectRoleId",
                table: "TeamMembers",
                column: "ProjectRoleId",
                principalTable: "ProjectRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Teams_TeamId",
                table: "TeamMembers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembershipRequests_ProjectRoles_ProjectRoleId",
                table: "TeamMembershipRequests",
                column: "ProjectRoleId",
                principalTable: "ProjectRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembershipRequests_Teams_TeamId",
                table: "TeamMembershipRequests",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_ProjectRoles_ProjectRoleId",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Teams_TeamId",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembershipRequests_ProjectRoles_ProjectRoleId",
                table: "TeamMembershipRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembershipRequests_Teams_TeamId",
                table: "TeamMembershipRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_ProjectRoles_ProjectRoleId",
                table: "TeamMembers",
                column: "ProjectRoleId",
                principalTable: "ProjectRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Teams_TeamId",
                table: "TeamMembers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembershipRequests_ProjectRoles_ProjectRoleId",
                table: "TeamMembershipRequests",
                column: "ProjectRoleId",
                principalTable: "ProjectRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembershipRequests_Teams_TeamId",
                table: "TeamMembershipRequests",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
