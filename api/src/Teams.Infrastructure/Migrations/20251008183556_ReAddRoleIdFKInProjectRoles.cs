using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReAddRoleIdFKInProjectRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "role_id",
                table: "project_roles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_project_roles_role_id",
                table: "project_roles",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_roles_roles_role_id",
                table: "project_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_roles_roles_role_id",
                table: "project_roles");

            migrationBuilder.DropIndex(
                name: "IX_project_roles_role_id",
                table: "project_roles");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "project_roles");
        }
    }
}
