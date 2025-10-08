using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropProjectPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_roles_projects_ProjectId",
                table: "project_roles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_projects_Guid",
                table: "projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_projects",
                table: "projects");

            migrationBuilder.DropIndex(
                name: "IX_project_roles_ProjectId",
                table: "project_roles");

            migrationBuilder.DropColumn(
                name: "id",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "project_roles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "projects",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "project_roles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_projects_Guid",
                table: "projects",
                column: "Guid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_projects",
                table: "projects",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_project_roles_ProjectId",
                table: "project_roles",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_project_roles_projects_ProjectId",
                table: "project_roles",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "id");
        }
    }
}
