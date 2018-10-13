using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementService.Data.Migrations
{
    public partial class MobileNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersInRoles_RoleId",
                table: "UsersInRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrgId",
                table: "Users",
                column: "OrgId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Orgs_OrgId",
                table: "Users",
                column: "OrgId",
                principalTable: "Orgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInRoles_Roles_RoleId",
                table: "UsersInRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInRoles_Users_UserId",
                table: "UsersInRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Orgs_OrgId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInRoles_Roles_RoleId",
                table: "UsersInRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInRoles_Users_UserId",
                table: "UsersInRoles");

            migrationBuilder.DropIndex(
                name: "IX_UsersInRoles_RoleId",
                table: "UsersInRoles");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrgId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Users");
        }
    }
}
