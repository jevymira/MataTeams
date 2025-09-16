#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_AspNetUsers_user_id",
                table: "projects");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "projects",
                newName: "owner_id");

            migrationBuilder.RenameIndex(
                name: "IX_projects_user_id",
                table: "projects",
                newName: "IX_projects_owner_id");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_AspNetUsers_owner_id",
                table: "projects",
                column: "owner_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_AspNetUsers_owner_id",
                table: "projects");

            migrationBuilder.RenameColumn(
                name: "owner_id",
                table: "projects",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_projects_owner_id",
                table: "projects",
                newName: "IX_projects_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_AspNetUsers_user_id",
                table: "projects",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
