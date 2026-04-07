using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCanCopyAndCopyOfToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanCopy",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CopyOf",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CopyOf",
                table: "Projects",
                column: "CopyOf");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Projects_CopyOf",
                table: "Projects",
                column: "CopyOf",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Projects_CopyOf",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CopyOf",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CanCopy",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CopyOf",
                table: "Projects");
        }
    }
}
