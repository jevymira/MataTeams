using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerGuidToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "owner_id",
                table: "projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_projects_owner_id",
                table: "projects",
                column: "owner_id");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_users_owner_id",
                table: "projects",
                column: "owner_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_users_owner_id",
                table: "projects");

            migrationBuilder.DropIndex(
                name: "IX_projects_owner_id",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "projects");
        }
    }
}
