using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseGuidForUserPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_skills_users_user_id",
                table: "user_skills");

            migrationBuilder.DropUniqueConstraint(
                name: "guid",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropColumn(
                name: "id",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "users",
                newName: "guid");

            migrationBuilder.AddColumn<Guid>(
                name: "UserGuid",
                table: "user_skills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "guid");

            migrationBuilder.CreateIndex(
                name: "IX_user_skills_UserGuid",
                table: "user_skills",
                column: "UserGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_user_skills_users_UserGuid",
                table: "user_skills",
                column: "UserGuid",
                principalTable: "users",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_skills_users_UserGuid",
                table: "user_skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_user_skills_UserGuid",
                table: "user_skills");

            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "user_skills");

            migrationBuilder.RenameColumn(
                name: "guid",
                table: "users",
                newName: "Guid");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "guid",
                table: "users",
                column: "Guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_skills_users_user_id",
                table: "user_skills",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
