using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teams.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseSnakeCaseNamingConvention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_role_skills_project_roles_project_role_id",
                table: "project_role_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_project_role_skills_skills_skill_id",
                table: "project_role_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_project_roles_projects_project_id",
                table: "project_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_project_roles_roles_role_id",
                table: "project_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_projects_users_owner_id",
                table: "projects");

            migrationBuilder.DropForeignKey(
                name: "FK_team_members_teams_team_id",
                table: "team_members");

            migrationBuilder.DropForeignKey(
                name: "FK_user_skills_skills_skill_id",
                table: "user_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_user_skills_users_user_id",
                table: "user_skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_skills",
                table: "user_skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_teams",
                table: "teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_team_members",
                table: "team_members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_skills",
                table: "skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_projects",
                table: "projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project_roles",
                table: "project_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project_role_skills",
                table: "project_role_skills");

            migrationBuilder.RenameIndex(
                name: "IX_user_skills_user_id",
                table: "user_skills",
                newName: "ix_user_skills_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_skills_skill_id",
                table: "user_skills",
                newName: "ix_user_skills_skill_id");

            migrationBuilder.RenameIndex(
                name: "IX_team_members_team_id",
                table: "team_members",
                newName: "ix_team_members_team_id");

            migrationBuilder.RenameIndex(
                name: "IX_projects_owner_id",
                table: "projects",
                newName: "ix_projects_owner_id");

            migrationBuilder.RenameIndex(
                name: "IX_project_roles_role_id",
                table: "project_roles",
                newName: "ix_project_roles_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_project_roles_project_id",
                table: "project_roles",
                newName: "ix_project_roles_project_id");

            migrationBuilder.RenameIndex(
                name: "IX_project_role_skills_skill_id",
                table: "project_role_skills",
                newName: "ix_project_role_skills_skill_id");

            migrationBuilder.RenameIndex(
                name: "IX_project_role_skills_project_role_id",
                table: "project_role_skills",
                newName: "ix_project_role_skills_project_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_skills",
                table: "user_skills",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_teams",
                table: "teams",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_team_members",
                table: "team_members",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_skills",
                table: "skills",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_roles",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_projects",
                table: "projects",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_project_roles",
                table: "project_roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_project_role_skills",
                table: "project_role_skills",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_project_role_skills_project_roles_project_role_id",
                table: "project_role_skills",
                column: "project_role_id",
                principalTable: "project_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_project_role_skills_skills_skill_id",
                table: "project_role_skills",
                column: "skill_id",
                principalTable: "skills",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_project_roles_projects_project_id",
                table: "project_roles",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_project_roles_roles_role_id",
                table: "project_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_projects_users_owner_id",
                table: "projects",
                column: "owner_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_team_members_teams_team_id",
                table: "team_members",
                column: "team_id",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_skills_skills_skill_id",
                table: "user_skills",
                column: "skill_id",
                principalTable: "skills",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_skills_users_user_id",
                table: "user_skills",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_project_role_skills_project_roles_project_role_id",
                table: "project_role_skills");

            migrationBuilder.DropForeignKey(
                name: "fk_project_role_skills_skills_skill_id",
                table: "project_role_skills");

            migrationBuilder.DropForeignKey(
                name: "fk_project_roles_projects_project_id",
                table: "project_roles");

            migrationBuilder.DropForeignKey(
                name: "fk_project_roles_roles_role_id",
                table: "project_roles");

            migrationBuilder.DropForeignKey(
                name: "fk_projects_users_owner_id",
                table: "projects");

            migrationBuilder.DropForeignKey(
                name: "fk_team_members_teams_team_id",
                table: "team_members");

            migrationBuilder.DropForeignKey(
                name: "fk_user_skills_skills_skill_id",
                table: "user_skills");

            migrationBuilder.DropForeignKey(
                name: "fk_user_skills_users_user_id",
                table: "user_skills");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_skills",
                table: "user_skills");

            migrationBuilder.DropPrimaryKey(
                name: "pk_teams",
                table: "teams");

            migrationBuilder.DropPrimaryKey(
                name: "pk_team_members",
                table: "team_members");

            migrationBuilder.DropPrimaryKey(
                name: "pk_skills",
                table: "skills");

            migrationBuilder.DropPrimaryKey(
                name: "pk_roles",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_projects",
                table: "projects");

            migrationBuilder.DropPrimaryKey(
                name: "pk_project_roles",
                table: "project_roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_project_role_skills",
                table: "project_role_skills");

            migrationBuilder.RenameIndex(
                name: "ix_user_skills_user_id",
                table: "user_skills",
                newName: "IX_user_skills_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_user_skills_skill_id",
                table: "user_skills",
                newName: "IX_user_skills_skill_id");

            migrationBuilder.RenameIndex(
                name: "ix_team_members_team_id",
                table: "team_members",
                newName: "IX_team_members_team_id");

            migrationBuilder.RenameIndex(
                name: "ix_projects_owner_id",
                table: "projects",
                newName: "IX_projects_owner_id");

            migrationBuilder.RenameIndex(
                name: "ix_project_roles_role_id",
                table: "project_roles",
                newName: "IX_project_roles_role_id");

            migrationBuilder.RenameIndex(
                name: "ix_project_roles_project_id",
                table: "project_roles",
                newName: "IX_project_roles_project_id");

            migrationBuilder.RenameIndex(
                name: "ix_project_role_skills_skill_id",
                table: "project_role_skills",
                newName: "IX_project_role_skills_skill_id");

            migrationBuilder.RenameIndex(
                name: "ix_project_role_skills_project_role_id",
                table: "project_role_skills",
                newName: "IX_project_role_skills_project_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_skills",
                table: "user_skills",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_teams",
                table: "teams",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_team_members",
                table: "team_members",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_skills",
                table: "skills",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                table: "roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_projects",
                table: "projects",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_roles",
                table: "project_roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_role_skills",
                table: "project_role_skills",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_role_skills_project_roles_project_role_id",
                table: "project_role_skills",
                column: "project_role_id",
                principalTable: "project_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_role_skills_skills_skill_id",
                table: "project_role_skills",
                column: "skill_id",
                principalTable: "skills",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_roles_projects_project_id",
                table: "project_roles",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_roles_roles_role_id",
                table: "project_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_projects_users_owner_id",
                table: "projects",
                column: "owner_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_team_members_teams_team_id",
                table: "team_members",
                column: "team_id",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_skills_skills_skill_id",
                table: "user_skills",
                column: "skill_id",
                principalTable: "skills",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
